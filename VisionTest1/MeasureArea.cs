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
        public double MeasureArea(Mat img, bool showImage = false)   // (Mat img,bool showImage = false)
        {

            RNG g_rng = new RNG(12345);           
            Mat[] g_vContours;
            Mat g_vHierarchy = new Mat();

            //Mat img = new Mat(Images.picMainRef, ImreadModes.Color);
            //Cv2.ImShow("SRC", img);

            //灰度图
            Mat g_grayImage = img.CvtColor(ColorConversionCodes.BGR2GRAY);            

            //模糊
            Cv2.Blur(g_grayImage, g_grayImage,new Size(3, 3));
            //Cv2.ImShow("grayblur",g_grayImage);

            ////canny
            //Mat g_cannyMat_output = new Mat();
            //Cv2.Canny(g_grayImage, g_cannyMat_output, g_nThresh, g_nThresh * 2, 3);
            //Cv2.ImShow("canny", g_cannyMat_output);

            //二值化
            Mat binary = new Mat();
            Cv2.Threshold(g_grayImage, binary, 20, 255, ThresholdTypes.Tozero);
            //Cv2.ImShow("binary", binary);

            //形态学操作
            Mat morphImg = new Mat();
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(5, 5), new Point(-1, -1));
            Cv2.MorphologyEx(binary, morphImg, MorphTypes.Close, kernel, new Point(-1, -1));
            //Cv2.ImShow("morph Image", morphImg);

            //Contours
            Mat Contours = Mat.Zeros(img.Size(), MatType.CV_8UC3);//img.EmptyClone();
            Cv2.FindContours(binary, out g_vContours,g_vHierarchy,RetrievalModes.Tree,ContourApproximationModes.ApproxSimple,new Point(0,0));
            //Cv2.ImShow("test", Contours);

            double g_ContourArea = 0;
            for (int i = 0; i < g_vContours.Count(); i++)
            {
                Scalar color = new Scalar(g_rng.Uniform(0, 255), g_rng.Uniform(0, 255), g_rng.Uniform(0, 255));//随机生成颜色值
                
                Cv2.DrawContours(Contours, g_vContours, i, color, -1, LineTypes.Link8, null, int.MaxValue, null);
               
                double dContourArea = Cv2.ContourArea(g_vContours[i]);
                g_ContourArea += dContourArea;
            }
            if (showImage == true)
            {
                Cv2.ImShow("Contours", Contours);
                Cv2.WaitKey();
                Cv2.DestroyAllWindows();
            }

            return g_ContourArea;
        }      
    }
}
