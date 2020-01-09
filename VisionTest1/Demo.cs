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
        public void showDemo()
        {
            if (GVar.imageFlip)
            {
                FlipTestImage();
            }
            SetMat();
            SetMatRefFromFile();
            //Demo(ImageSet, ImageSetRef);
            Demo(ImageMain, ImageMainRef);
            SetMatF1();
            //Demo(ImageMain_F1, ImageMainRef);
            SetMatF2();
            //Demo(ImageMain_F2, ImageMainRef);
        }
        public void Demo(Mat img,Mat imgRef)
        {

            //New Mat to show the test result
            Mat showTestResult = new Mat(600, 350, MatType.CV_8UC3, new Scalar(255, 255, 255));
            Cv2.PutText(showTestResult, "TestResult:", new Point(10, 30), HersheyFonts.HersheyComplex, 0.8, new Scalar(0, 0, 0), 1, LineTypes.AntiAlias);
            if (GVar.debugVision == true)
            {
                Cv2.ImShow("TestResult", showTestResult);
                Cv2.WaitKey();
            }

            // ??better to change from the threshold method to erode method to remove small noise but not real defect
            Cv2.Threshold(img, img, 30, 255, ThresholdTypes.Tozero);
            Cv2.Threshold(imgRef, imgRef, 30, 255, ThresholdTypes.Tozero);

            //1. Get blob numbers
            int blobs = GetBlobs(img, GVar.debugVision);
            Cv2.PutText(showTestResult, "1.Blobs: " + blobs, new Point(10, 60), HersheyFonts.HersheySimplex, 0.6, new Scalar(0, 0, 0), 1, LineTypes.AntiAlias);
            if (GVar.debugVision == true)
            {
                Cv2.ImShow("TestResult", showTestResult);
                Cv2.WaitKey();
            }

            //2. Mser sample to find closed area
            int iClosedArea = MserSample(img, GVar.debugVision);
            Cv2.PutText(showTestResult, "2.Closed areas: " + iClosedArea, new Point(10, 90), HersheyFonts.HersheySimplex, 0.6, new Scalar(0, 0, 0), 1, LineTypes.AntiAlias);
            if (GVar.debugVision == true)
            {
                Cv2.ImShow("TestResult", showTestResult);
                Cv2.WaitKey();
            }

            //3.Measure blob areas
            double blob_area = MeasureArea(img,GVar.debugVision);
            Cv2.PutText(showTestResult, "3.Contour Area: " + blob_area.ToString(), new Point(10, 120), HersheyFonts.HersheySimplex, 0.6, new Scalar(0, 0, 0), 1, LineTypes.AntiAlias);
            if (GVar.debugVision == true)
            {
                Cv2.ImShow("TestResult", showTestResult);
                Cv2.WaitKey();
            }

            //4. Get test picture Hue/Saturation/Color with average/Min/Max value
            Cv2.PutText(showTestResult, "4.HSV average value", new Point(10, 150), HersheyFonts.HersheySimplex, 0.6, new Scalar(0, 0, 0), 1, LineTypes.AntiAlias);
            float[][] ffVal = ColorTestHSV(img);
            string[] hsvString = { "Hue ave = ", "Sat ave = ", "Lum ave = " };
            for (int k = 0; k < 3; k++)
            {
                GVar.fHSV[k] = ffVal[k][1];  //[0]- min; [1]-average; [2]-max
                Cv2.PutText(showTestResult, hsvString[k] + GVar.fHSV[k].ToString(), new Point(10, 180+k*30), HersheyFonts.HersheySimplex, 0.6, new Scalar(0, 0, 0), 1, LineTypes.AntiAlias);
            }
            if (GVar.debugVision == true)
            {
                Cv2.ImShow("TestResult", showTestResult);
                Cv2.WaitKey();
            }

            //6. Uniformity test...

            //5. Compare test and reference picture with Histogram CompareHist method, sensitive to color change
            //double[] Ratios = CompareHist(img, imgRef,GVar.debugVision);
            double ratio = CompareImageByHist(img, imgRef, GVar.debugVision);
            Cv2.PutText(showTestResult, "5.Histogram compare", new Point(10, 270), HersheyFonts.HersheySimplex, 0.6, new Scalar(0, 0, 0), 1, LineTypes.AntiAlias);
            Cv2.PutText(showTestResult, "H/S : " + ratio.ToString(), new Point(10, 300), HersheyFonts.HersheySimplex, 0.6, new Scalar(0, 0, 0), 1, LineTypes.AntiAlias);
            //string[] HSV = { "H", "S", "V" };
            //for (int k = 0; k < 3; k++)
            //{
            //    GVar.G_CompareHist[k] = Ratios[k];
            //    Cv2.PutText(showTestResult, HSV[k] + ": " + GVar.G_CompareHist[k].ToString(), new Point(10, 300 + k * 30), HersheyFonts.HersheySimplex, 0.6, new Scalar(0, 0, 0), 1, LineTypes.AntiAlias);
            //}
            if (GVar.debugVision == true)
            {
                Cv2.ImShow("TestResult", showTestResult);
                Cv2.WaitKey();
            }


            //7. Keypoints method
            float matchRate = MatchTemplate(ImageROI, imgRef, GVar.debugVision);
            Cv2.PutText(showTestResult, "6.KeyPoints: " + matchRate.ToString(), new Point(10, 330), HersheyFonts.HersheySimplex, 0.6, new Scalar(0, 0, 0), 1, LineTypes.AntiAlias);
            Cv2.PutText(showTestResult, "The end of vision test!", new Point(10, 360), HersheyFonts.HersheySimplex, 0.6, new Scalar(0, 0, 0), 1, LineTypes.AntiAlias);
            Cv2.ImShow("TestResult", showTestResult);
            Cv2.WaitKey();
            Cv2.DestroyWindow("TestResult");
            Console.WriteLine("  ");


            //7. HoughLineP to detect lines

            

        }

        public double[] CompareHist(Mat img, Mat template, bool showImage = false)
        {
            Mat[] imgs = HistogramCalculation(img,showImage);
            Mat[] templates = HistogramCalculation(template,showImage);

            double[] ratios = new double[3];
            for (int i = 0; i < imgs.Count(); i++)
            {
                ratios[i] = Cv2.CompareHist(imgs[i], templates[i], HistCompMethods.Bhattacharyya);
            }

            return ratios;
        }


        public double CompareImageByHist(Mat img,Mat refImg,bool showImage = false)
        {
            Mat imgHsv = new Mat();
            Mat refImgHsv = new Mat();
            Cv2.CvtColor(img, imgHsv, ColorConversionCodes.RGB2HSV);
            Cv2.CvtColor(refImg, refImgHsv, ColorConversionCodes.RGB2HSV);

            Mat[] imgHsvs = Cv2.Split(imgHsv);
            Mat[] refImgHsvs = Cv2.Split(refImgHsv);

            int bin1 = 50;
            int bin2 = 60;
            int[] bins = { bin1, bin2 };

            int[] channels = { 0, 1 };

            Rangef[] ranges = new Rangef[]
            {
                new Rangef(0,180),
                new Rangef(0,256)
            };

            Mat imgHist = new Mat(img.Size(), MatType.CV_32FC2);
            Mat refImgHist = new Mat(img.Size(), MatType.CV_32FC2);

            Cv2.CalcHist(imgHsvs, channels, new Mat(), imgHist, 2, bins, ranges, true, false);
            Cv2.Normalize(imgHist, imgHist, 1, 0, NormTypes.MinMax, -1, null);

            Cv2.CalcHist(refImgHsvs, channels, new Mat(), refImgHist, 2, bins, ranges, true, false);
            Cv2.Normalize(refImgHist, refImgHist, 1, 0, NormTypes.MinMax, -1, null);

            double ratio = Cv2.CompareHist(imgHist, refImgHist, HistCompMethods.KLDiv);

            if (showImage == true)
            {
                Mat img1 = img.Clone();
                Cv2.PutText(img1, ratio.ToString(), new Point(50, 50), HersheyFonts.HersheyPlain, 1, new Scalar(0, 255, 0), 2, LineTypes.AntiAlias);
                Cv2.ImShow("CompareHistTestVSRef", img1);
                Cv2.WaitKey();
                Cv2.DestroyWindow("CompareHistTestVSRef");
            
            }
            return ratio;
        }

    }
}
