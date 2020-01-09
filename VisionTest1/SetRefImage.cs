using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;


namespace VisionTest1
{
    public partial class IMProcess
    {
        public void SaveReferenceImages(bool flip)
        {
            //1. read src file and create mat files
            if (flip)
            {
                FlipTestImage();
            }
            SetMat();           
   
            Cv2.ImWrite(Images.picROIRef, ImageROI);
            Cv2.ImWrite(Images.picSetRef, ImageSet);
            Cv2.ImWrite(Images.picPlusRef, ImagePlus);
            Cv2.ImWrite(Images.picMainRef, ImageMain);

        }    
        
        public void FlipTestImage()
        {
            if (ImageOri == null)
            {
                ImageOri = new Mat(Images.picSWS, ImreadModes.Color);
            }
            Cv2.Flip(ImageOri, ImageOri, FlipMode.XY);
            Cv2.ImWrite(Images.picSWS, ImageOri);
        }

            
    }
}
