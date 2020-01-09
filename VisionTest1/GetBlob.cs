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
        public int GetBlobs(Mat img, bool showImage = false)
        {

            Mat gray = img.CvtColor(ColorConversionCodes.BGR2GRAY);
            //Mat binary = gray.Threshold(0, 255, ThresholdTypes.Otsu | ThresholdTypes.Binary);
            Mat binary = gray.Threshold(50, 255, ThresholdTypes.Binary);


            //2.Define/search ROI area
            Mat labelView = img.EmptyClone();
            Mat rectView = binary.CvtColor(ColorConversionCodes.GRAY2BGR);
            ConnectedComponents cc = Cv2.ConnectedComponentsEx(binary);
            if (cc.LabelCount <= 1)
                throw new Exception("no blob found");

            //draw lables
            cc.RenderBlobs(labelView);

            //draw boxes except background
            foreach (var blob in cc.Blobs.Skip(1))
            {
                rectView.Rectangle(blob.Rect, Scalar.Red);
            }

            if (showImage == true)
            {
                using (new Window("blob image", rectView))
                //using (new Window("labelview image", labelView))
                {
                    //Cv2.WaitKey(1000);
                    Cv2.WaitKey(0);
                    Cv2.DestroyWindow("blob image");
                    //Cv2.DestroyWindow("labelview image");
                }
            }
            return cc.LabelCount - 1;
        }
        //public int GetBlobs(Mat img, bool showImage = false)
        //{           

        //    Mat gray = img.CvtColor(ColorConversionCodes.BGR2GRAY);
        //    //Mat binary = gray.Threshold(0, 255, ThresholdTypes.Otsu | ThresholdTypes.Binary);
        //    Mat binary = gray.Threshold(50, 255, ThresholdTypes.Binary);


        //    //2.Define/search ROI area
        //    Mat labelView = img.EmptyClone();
        //    Mat rectView = binary.CvtColor(ColorConversionCodes.GRAY2BGR);
        //    ConnectedComponents cc = Cv2.ConnectedComponentsEx(binary);
        //    if (cc.LabelCount <= 1)
        //        throw new Exception("no blob found");

        //    //draw lables
        //    cc.RenderBlobs(labelView);

        //    //draw boxes except background
        //    foreach (var blob in cc.Blobs.Skip(1))
        //    {
        //        rectView.Rectangle(blob.Rect, Scalar.Red);
        //    }

        //    if (showImage == true)
        //    {
        //        using (new Window("blob image", rectView))
        //        //using (new Window("labelview image", labelView))
        //        {
        //            //Cv2.WaitKey(1000);
        //            Cv2.WaitKey(0);
        //            Cv2.DestroyWindow("blob image");
        //            //Cv2.DestroyWindow("labelview image");
        //        }
        //    }
        //    return cc.LabelCount - 1;
        //}    



    }
}
