using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.Windows.Forms;
using System.Diagnostics;


namespace VisionTest1
{
    
    public partial class IMProcess
    {        
        //public void MatchTest()
        //{

        //    //GetBlobs();            
        //    //SetReferenceImages();
        //    SetMat();
        //    SetMatRefFromFile();
        //    GVar.matchRateSet = MatchTemplate(ImageROI,ImageSetRef,GVar.showImageMatchTemplate,"setMatch");
        //    GVar.matchRatePlus = MatchTemplate(ImageROI, ImagePlusRef, GVar.showImageMatchTemplate, "plusMatch");
        //    GVar.matchRateMain = MatchTemplate(ImageROI, ImageMainRef, GVar.showImageMatchTemplate, "mainMatch");

        //    float[][] ffVal = ColorTestHSV(ImageSetRef);
        //    for (int k = 0; k < 3; k++)
        //    {
        //        GVar.fHSV[k] = ffVal[k][1];
        //    }
        //    ffVal = ColorTestHSV(ImagePlus);
        //    for (int k = 0; k < 3; k++)
        //    {
        //        GVar.fHSVPlus[k] = ffVal[k][1];
        //    }
        //    ffVal = ColorTestHSV(ImageMain);
        //    for (int k = 0; k < 3; k++)
        //    {
        //        GVar.fHSVMain[k] = ffVal[k][1];
        //    }

        //    GVar.iBlobsSet = GetBlobs(ImageSet, GVar.showImageBlob);
        //    GVar.iBlobsPlus = GetBlobs(ImagePlus, GVar.showImageBlob);
        //    GVar.iBlobsMain = GetBlobs(ImageMain, GVar.showImageBlob);

        //    //float[][] ffVal = new float[3][];
        //    //ffVal = ColorTestBGR(ImageSetRef);
        //    //GVar.fColorSet = ffVal[0][1];  //b color average
        //}

        
        public float[][] ColorTestHSV(Mat img)
        {
            
            Mat hsvImage = img.CvtColor(ColorConversionCodes.BGR2HSV);
            Mat[] dftPlanes;
            Cv2.Split(hsvImage, out dftPlanes);      //in HSV order

            //byte[] h = new byte[dftPlanes[0].Rows * dftPlanes[0].Cols];
            //dftPlanes[0].GetArray(0, 0, h);
            //byte[] s = new byte[dftPlanes[2].Rows * dftPlanes[2].Cols];
            //dftPlanes[1].GetArray(0, 0, s);

            //Cv2.Threshold(dftPlanes[0], dftPlanes[0], 20, 255, ThresholdTypes.Tozero);
            //Cv2.Threshold(dftPlanes[1], dftPlanes[1], 20, 255, ThresholdTypes.Tozero);
            //Cv2.Threshold(dftPlanes[2], dftPlanes[2], 20, 255, ThresholdTypes.Tozero);
            //Cv2.ImShow("h", dftPlanes[0]);
            //Cv2.ImShow("s", dftPlanes[1]);
            //Cv2.ImShow("v", dftPlanes[2]);
            //Cv2.WaitKey(0);
            //Cv2.DestroyAllWindows();

            float[][] ffval = new float[3][];
            for (int k = 0; k < 3; k++)
            {
                ffval[k] = new float[3];
            }
            ffval[0] = pixelTestVector1(dftPlanes[0]);   // Hue
            ffval[1] = pixelTestVector1(dftPlanes[1]);   // Saturation
            ffval[2] = pixelTestVector1(dftPlanes[2]);   // Luminance      
            
            return ffval;
        }

        public float[][] ColorTestBGR(Mat img)
        {

            //Mat hsvImage = img.CvtColor(ColorConversionCodes.BGR2HSV);
            Mat[] dftPlanes;
            Cv2.Split(img, out dftPlanes);     

            float[][] ffval = new float[3][];
            for (int k = 0; k < 3; k++)
            {
                ffval[k] = new float[3];
            }
            ffval[0] = pixelTestVector1(dftPlanes[0]);   // Hue
            ffval[1] = pixelTestVector1(dftPlanes[1]);   // Saturation
            ffval[2] = pixelTestVector1(dftPlanes[2]);   // Luminance      

            return ffval;
        }

        public float[][] ColorTestXYZ(Mat img)
        {

            Mat hsvImage = img.CvtColor(ColorConversionCodes.BGR2XYZ);
            Mat[] dftPlanes;
            Cv2.Split(hsvImage, out dftPlanes);      //in HSV order

            //byte[] h = new byte[dftPlanes[0].Rows * dftPlanes[0].Cols];
            //dftPlanes[0].GetArray(0, 0, h);
            //byte[] s = new byte[dftPlanes[2].Rows * dftPlanes[2].Cols];
            //dftPlanes[1].GetArray(0, 0, s);

            //Cv2.Threshold(dftPlanes[0], dftPlanes[0], 20, 255, ThresholdTypes.Tozero);
            //Cv2.Threshold(dftPlanes[1], dftPlanes[1], 20, 255, ThresholdTypes.Tozero);
            //Cv2.Threshold(dftPlanes[2], dftPlanes[2], 20, 255, ThresholdTypes.Tozero);
            //Cv2.ImShow("h", dftPlanes[0]);
            //Cv2.ImShow("s", dftPlanes[1]);
            //Cv2.ImShow("v", dftPlanes[2]);
            //Cv2.WaitKey(0);
            //Cv2.DestroyAllWindows();

            float[][] ffval = new float[3][];
            for (int k = 0; k < 3; k++)
            {
                ffval[k] = new float[3];
            }
            ffval[0] = pixelTestVector1(dftPlanes[0]);   // Hue
            ffval[1] = pixelTestVector1(dftPlanes[1]);   // Saturation
            ffval[2] = pixelTestVector1(dftPlanes[2]);   // Luminance      

            return ffval;
        }


        public float[] pixelTestVector1(Mat img)
        {
            float[] fVal = new float[3];
            int min = 9999;
            int max = 0;
            int sum = 0;
            int count = 0;
            for (int i=0;i<img.Rows;i++)
            {
                for (int j=0;j<img.Cols;j++)
                {
                    int iVal = (int)img.At<byte>(i,j);
                    if (iVal > 0)
                    {
                        sum += iVal;
                        count++;
                        if (iVal < min)
                            min = iVal;
                        if (iVal > max)
                            max = iVal;
                    }                    
                }
            }

            //[0]- min; [1]-average; [2]-max
            fVal[0] = min;
            fVal[2] = max;
            if (count != 0)
                fVal[1] = (float)sum / count;
            else
                fVal[1] = 0;

            return fVal;
        }

        public float[] pixelTestUniformity(Mat img)
        {
            //threshold method should be used before it
            float[] fVal = new float[3];
            int min = 9999;
            int max = 0;
            int sum = 0;
            int count = 0;
            for (int i = 0; i < img.Rows; i++)
            {
                for (int j = 0; j < img.Cols; j++)
                {
                    int iVal = (int)img.At<byte>(i, j);
                    if (iVal > 0)
                    {
                        sum += iVal;
                        count++;
                        if (iVal < min)
                            min = iVal;
                        if (iVal > max)
                            max = iVal;
                    }
                }
            }

            //[0]- min; [1]-average; [2]-max
            fVal[0] = min;
            fVal[2] = max;
            if (count != 0)
                fVal[1] = (float)sum / count;
            else
                fVal[1] = 0;

            return fVal;
        }

        //public float[][] pixelTestVector3(Mat img)
        //{
        //    float[][] ffval = new float[3][];
        //    for (int k = 0; k < 3; k++)
        //    {
        //        ffval[k] = new float[3];
        //    }
        //    int[] min = new int[3] { 256, 256, 256 };
        //    int[] max = new int[3] { 0,0,0 };
        //    int[] sum = new int[3] { 0, 0, 0 };
        //    int[] count = new int[3] { 0, 0, 0 };
        //    int[] iVal = new int[3] { 0, 0, 0 };
        //    for (int i = 0; i < img.Rows; i++)
        //    {
        //        for (int j = 0; j < img.Cols; j++)
        //        {
        //            Vec3b iVector = img.At<Vec3b>(i, j);
        //            iVal[0] = (int)iVector.Item0;
        //            iVal[1] = (int)iVector.Item0;
        //            iVal[2] = (int)iVector.Item2;
        //            for (int k = 0; k < 3; k++)
        //            {
        //                if (iVal[k] > 0)
        //                {
        //                    sum[k] += iVal[k];
        //                    count[k]++;
        //                    if (iVal[k] < min[k])
        //                        min[k] = iVal[k];
        //                    if (iVal[k] > max[k])
        //                        max[k] = iVal[k];
        //                }                      
        //            }
        //        }
        //    }

        //    for (int i = 0; i < 3; i++)
        //    {
        //        ffval[i][0] = (float)min[i];
        //        ffval[i][2] = (float)max[i];
        //        if (count[i] != 0)
        //            ffval[i][1] = (float)sum[i] / count[i];
        //    }

        //    return ffval;

        //    //if (count != 0)
        //    //    return (float)sum / count;
        //    //else
        //    //    return 0;

        //}

        //public float[][] ColorTestBGR(Mat img)
        //{
        //    Mat[] dftPlanes;
        //    Cv2.Split(img, out dftPlanes);

        //    byte[] b = new byte[dftPlanes[0].Rows * dftPlanes[0].Cols];
        //    dftPlanes[0].GetArray(0, 0, b);
        //    byte[] g = new byte[dftPlanes[1].Rows * dftPlanes[1].Cols];
        //    dftPlanes[0].GetArray(0, 0, g);
        //    byte[] r = new byte[dftPlanes[2].Rows * dftPlanes[2].Cols];
        //    dftPlanes[1].GetArray(0, 0, r);
        //    Cv2.Threshold(dftPlanes[0], dftPlanes[0], 20, 255, ThresholdTypes.Tozero);      //b
        //    Cv2.Threshold(dftPlanes[1], dftPlanes[1], 20, 255, ThresholdTypes.Tozero);      //g
        //    Cv2.Threshold(dftPlanes[2], dftPlanes[2], 20, 255, ThresholdTypes.Tozero);      //r
        //    Cv2.ImShow("b", dftPlanes[0]);
        //    Cv2.ImShow("g", dftPlanes[1]);
        //    Cv2.ImShow("r", dftPlanes[2]);
        //    Cv2.WaitKey(0);
        //    Cv2.DestroyAllWindows();

        //    float[][] ffVal = new float[3][];   //{ 0.0f,0.0f,0.0f,0.0f, 0.0f, 0.0f, 0.0f, 0.0f,0.0f };
        //    ffVal = pixelTestVector3(img);
        //    //ffVal[0] = pixelTestVector1(dftPlanes[0]);
        //    //ffVal[1] = pixelTestVector1(dftPlanes[1]);
        //    //ffVal[2] = pixelTestVector1(dftPlanes[2]);

        //    return ffVal;
        //}


    }
}
