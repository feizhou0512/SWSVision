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
        Mat calHashCode(Mat imgOri)
        {
            Mat img = new Mat();
            Cv2.CvtColor(imgOri, img, ColorConversionCodes.BGR2GRAY);
            Cv2.Resize(img, img, new Size(8, 8));
            Scalar imgMean = Cv2.Mean(img);
            Cv2.Threshold(img, img, imgMean[0], 1, ThresholdTypes.Binary);
            return (img);
        }
        public void matchTestCompareHist()
        {
            Mat img = new Mat(Images.picTest, ImreadModes.Color);
            Mat refImg = new Mat(Images.picSetRef, ImreadModes.Color);
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

            

            double ratio = Cv2.CompareHist(refImgHist, imgHist, HistCompMethods.Bhattacharyya);

        }


       public int MserSample(Mat src,bool showImage = false)     //find closed area
        {
            int i = 0;
            using (Mat gray = new Mat())
            using (Mat dst = src.Clone())
            {
                Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

                i = CppStyleMSER(gray, dst);  // C++ style

                if (showImage == true)
                {
                    using (new Window("MSER src", src))
                    using (new Window("MSER gray", gray))
                    using (new Window("MSER dst", dst))
                    {
                        Cv2.WaitKey();
                        Cv2.DestroyAllWindows();
                    }
                }
            }
            return i;
        }

        private int CppStyleMSER(Mat gray, Mat dst)
        {
            MSER mser = MSER.Create();
            Point[][] contours;
            Rect[] bboxes;
            mser.DetectRegions(gray, out contours, out bboxes);
            foreach (Point[] pts in contours)
            {
                Scalar color = Scalar.RandomColor();
                foreach (Point p in pts)
                {
                    dst.Circle(p, 1, color);
                }
            }

            //what will happen if you draw the box, fei
            List<Rect> Lboxes = new List<Rect>();
            foreach (Rect rt in bboxes)
            {
                //此处可加一个算法,排除相似的box
                int length = Lboxes.Count();
                if (length != 0)
                {
                    bool iUpdate = false;
                    int iCompare = 0;
                    Rect x = new Rect();
                    foreach (Rect rtl in Lboxes)
                    {
                        iCompare = CompareRect(rtl, rt);
                        if (iCompare == 2)
                        {
                            iUpdate = true;
                            x = rtl;
                            break;                           
                        }             
                    }

                    if (iCompare == 0)
                    {
                        Lboxes.Add(rt);
                    }
                    else if (iCompare == 2)
                    {
                        Lboxes.Remove(x);
                        Lboxes.Add(rt);
                    }
                }
                else
                {
                    Lboxes.Add(rt);
                }
            }

            foreach (Rect rt in Lboxes)
            {
                Scalar color = Scalar.RandomColor();
                dst.Rectangle(rt, color);
            }

            return Lboxes.Count();
        }

        public int CompareRect(Rect r1, Rect r2)
        {
            int i = 0;
            if (r1.Contains(r2)) 
            {
                if ((Math.Abs(r1.Width - r2.Width) < 20) && (Math.Abs(r1.Height - r2.Height) < 20))
                    i = 1;
            }
            else if(r2.Contains(r1))
            {
                if ((Math.Abs(r1.Width - r2.Width) < 20) && (Math.Abs(r1.Height - r2.Height) < 20))
                    i = 2;
            }

            //i=0: not contained or not similar; i=1: r1 contain r2 and similar; i=2:r2 contain r1 and similar
            return i;
        }
        //public bool CompareRect(Rect r1,Rect r2)
        //{
        //    bool bSimilar = false;
        //    if ((Math.Abs(r1.X -r2.X) <20 ) && (Math.Abs(r1.Y - r2.Y) < 20))
        //    {
        //        if ((Math.Abs(r1.Width - r2.Width) < 20) && (Math.Abs(r1.Height - r2.Height) < 20))
        //        {
        //            bSimilar = true;
        //        }
        //    }

        //    return bSimilar;
        //}

        public void UpdateImage()   //update picuter pixel value
        {
            Mat img = new Mat(Images.picMainRef, ImreadModes.Color);
            Mat[] dftPlanes;
            Cv2.Split(img, out dftPlanes);     
 
            //Cv2.Threshold(dftPlanes[0], dftPlanes[0], 20, 255, ThresholdTypes.Tozero);
            //Cv2.Threshold(dftPlanes[1], dftPlanes[1], 20, 255, ThresholdTypes.Tozero);
            //Cv2.Threshold(dftPlanes[2], dftPlanes[2], 20, 255, ThresholdTypes.Tozero);

            updatePixel(dftPlanes[0], 60);
            updatePixel(dftPlanes[1], 20);
            updatePixel(dftPlanes[2], 60);
           
            Mat dst = img.EmptyClone();
            Cv2.Merge(dftPlanes, dst);
           
            Cv2.ImWrite(Images.picMainRef, dst);

        }

        public void updatePixel(Mat img,int increase = 50)
        {
            int x = 0;
            for (int i = 0; i < img.Rows; i++)
            {
                for (int j = 0; j < img.Cols; j++)
                {

                    x = (int)img.Get<byte>(i, j);
                    if (x > 20)   // no modification on dark area
                    {
                        x += increase;
                        if (x > 255)
                            x = 255;
                        img.Set<byte>(i, j, (byte)x);
                    }
                    
                 }
            }

        }


        
        /// 直方图计算       
        public void HistogramCalculation()
        {
            Mat img = new Mat(Images.picSetRef, ImreadModes.Color);
            Mat src = new Mat();
            Cv2.CvtColor(img, src, ColorConversionCodes.RGB2HSV);
            //using (Mat src = new Mat(Images.picSetRef, ImreadModes.Color))
            using (Mat histogram = new Mat())
            {
                //计算直方图
                Mat[] mats = Cv2.Split(src); //分割图像(把三通道分割为3个单通道)
                Mat hist_B = new Mat();
                Mat hist_G = new Mat();
                Mat hist_R = new Mat();

                int[] channels0 = { 0 };
                int[] channels1 = { 1 };
                int[] channels2 = { 2 };
                int[] histSize = { 256 };


                Rangef[] rangefs = new Rangef[]
                {
                    new Rangef(0, 256),
                };

               
                //     computes the joint dense histogram for a set of images.
                //      计算一组图像的联合密集直方图。               
                Cv2.CalcHist(mats, channels0, new Mat(), hist_B, 1, histSize, rangefs, true, false);
                Cv2.CalcHist(mats, channels1, new Mat(), hist_G, 1, histSize, rangefs, true, false);
                Cv2.CalcHist(mats, channels2, new Mat(), hist_R, 1, histSize, rangefs, true, false);

                int high = 400;
                int width = 512;
                int bin_w = width / 256;//每个bins的宽度  画布的宽度除以bins的个数
                Mat histImage = new Mat(width, high, MatType.CV_8UC3, new Scalar(0, 0, 0)); //定义一个Mat对象，相当于一个画布

                //归一化，像素值有可能数据量很大，压缩一下。是范围在定义画布的范围内。               
                Cv2.Normalize(hist_B, hist_B, 0, histImage.Rows, NormTypes.MinMax, -1, null);
                Cv2.Normalize(hist_G, hist_G, 0, histImage.Rows, NormTypes.MinMax, -1, null);
                Cv2.Normalize(hist_R, hist_R, 0, histImage.Rows, NormTypes.MinMax, -1, null);

                //绘制直方图
                for (int i = 1; i < 256; i++)//遍历直方图的级数
                {
                    //B 画线，一条线有两个点组成。首先确定每个点的坐标(x,y) .遍历从1开始。0 ~ 1 两个点组成一条线，依次类推。
                    Cv2.Line(histImage, new Point(bin_w * (i - 1), high - Math.Round(hist_B.At<float>(i - 1))), new Point(bin_w * (i - 1), high - Math.Round(hist_B.At<float>(i))), new Scalar(255, 0, 0), 1, LineTypes.AntiAlias);

                    //G
                    Cv2.Line(histImage, new Point(bin_w * (i - 1), high - Math.Round(hist_G.At<float>(i - 1))), new Point(bin_w * (i - 1), high - Math.Round(hist_G.At<float>(i))), new Scalar(0, 255, 0), 1, LineTypes.AntiAlias);

                    //R
                    Cv2.Line(histImage, new Point(bin_w * (i - 1), high - Math.Round(hist_R.At<float>(i - 1))), new Point(bin_w * (i - 1), high - Math.Round(hist_R.At<float>(i))), new Scalar(0, 0, 255), 1, LineTypes.AntiAlias);

                }
                using (new Window("SRC", WindowMode.Normal, src))
                using (new Window("histImage", WindowMode.Normal, histImage))
                {
                    Cv2.WaitKey(0);
                }
                
            }

        }

        public Mat[] HistogramCalculation(Mat img,bool showImage = false)
        {
            //Mat img = new Mat(Images.picSetRef, ImreadModes.Color);
            Mat src = new Mat();
            Cv2.CvtColor(img, src, ColorConversionCodes.RGB2HSV);

            using (Mat histogram = new Mat())
            {
                //计算直方图
                Mat[] mats = Cv2.Split(src); //分割图像(把三通道分割为3个单通道)
                //Mat hist_H = new Mat();
                //Mat hist_S = new Mat();
                //Mat hist_V = new Mat();
                Mat[] hist_HSV = { new Mat(),new Mat(),new Mat()};

                int[] channels0 = { 0 };
                int[] channels1 = { 1 };
                int[] channels2 = { 2 };
                int[] histSize = { 16 };    //256

                Rangef[] rangefh = new Rangef[]
               {
                    new Rangef(0, 180),
               };
                Rangef[] rangefsv = new Rangef[]
                {
                    new Rangef(0, 256),
                };


                //     computes the joint dense histogram for a set of images.
                //      计算一组图像的联合密集直方图。               
                Cv2.CalcHist(mats, channels0, new Mat(), hist_HSV[0], 1, histSize, rangefh, true, false);
                Cv2.CalcHist(mats, channels1, new Mat(), hist_HSV[1], 1, histSize, rangefsv, true, false);
                Cv2.CalcHist(mats, channels2, new Mat(), hist_HSV[2], 1, histSize, rangefsv, true, false);

                int high = 400;
                int width = 512;
                int bin_w = width / 16;//每个bins的宽度  画布的宽度除以bins的个数  //256
                Mat histImage = new Mat(width, high, MatType.CV_8UC3, new Scalar(0, 0, 0)); //定义一个Mat对象，相当于一个画布
                //using (new Window("SRC", src))
                //using (new Window("histImage", histImage))
                //{
                //    Cv2.WaitKey(0);
                //    Cv2.DestroyWindow("SRC");
                //    Cv2.DestroyWindow("histImage");
                //}

                //归一化，像素值有可能数据量很大，压缩一下。是范围在定义画布的范围内。               
                Cv2.Normalize(hist_HSV[0], hist_HSV[0], 0, histImage.Rows, NormTypes.MinMax, -1, null);
                Cv2.Normalize(hist_HSV[1], hist_HSV[1], 0, histImage.Rows, NormTypes.MinMax, -1, null);
                Cv2.Normalize(hist_HSV[2], hist_HSV[2], 0, histImage.Rows, NormTypes.MinMax, -1, null);

                //绘制直方图
                for (int i = 1; i < 16; i++)//遍历直方图的级数 //256
                {
                    //B 画线，一条线有两个点组成。首先确定每个点的坐标(x,y) .遍历从1开始。0 ~ 1 两个点组成一条线，依次类推。
                    Cv2.Line(histImage, new Point(bin_w * (i - 1), high - Math.Round(hist_HSV[0].At<float>(i - 1))), new Point(bin_w * (i - 1), high - Math.Round(hist_HSV[0].At<float>(i))), new Scalar(255, 0, 0), 1, LineTypes.AntiAlias);

                    //G
                    Cv2.Line(histImage, new Point(bin_w * (i - 1), high - Math.Round(hist_HSV[1].At<float>(i - 1))), new Point(bin_w * (i - 1), high - Math.Round(hist_HSV[1].At<float>(i))), new Scalar(0, 255, 0), 1, LineTypes.AntiAlias);

                    //R
                    Cv2.Line(histImage, new Point(bin_w * (i - 1), high - Math.Round(hist_HSV[2].At<float>(i - 1))), new Point(bin_w * (i - 1), high - Math.Round(hist_HSV[2].At<float>(i))), new Scalar(0, 0, 255), 1, LineTypes.AntiAlias);

                }
                if (showImage == true)
                {
                    using (new Window("SRC", src))   //WindowMode.Normal
                    using (new Window("histImage", histImage))
                    {
                        Cv2.WaitKey(0);
                        Cv2.DestroyWindow("SRC");
                        Cv2.DestroyWindow("histImage");
                    }
                }

                return hist_HSV;

            }

        }
    }
}
