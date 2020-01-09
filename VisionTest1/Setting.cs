using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;


namespace VisionTest1
{   
    public static class Images
    {
        public const string picFromCamera = "D:/TestImages/test.bmp";
        public const string pictureSRC = "D:/TestImages/put.bmp";
        public const string pictureDST = "D:/TestImages/pictureDST.bmp";

        public const string picSWS = "D:/TestImages/SWS.bmp";
        public const string picSWS_F1 = "D:/TestImages/SWSF1.bmp";
        public const string picSWS_F2 = "D:/TestImages/SWSF2.bmp";
        public const string picSWS_F3 = "D:/TestImages/SWSF3.bmp";
        public const string picSWS_F4 = "D:/TestImages/SWSF4.bmp";
        public const string picROIRef = "D:/TestImages/RefROIALL.bmp";
        public const string picSetRef = "D:/TestImages/RefSet.bmp";
        public const string picPlusRef = "D:/TestImages/RefPlus.bmp";
        public const string picMainRef = "D:/TestImages/RefHauptschalter.bmp";
        public const string picThumbWheel= "D:/TestImages/ThumbWheel.bmp";


        public const string picTest = "D:/TestImages/TestSet.png";
    }

    public static class GVar
    {
        //show image or not for debug purpose
        public static bool showImageMatchTemplate = false;
        public static bool showImageBlob = false;
        public static bool showImageHist = false;
        public static bool debugVision = false;
        public static bool imageFlip = false;

        public static float[] fHSV = { 0.0f, 0.0f, 0.0f };
        public static double[] G_CompareHist = { 0, 0, 0 };
        //GRA_Tip_Setzen   (Set key)
        //public static float matchRateSet = 0.0f; 
        //public static float[] fHSVSet = { 0.0f, 0.0f, 0.0f };        
        //public static double[] G_CompareHistSet = { 0, 0, 0 };
        //public static int iBlobsSet = 0;


        //other keys
        //public static float matchRatePlus = 0.0f;       //GRA_Tip_Hoch
        //public static float matchRateMinus = 0.0f;       //GRA_Tip_Runter
        //public static float matchRateMain = 0.0f;       //GRA_Hauptschalter
        //public static float matchRateReset = 0.0f;      //GRA_Tip_Rücksetzen
        //public static float matchRateThumbWheel = 0.0f;      //FAS_Menu_Thumbwheel

        //public static float[] fHSVPlus = { 0.0f, 0.0f, 0.0f };
        //public static float[] fHSVMain = { 0.0f, 0.0f, 0.0f };

        //public static int iBlobsPlus = 0;
        //public static int iBlobsMain = 0;

    }

    public static class areaROI
    {
        public const int x = 2200;
        public const int y = 1300;
        public const int width = 1200;
        public const int height = 1100;
    }
    public static class areaSet
    {
        public const int x = 2350;
        public const int y = 1800;
        public const int width = 200;
        public const int height = 150;
    }

    public static class areaThumbWheel
    {
        public const int x = 3000;
        public const int y = 1950;
        public const int width = 250;
        public const int height = 150;
    }    
            
    public static class areaVerstellung
    {
        public const int x = 2600;
        public const int y = 1700;
        public const int width = 200;
        public const int height = 200;
    }

    public static class areaRes
    {
        public const int x = 2900;
        public const int y = 3150;
        public const int width = 250;
        public const int height = 150;
    }
    public static class areaMain
    {
        public const int x = 3000;
        public const int y = 1350;
        public const int width = 250;
        public const int height = 200;
    }
    public static class areaPlus
    {
        public const int x = 2650;
        public const int y = 1550;
        public const int width = 150;
        public const int height = 150;
    }



    public partial class IMProcess
    {
        Rect RROI = new Rect(areaROI.x, areaROI.y, areaROI.width, areaROI.height);
        Rect RMain = new Rect(areaMain.x, areaMain.y, areaMain.width, areaMain.height);
        Rect RPlus = new Rect(areaPlus.x, areaPlus.y, areaPlus.width, areaPlus.height);
        Rect RSet = new Rect(areaSet.x, areaSet.y, areaSet.width, areaSet.height);
        Rect RThumbWheel = new Rect(areaThumbWheel.x, areaThumbWheel.y, areaThumbWheel.width, areaThumbWheel.height);

        Mat ImageOri;
        Mat ImageOri_F1;
        Mat ImageOri_F2;
        Mat ImageOri_F3;
        Mat ImageROI;
        Mat ImageSet;
        Mat ImagePlus;
        Mat ImageThumbWheel;
        Mat ImageMain;
        Mat ImageMain_F1;
        Mat ImageMain_F2;
        Mat ImageMain_F3;
        Mat ImageSetRef;
        Mat ImagePlusRef;
        Mat ImageMainRef;
        Mat ImageThumbWheelRef;

        public void SetMat()
        {
            if (ImageOri == null)
            {
                ImageOri = new Mat(Images.picSWS, ImreadModes.Color);
            }
            if (ImageROI == null)
            {
                ImageROI = new Mat(ImageOri, RROI);
            }
            ImageSet = new Mat(ImageOri, RSet);
            ImagePlus = new Mat(ImageOri, RPlus);
            ImageMain = new Mat(ImageOri, RMain);
        }

        public void SetMatF1()
        {
            ImageOri_F1 = new Mat(Images.picSWS_F1, ImreadModes.Color);
            ImageMain_F1 = new Mat(ImageOri_F1, RMain);
        }

        public void SetMatF2()
        {
            ImageOri_F2 = new Mat(Images.picSWS_F2, ImreadModes.Color);
            ImageMain_F2 = new Mat(ImageOri_F2, RMain);
        }

        public void SetMatRefFromFile()
        {
            if (ImageOri == null)
            {
                ImageOri = new Mat(Images.picSWS, ImreadModes.Color);
            }
            if (ImageROI == null)
            {
                ImageROI = new Mat(ImageOri, RROI);
            }
            ImageSetRef = new Mat(Images.picSetRef, ImreadModes.Color);
            ImagePlusRef = new Mat(Images.picPlusRef, ImreadModes.Color);
            ImageMainRef = new Mat(Images.picMainRef, ImreadModes.Color);
        }
    }


}
