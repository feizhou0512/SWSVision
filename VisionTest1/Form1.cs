using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging;
using AForge.Imaging.Filters;

using GxIAPINET;
using GxIAPINET.Sample.Common;

using TestClass.SWS;


namespace VisionTest1
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection videoDevices;
        bool m_bIsOpen = false;                                 ///< The flag indicates whether the first device has been opened
        bool m_bIsSnap = false;                                 ///< The flag indicates whether AcquisitionStart command has been send 
        IGXFactory m_objIGXFactory = null;                      ///< The handle for factory
        IGXDevice m_objIGXDevice = null;                        ///< The handle for device
        IGXStream m_objIGXStream = null;                        ///< The handle for stream
        IGXFeatureControl m_objIGXFeatureControl = null;        ///< The handle for feature control
        CStatistics m_objStatistic = new CStatistics();         ///< Statistics
        CStopWatch m_objStopTime = new CStopWatch();            ///< Stopwatch
        GxBitmap m_objGxBitmap = null;
        PressAll Ki = new PressAll();
        IMProcess im = new IMProcess();

        
        public Form1()
        {
            InitializeComponent();
            AllocConsole();
            //FreeConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();



        private void __InitDevice()
        {
            if (null != m_objIGXFeatureControl)
            {
                //Set the continuous frame acquisition mode
                m_objIGXFeatureControl.GetEnumFeature("AcquisitionMode").SetValue("Continuous");

                //Set the TriggerMode on
                m_objIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("On");

                //Set the TriggerSource to SoftTrigger
                m_objIGXFeatureControl.GetEnumFeature("TriggerSource").SetValue("Software");
            }
        }

        private void __CloseStream()
        {
            try
            {
                // Close stream
                if (null != m_objIGXStream)
                {
                    m_objIGXStream.Close();
                    m_objIGXStream = null;
                }
            }
            catch (Exception)
            {
            }
        }

        private void __CloseDevice()
        {
            try
            {
                //Close device
                if (null != m_objIGXDevice)
                {
                    m_objIGXDevice.Close();
                    m_objIGXDevice = null;
                }
            }
            catch (Exception)
            {
            }
        }

        private void __CloseAll()
        {
            try
            {
                // Check whether the device has been stoped acquisition
                if (m_bIsSnap)
                {
                    if (null != m_objIGXFeatureControl)
                    {
                        m_objIGXFeatureControl.GetCommandFeature("AcquisitionStop").Execute();
                        m_objIGXFeatureControl = null;
                    }
                }
            }
            catch (Exception)
            {
            }
            m_bIsSnap = false;
            try
            {
                // Stop stream channel acquisition and close stream
                if (null != m_objIGXStream)
                {
                    m_objIGXStream.StopGrab();
                    m_objIGXStream.Close();
                    m_objIGXStream = null;
                }
            }
            catch (Exception)
            {

            }

            // Close device
            __CloseDevice();
            m_bIsOpen = false;
        }

        private void SetReferenceImage_Click(object sender, EventArgs e)
        {
            try
            {
                
                GVar.imageFlip = Flip.Checked;
                IMProcess setRef = new IMProcess();
                setRef.SaveReferenceImages(GVar.imageFlip);
                MessageBox.Show("Ref image are set successfully!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


            private void ExitProgram_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Environment.Exit(0);
        }



       

       

        private void DemoShow_Click(object sender, EventArgs e)
        {
            GVar.showImageMatchTemplate = ShowMatchImage.Checked;
            GVar.showImageBlob = showBlobs.Checked;
            GVar.showImageHist = showHist.Checked;
            GVar.debugVision = Debug.Checked;
            GVar.imageFlip = Flip.Checked;
            //IMProcess newIMP = new IMProcess();
            //newIMP.showDemo();
            im.showDemo();
            
        }

        private void Can_DO_Click(object sender, EventArgs e)
        {
            Ki.Do();
        }


        private void Snapshot_Click(object sender, EventArgs e)
        {
            try
            { 
                //1.Open Device
                // Before using any GxIAPINET methods, the GxIAPINET must be initialized.
                m_objIGXFactory = IGXFactory.GetInstance();
                m_objIGXFactory.Init();

                //open device
                List<IGXDeviceInfo> listGXDeviceInfo = new List<IGXDeviceInfo>();

                // Close stream
                __CloseStream();

                // If the device is opened then close it to ensure the camera could open again.
                __CloseDevice();

                // Enumerate all camera devices
                m_objIGXFactory.UpdateDeviceList(200, listGXDeviceInfo);

                // Check if found any device
                if (listGXDeviceInfo.Count <= 0)
                {
                    MessageBox.Show("No devices found!");
                    return;
                }

                //Open the first found device 
                m_objIGXDevice = m_objIGXFactory.OpenDeviceBySN(listGXDeviceInfo[0].GetSN(), GX_ACCESS_MODE.GX_ACCESS_EXCLUSIVE);
                m_objIGXFeatureControl = m_objIGXDevice.GetRemoteFeatureControl();


                // Open stream 
                if (null != m_objIGXDevice)
                {
                    m_objIGXStream = m_objIGXDevice.OpenStream(0);
                }

                // It is recommended that the user set the camera's stream channel packet length value
                // according to the current network environment after turning on 
                // the network camera to improve the collection performance of the network camera. 
                // For the setting method, refer to the following code.
                GX_DEVICE_CLASS_LIST objDeviceClass = m_objIGXDevice.GetDeviceInfo().GetDeviceClass();
                if (GX_DEVICE_CLASS_LIST.GX_DEVICE_CLASS_GEV == objDeviceClass)
                {
                    // Determine whether the device supports the stream channel packet function.
                    if (true == m_objIGXFeatureControl.IsImplemented("GevSCPSPacketSize"))
                    {
                        // Get the optimal packet length value of the current network environment
                        uint nPacketSize = m_objIGXStream.GetOptimalPacketSize();
                        // Set the optimal packet length value to the stream channel packet length of the current device.
                        m_objIGXFeatureControl.GetIntFeature("GevSCPSPacketSize").SetValue(nPacketSize);
                    }
                }

                __InitDevice();

                m_objGxBitmap = new GxBitmap(m_objIGXDevice, m_pic_ShowImage);



                //2.Start acquisition
                // Start stream channel acquisition
                if (null != m_objIGXStream)
                {
                    m_objIGXStream.StartGrab();
                }

                // Send AcquisitionStart command
                if (null != m_objIGXFeatureControl)
                {
                    m_objIGXFeatureControl.GetCommandFeature("AcquisitionStart").Execute();
                }



                //3.Snapshot
                IImageData objIImageData = null;
                double dElapsedtime = 0;
                uint nTimeout = 500;


                //Flush image queues to clear out-of-date images
                if (null != m_objIGXStream)
                {
                    m_objIGXStream.FlushQueue();
                }

                //Send TriggerSoftware commands
                if (null != m_objIGXFeatureControl)
                {
                    m_objIGXFeatureControl.GetCommandFeature("TriggerSoftware").Execute();
                }

                //Get image
                if (null != m_objIGXStream)
                {
                    //Start stopwatch
                    m_objStopTime.Start();

                    objIImageData = m_objIGXStream.GetImage(nTimeout);

                    //Stop stopwatch and get the ElapsedTime
                    dElapsedtime = m_objStopTime.Stop();
                }

                m_objGxBitmap.Show(objIImageData);
                string strFileName = @"D:\TestImages\SWS.bmp";
                m_objGxBitmap.SaveBmp(objIImageData, strFileName);


                if (null != objIImageData)
                {
                    // Release resource
                    objIImageData.Destroy();
                }

                //4.Stop acquisition
                // Send AcquisitionStop command
                if (null != m_objIGXFeatureControl)
                {
                    m_objIGXFeatureControl.GetCommandFeature("AcquisitionStop").Execute();
                }

                // Stop stream channel acquisition
                if (null != m_objIGXStream)
                {
                    m_objIGXStream.StopGrab();
                }

                //5.Close device
                // Reset statistical time count
                m_objStatistic.Reset();

                // close stream and device
                __CloseAll();



            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Can_init_Click_1(object sender, EventArgs e)
        {
            Ki.Initialize();
        }

        private void Can_Close_Click_1(object sender, EventArgs e)
        {
            Ki.Close();
        }
    }
}
