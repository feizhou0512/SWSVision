using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Video.DirectShow;

namespace VisionTest1
{

    class Camera
    {      
            public string[] GetCamera()
        {
            FilterInfoCollection videoDevices;
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)
                throw new ApplicationException();

            string[] devicesName = new string[videoDevices.Count];

            int i = 0;
           
            foreach(FilterInfo device in videoDevices)
            {
                devicesName[i] = device.Name;
            }

            return devicesName;        
        }



    }
}
