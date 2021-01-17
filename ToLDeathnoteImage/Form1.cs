using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

using Hooking;

namespace ToLDeathnoteImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Text = Application.ProductName + " v" + Application.ProductVersion.Remove(Application.ProductVersion.LastIndexOf('.')) + " by " + Application.CompanyName;

            doubleClickTime = GetDoubleClickTime();

            #region Deathnote Borders
            deathnote.x = 610;
            deathnote.y =  79;
            deathnote.width = 1310 - deathnote.x; // 700 px
            deathnote.height = 979 - deathnote.y; // 900 px

            deathnote.exitButtonWidth = deathnote.x + deathnote.width - 1286;
            deathnote.exitButtonHeight = 109 - deathnote.y;

            deathnote.clearButtonX = 959;
            deathnote.clearButtonY = 1002;
            deathnote.clearButtonYesX = 1061;
            deathnote.clearButtonYesY = 581;

            deathnote.brushButtonX = 1197;
            deathnote.brushButtonY = 1003;
            deathnote.brushSizeOneX = 1336;
            deathnote.brushSizeOneY = 730;
            deathnote.brushSizeTwoX = 1336;
            deathnote.brushSizeTwoY = 682;
            deathnote.brushSizeThreeX = 1336;
            deathnote.brushSizeThreeY = 632;
            deathnote.brushSizeFourX = 1336;
            deathnote.brushSizeFourY = 589;
            deathnote.brushSizeFiveX = 1336;
            deathnote.brushSizeFiveY = 539;
            #endregion

            #region Deathnote Colors
            deathnoteColors = new List<DeathnoteColor>();

            deathnoteColors.Add(new DeathnoteColor());
            deathnoteColors[(int)DeathnoteColors.TRANSPARENT].x = 0;
            deathnoteColors[(int)DeathnoteColors.TRANSPARENT].y = 0;
            deathnoteColors[(int)DeathnoteColors.TRANSPARENT].r = 20000;
            deathnoteColors[(int)DeathnoteColors.TRANSPARENT].g = 20000;
            deathnoteColors[(int)DeathnoteColors.TRANSPARENT].b = 20000;

            deathnoteColors.Add(new DeathnoteColor());
            deathnoteColors[(int)DeathnoteColors.BLACK].x = 583;
            deathnoteColors[(int)DeathnoteColors.BLACK].y = 375;
            deathnoteColors[(int)DeathnoteColors.BLACK].r = 0;
            deathnoteColors[(int)DeathnoteColors.BLACK].g = 0;
            deathnoteColors[(int)DeathnoteColors.BLACK].b = 0;

            deathnoteColors.Add(new DeathnoteColor());
            deathnoteColors[(int)DeathnoteColors.WHITE].x = 583;
            deathnoteColors[(int)DeathnoteColors.WHITE].y = 420;
            deathnoteColors[(int)DeathnoteColors.WHITE].r = 177;
            deathnoteColors[(int)DeathnoteColors.WHITE].g = 177;
            deathnoteColors[(int)DeathnoteColors.WHITE].b = 177;

            deathnoteColors.Add(new DeathnoteColor());
            deathnoteColors[(int)DeathnoteColors.RED].x = 583;
            deathnoteColors[(int)DeathnoteColors.RED].y = 465;
            deathnoteColors[(int)DeathnoteColors.RED].r = 100;
            deathnoteColors[(int)DeathnoteColors.RED].g = 0;
            deathnoteColors[(int)DeathnoteColors.RED].b = 0;

            deathnoteColors.Add(new DeathnoteColor());
            deathnoteColors[(int)DeathnoteColors.BLUE].x = 583;
            deathnoteColors[(int)DeathnoteColors.BLUE].y = 510;
            deathnoteColors[(int)DeathnoteColors.BLUE].r = 0;
            deathnoteColors[(int)DeathnoteColors.BLUE].g = 101;
            deathnoteColors[(int)DeathnoteColors.BLUE].b = 160;

            deathnoteColors.Add(new DeathnoteColor());
            deathnoteColors[(int)DeathnoteColors.PURPLE].x = 583;
            deathnoteColors[(int)DeathnoteColors.PURPLE].y = 555;
            deathnoteColors[(int)DeathnoteColors.PURPLE].r = 99;
            deathnoteColors[(int)DeathnoteColors.PURPLE].g = 0;
            deathnoteColors[(int)DeathnoteColors.PURPLE].b = 124;

            deathnoteColors.Add(new DeathnoteColor());
            deathnoteColors[(int)DeathnoteColors.GREEN].x = 583;
            deathnoteColors[(int)DeathnoteColors.GREEN].y = 600;
            deathnoteColors[(int)DeathnoteColors.GREEN].r = 0;
            deathnoteColors[(int)DeathnoteColors.GREEN].g = 123;
            deathnoteColors[(int)DeathnoteColors.GREEN].b = 0;

            deathnoteColors.Add(new DeathnoteColor());
            deathnoteColors[(int)DeathnoteColors.YELLOW].x = 583;
            deathnoteColors[(int)DeathnoteColors.YELLOW].y = 645;
            deathnoteColors[(int)DeathnoteColors.YELLOW].r = 163;
            deathnoteColors[(int)DeathnoteColors.YELLOW].g = 152;
            deathnoteColors[(int)DeathnoteColors.YELLOW].b = 0;

            deathnoteColors.Add(new DeathnoteColor());
            deathnoteColors[(int)DeathnoteColors.ORANGE].x = 583;
            deathnoteColors[(int)DeathnoteColors.ORANGE].y = 690;
            deathnoteColors[(int)DeathnoteColors.ORANGE].r = 194;
            deathnoteColors[(int)DeathnoteColors.ORANGE].g = 106;
            deathnoteColors[(int)DeathnoteColors.ORANGE].b = 0;
            #endregion

            convertedImageColorsBrush1 = new DeathnoteColors[deathnote.width, deathnote.height];
            convertedImageColorsBrush2 = new DeathnoteColors[(int)(deathnote.width/2), (int)(deathnote.height/2)];
            convertedImageColorsBrush3 = new DeathnoteColors[(int)(deathnote.width/3), (int)(deathnote.height/3)];
            convertedImageColorsBrush4 = new DeathnoteColors[(int)(deathnote.width/4), (int)(deathnote.height/4)];
            selectedColor = DeathnoteColors.TRANSPARENT;

            convertedImageColorsBrush1Drawn = new bool[deathnote.width, deathnote.height];
            convertedImageColorsBrush2Drawn = new bool[(int)(deathnote.width / 2), (int)(deathnote.height / 2)];
            convertedImageColorsBrush3Drawn = new bool[(int)(deathnote.width / 3), (int)(deathnote.height / 3)];
            convertedImageColorsBrush4Drawn = new bool[(int)(deathnote.width / 4), (int)(deathnote.height / 4)];

            drawX = 0;
            drawY = 0;

            usedColors = new List<DeathnoteColors>();
            clb_colors.Items.Clear();
            usedColorIndex = 0;

            foreach (DeathnoteColors c in (DeathnoteColors[])Enum.GetValues(typeof(DeathnoteColors)))
            {
                if (c != DeathnoteColors.NODRAW)
                {
                    clb_colors.Items.Add(c.ToString());
                    clb_colors.SetItemChecked(clb_colors.Items.Count - 1, true);
                }
            }
        }

        #region Fields

        bool updateNeeded;

        int drawX, drawY;

        uint doubleClickTime;
        Deathnote deathnote;
        List<DeathnoteColor> deathnoteColors;
        DeathnoteColors selectedColor;
        List<DeathnoteColors> usedColors;
        int usedColorIndex;

        DeathnoteColors[,] convertedImageColorsBrush1;
        DeathnoteColors[,] convertedImageColorsBrush2;
        DeathnoteColors[,] convertedImageColorsBrush3;
        DeathnoteColors[,] convertedImageColorsBrush4;

        bool[,] convertedImageColorsBrush1Drawn;
        bool[,] convertedImageColorsBrush2Drawn;
        bool[,] convertedImageColorsBrush3Drawn;
        bool[,] convertedImageColorsBrush4Drawn;

        Bitmap convertedImageBrush1;
        Bitmap convertedImageBrush2;
        Bitmap convertedImageBrush3;
        Bitmap convertedImageBrush4;

        Image resizedImage;

        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };

        #endregion

        #region Enums & Structs

        public enum MouseActionAdresses
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }
        
        public enum DeathnoteColors
        {
            NODRAW = -1,
            TRANSPARENT = 0,
            BLACK,
            WHITE,
            RED,
            BLUE,
            PURPLE,
            GREEN,
            YELLOW,
            ORANGE
        }

        public class DeathnoteColor
        {
            public int x;
            public int y;
            public int r;
            public int g;
            public int b;
        }

        public struct Deathnote
        {
            public int x;
            public int y;
            public int width;
            public int height;

            public int exitButtonWidth;
            public int exitButtonHeight;

            public int clearButtonX;
            public int clearButtonY;
            public int clearButtonYesX;
            public int clearButtonYesY;

            public int brushButtonX;
            public int brushButtonY;

            public int brushSizeOneX;
            public int brushSizeOneY;

            public int brushSizeTwoX;
            public int brushSizeTwoY;

            public int brushSizeThreeX;
            public int brushSizeThreeY;

            public int brushSizeFourX;
            public int brushSizeFourY;

            public int brushSizeFiveX;
            public int brushSizeFiveY;
        }
        
        #endregion

        #region Dll Imports

        [DllImport("user32.dll")]
        private static extern uint GetDoubleClickTime();

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern System.Boolean SetForegroundWindow(System.IntPtr hwnd);

        #endregion

        #region Methods

        public System.Boolean MakeWindowActive(System.String windowTitle)
        {
            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();

            foreach (System.Diagnostics.Process p in processList)
            {
                if (p.MainWindowTitle == windowTitle)
                {
                    SetForegroundWindow(p.MainWindowHandle);
                    return true;
                }
            }
            return false;
        }

        void LClick(System.Int32 x, System.Int32 y)
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y);
            Wait(50);
            mouse_event((System.Int32)(MouseActionAdresses.LEFTDOWN), 0, 0, 0, 0);
            Wait(20);
            mouse_event((System.Int32)(MouseActionAdresses.LEFTUP), 0, 0, 0, 0);
            Wait(30);

        }

        void LClickFast(System.Int32 x, System.Int32 y)
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y);
            Wait(20);
            mouse_event((System.Int32)(MouseActionAdresses.LEFTDOWN), 0, 0, 0, 0);
            Wait(10);
            mouse_event((System.Int32)(MouseActionAdresses.LEFTUP), 0, 0, 0, 0);
            Wait(40);
        }

        #region Wait()
        bool go = false;
        int ms;
        /// <summary>
        /// Method for sleep without program stop
        /// </summary>
        /// <param name="ms">Time to wait in milliseconds</param>
        private void Wait(int milliseconds)
        {
            ms = milliseconds;
            go = false;
            System.Threading.Thread T = new System.Threading.Thread(new System.Threading.ThreadStart(Thread_Wait));
            T.Start();
            do
            {
                System.Windows.Forms.Application.DoEvents();
            } while (go == false);
        }

        /// <summary>
        /// Wait Thread, called by Wait(int milliseconds)
        /// </summary>
        private void Thread_Wait()
        {
            Thread.Sleep(ms);
            go = true;
        }
        #endregion

        /// <summary>
        /// Resize the image to the specified width and height stretching the image.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        static Bitmap ResizeImageStretch(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        /// <summary>
        /// Resize the image to the specified width and height, preserving aspect ratio.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        static Image ResizeImageFixedSize(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                              PixelFormat.Format24bppRgb);
            bmPhoto.MakeTransparent();
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                             imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Transparent);            
            grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public bool IsValidDrawPoint (int x, int y, int brushSize, bool smoothing)
        {
            bool valid = true;
            if (brushSize == 1)
            {
                // No Drawing over exit button
                if (!smoothing && brushSize * x >= deathnote.width - deathnote.exitButtonWidth && brushSize * y <= deathnote.exitButtonHeight)
                    valid = false;

                if (convertedImageColorsBrush1Drawn[x, y])
                    valid = false;

                // Ajacent points are the same
                if (!smoothing && cb_border_points_only.Checked
                    && (x == 0 || convertedImageColorsBrush1[x, y] == convertedImageColorsBrush1[x - 1, y] || convertedImageColorsBrush1[x - 1, y] == DeathnoteColors.NODRAW)
                    && (x == convertedImageColorsBrush1.GetLength(0) - 1 || convertedImageColorsBrush1[x, y] == convertedImageColorsBrush1[x + 1, y] || convertedImageColorsBrush1[x + 1, y] == DeathnoteColors.NODRAW)
                    && (y == 0 || convertedImageColorsBrush1[x, y] == convertedImageColorsBrush1[x, y - 1] || convertedImageColorsBrush1[x, y - 1] == DeathnoteColors.NODRAW)
                    && (y == convertedImageColorsBrush1.GetLength(1) - 1 || convertedImageColorsBrush1[x, y] == convertedImageColorsBrush1[x, y + 1] || convertedImageColorsBrush1[x, y + 1] == DeathnoteColors.NODRAW))
                    valid = false;
            }
            else if (brushSize == 2)
            {
                // No Drawing over exit button
                if (!smoothing && brushSize * x >= deathnote.width - deathnote.exitButtonWidth && brushSize * y <= deathnote.exitButtonHeight)
                    valid = false;

                if (convertedImageColorsBrush2Drawn[x, y])
                    valid = false;

                // Ajacent points are the same
                if (!smoothing && cb_border_points_only.Checked
                    && (x == 0 || convertedImageColorsBrush2[x, y] == convertedImageColorsBrush2[x - 1, y] || convertedImageColorsBrush2[x - 1, y] == DeathnoteColors.NODRAW)
                    && (x == convertedImageColorsBrush2.GetLength(0) - 1 || convertedImageColorsBrush2[x, y] == convertedImageColorsBrush2[x + 1, y] || convertedImageColorsBrush2[x + 1, y] == DeathnoteColors.NODRAW)
                    && (y == 0 || convertedImageColorsBrush2[x, y] == convertedImageColorsBrush2[x, y - 1] || convertedImageColorsBrush2[x, y - 1] == DeathnoteColors.NODRAW)
                    && (y == convertedImageColorsBrush2.GetLength(1) - 1 || convertedImageColorsBrush2[x, y] == convertedImageColorsBrush2[x, y + 1] || convertedImageColorsBrush2[x, y + 1] == DeathnoteColors.NODRAW))
                    valid = false;
            }
            else if (brushSize == 3)
            {
                // No Drawing over exit button
                if (!smoothing && brushSize * x >= deathnote.width - deathnote.exitButtonWidth && brushSize * y <= deathnote.exitButtonHeight)
                    valid = false;

                if (convertedImageColorsBrush3Drawn[x, y])
                    valid = false;

                // Ajacent points are the same
                if (!smoothing && cb_border_points_only.Checked
                    && (x == 0 || convertedImageColorsBrush3[x, y] == convertedImageColorsBrush3[x - 1, y] || convertedImageColorsBrush3[x - 1, y] == DeathnoteColors.NODRAW)
                    && (x == convertedImageColorsBrush3.GetLength(0) - 1 || convertedImageColorsBrush3[x, y] == convertedImageColorsBrush3[x + 1, y] || convertedImageColorsBrush3[x + 1, y] == DeathnoteColors.NODRAW)
                    && (y == 0 || convertedImageColorsBrush3[x, y] == convertedImageColorsBrush3[x, y - 1] || convertedImageColorsBrush3[x, y - 1] == DeathnoteColors.NODRAW)
                    && (y == convertedImageColorsBrush3.GetLength(1) - 1 || convertedImageColorsBrush3[x, y] == convertedImageColorsBrush3[x, y + 1] || convertedImageColorsBrush3[x, y + 1] == DeathnoteColors.NODRAW))
                    valid = false;
            }
            else if (brushSize == 4)
            {
                // No Drawing over exit button
                if (!smoothing && brushSize * x >= deathnote.width - deathnote.exitButtonWidth && brushSize * y <= deathnote.exitButtonHeight)
                    valid = false;

                if (convertedImageColorsBrush4Drawn[x, y])
                    valid = false;

                // Ajacent points are the same
                if (!smoothing && cb_border_points_only.Checked
                    && (x == 0 || convertedImageColorsBrush4[x, y] == convertedImageColorsBrush4[x - 1, y] || convertedImageColorsBrush4[x - 1, y] == DeathnoteColors.NODRAW)
                    && (x == convertedImageColorsBrush4.GetLength(0) - 1 || convertedImageColorsBrush4[x, y] == convertedImageColorsBrush4[x + 1, y] || convertedImageColorsBrush4[x + 1, y] == DeathnoteColors.NODRAW)
                    && (y == 0 || convertedImageColorsBrush4[x, y] == convertedImageColorsBrush4[x, y - 1] || convertedImageColorsBrush4[x, y - 1] == DeathnoteColors.NODRAW)
                    && (y == convertedImageColorsBrush4.GetLength(1) - 1 || convertedImageColorsBrush4[x, y] == convertedImageColorsBrush4[x, y + 1] || convertedImageColorsBrush4[x, y + 1] == DeathnoteColors.NODRAW))
                    valid = false;
            }

            return valid;
        }

        public Tuple<int, int> SearchAdjacentPoint (int startX, int startY, int brushSize, bool smoothing)
        {
            int retX = -1, retY = -1, x, y;
            DeathnoteColors startColor = DeathnoteColors.BLACK;
            if (brushSize == 1)
                startColor = convertedImageColorsBrush1[startX, startY];
            else if (brushSize == 2)
                startColor = convertedImageColorsBrush2[startX, startY];
            else if (brushSize == 3)
                startColor = convertedImageColorsBrush3[startX, startY];
            else if (brushSize == 4)
                startColor = convertedImageColorsBrush4[startX, startY];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 1 && j == 1)
                        continue;

                    x = startX - 1 + i;
                    y = startY - 1 + j;

                    if (brushSize == 1)
                    {
                        if (x < 0 || x > convertedImageColorsBrush1.GetLength(0) - 1
                            || y < 0 || y > convertedImageColorsBrush1.GetLength(1) - 1)
                            continue;

                        if (convertedImageColorsBrush1[x, y] != startColor)
                            continue;

                        if (IsValidDrawPoint(x, y, brushSize, smoothing))
                        {
                            retX = x;
                            retY = y;
                            break;
                        }
                    }
                    else if (brushSize == 2)
                    {
                        if (x < 0 || x > convertedImageColorsBrush2.GetLength(0) - 1
                            || y < 0 || y > convertedImageColorsBrush2.GetLength(1) - 1)
                            continue;

                        if (convertedImageColorsBrush2[x, y] != startColor)
                            continue;

                        if (IsValidDrawPoint(x, y, brushSize, smoothing))
                        {
                            retX = x;
                            retY = y;
                            break;
                        }
                    }
                    else if (brushSize == 3)
                    {
                        if (x < 0 || x > convertedImageColorsBrush3.GetLength(0) - 1
                            || y < 0 || y > convertedImageColorsBrush3.GetLength(1) - 1)
                            continue;

                        if (convertedImageColorsBrush3[x, y] != startColor)
                            continue;

                        if (IsValidDrawPoint(x, y, brushSize, smoothing))
                        {
                            retX = x;
                            retY = y;
                            break;
                        }
                    }
                    else if (brushSize == 4)
                    {
                        if (x < 0 || x > convertedImageColorsBrush4.GetLength(0) - 1
                            || y < 0 || y > convertedImageColorsBrush4.GetLength(1) - 1)
                            continue;

                        if (convertedImageColorsBrush4[x, y] != startColor)
                            continue;

                        if (IsValidDrawPoint(x, y, brushSize, smoothing))
                        {
                            retX = x;
                            retY = y;
                            break;
                        }
                    }
                }
                if (retX != -1 && retY != -1)
                    break;
            }

            return Tuple.Create(retX, retY);
        }

        void ResetDrawPosition ()
        {
            selectedColor = DeathnoteColors.TRANSPARENT;
            drawX = 0;
            drawY = 0;
            usedColorIndex = 0;

            Array.Clear(convertedImageColorsBrush1Drawn, 0, convertedImageColorsBrush1Drawn.Length);
            Array.Clear(convertedImageColorsBrush2Drawn, 0, convertedImageColorsBrush2Drawn.Length);
            Array.Clear(convertedImageColorsBrush3Drawn, 0, convertedImageColorsBrush3Drawn.Length);
            Array.Clear(convertedImageColorsBrush4Drawn, 0, convertedImageColorsBrush4Drawn.Length);

            tb_drawX.Text = (drawX + 1).ToString();
            tb_drawY.Text = (drawY + 1).ToString();

            trackBar_brushsize.Enabled = true;
        }

        bool IsPointToSmooth(int startX, int startY, int brushSize)
        {
            if (!IsValidDrawPoint(startX, startY, brushSize, false))
                return false;

            int x, y;
            int transparentPoints = 0;

            // Transparent neigbor points
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 1 && j == 1)
                        continue;

                    x = startX - 1 + i;
                    y = startY - 1 + j;

                    if (brushSize == 1)
                    {
                        if (x < 0 || x > convertedImageColorsBrush1.GetLength(0) - 1
                            || y < 0 || y > convertedImageColorsBrush1.GetLength(1) - 1)
                            continue;

                        if (convertedImageColorsBrush1[x, y] == DeathnoteColors.TRANSPARENT)
                            transparentPoints += 1;
                    }
                    else if (brushSize == 2)
                    {
                        if (x < 0 || x > convertedImageColorsBrush2.GetLength(0) - 1
                            || y < 0 || y > convertedImageColorsBrush2.GetLength(1) - 1)
                            continue;

                        if (convertedImageColorsBrush2[x, y] == DeathnoteColors.TRANSPARENT)
                            transparentPoints += 1;
                    }
                    else if (brushSize == 3)
                    {
                        if (x < 0 || x > convertedImageColorsBrush3.GetLength(0) - 1
                            || y < 0 || y > convertedImageColorsBrush3.GetLength(1) - 1)
                            continue;

                        if (convertedImageColorsBrush3[x, y] == DeathnoteColors.TRANSPARENT)
                            transparentPoints += 1;
                    }
                    else if (brushSize == 4)
                    {
                        if (x < 0 || x > convertedImageColorsBrush4.GetLength(0) - 1
                            || y < 0 || y > convertedImageColorsBrush4.GetLength(1) - 1)
                            continue;

                        if (convertedImageColorsBrush4[x, y] == DeathnoteColors.TRANSPARENT)
                            transparentPoints += 1;
                    }
                }
            }

            Tuple<int, int> newPoint = null;
            int xAdj = startX;
            int yAdj = startY;
            List<Tuple<int, int>> adjacentPoints = new List<Tuple<int, int>>();

            do
            {
                newPoint = SearchAdjacentPoint(xAdj, yAdj, brushSize, true);
                if (newPoint.Item1 != -1 && newPoint.Item2 != -1)
                {
                    xAdj = newPoint.Item1;
                    yAdj = newPoint.Item2;
                    adjacentPoints.Add(new Tuple<int, int>(xAdj, yAdj));
                    if (brushSize == 1)
                        convertedImageColorsBrush1Drawn[xAdj, yAdj] = true;
                    if (brushSize == 2)
                        convertedImageColorsBrush2Drawn[xAdj, yAdj] = true;
                    if (brushSize == 3)
                        convertedImageColorsBrush3Drawn[xAdj, yAdj] = true;
                    if (brushSize == 4)
                        convertedImageColorsBrush4Drawn[xAdj, yAdj] = true;
                }
                if (adjacentPoints.Count >= trackBar_smooth_same_color.Value)
                    break;
            } while (newPoint.Item1 != -1 && newPoint.Item2 != -1);

            for (int i = 0; i < adjacentPoints.Count; i++)
            {
                if (brushSize == 1)
                    convertedImageColorsBrush1Drawn[adjacentPoints[i].Item1, adjacentPoints[i].Item2] = false;
                if (brushSize == 2)
                    convertedImageColorsBrush2Drawn[adjacentPoints[i].Item1, adjacentPoints[i].Item2] = false;
                if (brushSize == 3)
                    convertedImageColorsBrush3Drawn[adjacentPoints[i].Item1, adjacentPoints[i].Item2] = false;
                if (brushSize == 4)
                    convertedImageColorsBrush4Drawn[adjacentPoints[i].Item1, adjacentPoints[i].Item2] = false;
            }

            if (transparentPoints >= -trackBar_smooth_transparent.Value)
                return true;
            else if (adjacentPoints.Count < trackBar_smooth_same_color.Value)
                return true;
            else
                return false;
        }

        void CreateImages(Image resizedImage)
        {
            usedColors.Clear();
            foreach (string item in clb_colors.CheckedItems)
            {
                for (int c = 0; c < Enum.GetNames(typeof(DeathnoteColors)).Length; c++)
                {
                    if (((DeathnoteColors)c).ToString() == item)
                    {
                        usedColors.Add((DeathnoteColors)c);
                        break;
                    }
                }
            }

            #region Brush 1 Image
            convertedImageBrush1 = new Bitmap(deathnote.width, deathnote.height);
            convertedImageBrush1.MakeTransparent();
            for (int x = 0; x < resizedImage.Width; x++)
            {
                for (int y = 0; y < resizedImage.Height; y++)
                {
                    Color pixel = ((Bitmap)resizedImage).GetPixel(x, y);
                    int closestColor = 0;
                    int closest = 500;
                    int colorDistance;

                    if (!(pixel.R == 0 && pixel.G == 0 && pixel.B == 0 && pixel.A == 0))
                    {
                        for (int c = 1; c < usedColors.Count; c++)
                        {
                            colorDistance = Math.Abs(pixel.R - deathnoteColors[(int)usedColors[c]].r) + Math.Abs(pixel.G - deathnoteColors[(int)usedColors[c]].g) + Math.Abs(pixel.B - deathnoteColors[(int)usedColors[c]].b);
                            if (colorDistance < closest)
                            {
                                closest = colorDistance;
                                closestColor = (int)usedColors[c];
                            }
                        }

                        if (closestColor > 0)
                            convertedImageBrush1.SetPixel(x, y, Color.FromArgb(deathnoteColors[closestColor].r, deathnoteColors[closestColor].g, deathnoteColors[closestColor].b));
                        else
                            convertedImageBrush1.SetPixel(x, y, Color.Transparent);

                        convertedImageColorsBrush1[x, y] = (DeathnoteColors)closestColor;
                    }
                    else
                        convertedImageColorsBrush1[x, y] = DeathnoteColors.TRANSPARENT;
                }
            }
            Array.Clear(convertedImageColorsBrush1Drawn, 0, convertedImageColorsBrush1Drawn.Length);

            if (cb_smoothing.Checked)
            {
                List<Tuple<int, int>> makeTransparent = new List<Tuple<int, int>>();
                for (int x = 0; x < convertedImageBrush1.Width; x++)
                {
                    for (int y = 0; y < convertedImageBrush1.Height; y++)
                    {
                        if (IsPointToSmooth(x, y, 1))
                        {
                            makeTransparent.Add(new Tuple<int, int>(x, y));
                        }
                    }
                }
                for (int i = 0; i < makeTransparent.Count; i++)
                {
                    convertedImageBrush1.SetPixel(makeTransparent[i].Item1, makeTransparent[i].Item2, Color.Transparent);
                    convertedImageColorsBrush1[makeTransparent[i].Item1, makeTransparent[i].Item2] = DeathnoteColors.NODRAW;
                }
            }
            #endregion

            #region Brush 2 Image
            convertedImageBrush2 = new Bitmap((int)(deathnote.width / 2), (int)(deathnote.height / 2));
            convertedImageBrush2.MakeTransparent();
            for (int x = 0; x < resizedImage.Width; x++)
            {
                for (int y = 0; y < resizedImage.Height; y++)
                {
                    if (x % 2 == 1 && y % 2 == 1)
                    {
                        Color pixel1 = ((Bitmap)resizedImage).GetPixel(x - 1, y - 1);
                        Color pixel2 = ((Bitmap)resizedImage).GetPixel(x - 1, y);
                        Color pixel3 = ((Bitmap)resizedImage).GetPixel(x, y - 1);
                        Color pixel4 = ((Bitmap)resizedImage).GetPixel(x, y);
                        int closestColor = 0;
                        int closest = 500;
                        int colorDistance;
                        int r = 0, g = 0, b = 0, a = 0, div = 0;
                        if (!(pixel1.R == 0 && pixel1.G == 0 && pixel1.B == 0 && pixel1.A == 0) &&
                            !(pixel2.R == 0 && pixel2.G == 0 && pixel2.B == 0 && pixel2.A == 0) &&
                            !(pixel3.R == 0 && pixel3.G == 0 && pixel3.B == 0 && pixel3.A == 0) &&
                            !(pixel4.R == 0 && pixel4.G == 0 && pixel4.B == 0 && pixel4.A == 0))
                        {
                            if (pixel1.A != 0)
                            {
                                r += pixel1.R;
                                g += pixel1.G;
                                b += pixel1.B;
                                a += pixel1.A;
                                div++;
                            }
                            if (pixel2.A != 0)
                            {
                                r += pixel2.R;
                                g += pixel2.G;
                                b += pixel2.B;
                                a += pixel2.A;
                                div++;
                            }
                            if (pixel3.A != 0)
                            {
                                r += pixel3.R;
                                g += pixel3.G;
                                b += pixel3.B;
                                a += pixel3.A;
                                div++;
                            }
                            if (pixel4.A != 0)
                            {
                                r += pixel4.R;
                                g += pixel4.G;
                                b += pixel4.B;
                                a += pixel4.A;
                                div++;
                            }

                            r = r / div;
                            g = g / div;
                            b = b / div;
                            a = a / div;

                            for (int c = 1; c < usedColors.Count; c++)
                            {
                                colorDistance = Math.Abs(r - deathnoteColors[(int)usedColors[c]].r) + Math.Abs(g - deathnoteColors[(int)usedColors[c]].g) + Math.Abs(b - deathnoteColors[(int)usedColors[c]].b);
                                if (colorDistance < closest)
                                {
                                    closest = colorDistance;
                                    closestColor = (int)usedColors[c];
                                }
                            }

                            if (closestColor > 0)
                                convertedImageBrush2.SetPixel((int)Math.Ceiling(x / 2.0) - 1, (int)Math.Ceiling(y / 2.0) - 1, Color.FromArgb(deathnoteColors[closestColor].r, deathnoteColors[closestColor].g, deathnoteColors[closestColor].b));
                            else
                                convertedImageBrush2.SetPixel((int)Math.Ceiling(x / 2.0) - 1, (int)Math.Ceiling(y / 2.0) - 1, Color.Transparent);

                            convertedImageColorsBrush2[(int)Math.Ceiling(x / 2.0) - 1, (int)Math.Ceiling(y / 2.0) - 1] = (DeathnoteColors)closestColor;
                        }
                        else
                            convertedImageColorsBrush2[(int)Math.Ceiling(x / 2.0) - 1, (int)Math.Ceiling(y / 2.0) - 1] = DeathnoteColors.TRANSPARENT;
                    }
                }
            }
            Array.Clear(convertedImageColorsBrush2Drawn, 0, convertedImageColorsBrush2Drawn.Length);

            if (cb_smoothing.Checked)
            {
                List<Tuple<int, int>> makeTransparent = new List<Tuple<int, int>>();
                for (int x = 0; x < convertedImageBrush2.Width; x++)
                {
                    for (int y = 0; y < convertedImageBrush2.Height; y++)
                    {
                        if (IsPointToSmooth(x, y, 2))
                        {
                            makeTransparent.Add(new Tuple<int, int>(x, y));
                        }
                    }
                }
                for (int i = 0; i < makeTransparent.Count; i++)
                {
                    convertedImageBrush2.SetPixel(makeTransparent[i].Item1, makeTransparent[i].Item2, Color.Transparent);
                    convertedImageColorsBrush2[makeTransparent[i].Item1, makeTransparent[i].Item2] = DeathnoteColors.NODRAW;
                }
            }
            #endregion

            #region Brush 3 Image
            convertedImageBrush3 = new Bitmap((int)(deathnote.width / 3), (int)(deathnote.height / 3));
            convertedImageBrush3.MakeTransparent();
            for (int x = 0; x < resizedImage.Width; x++)
            {
                for (int y = 0; y < resizedImage.Height; y++)
                {
                    if (x % 3 == 2 && y % 3 == 2)
                    {
                        Color pixel1 = ((Bitmap)resizedImage).GetPixel(x - 2, y - 2);
                        Color pixel2 = ((Bitmap)resizedImage).GetPixel(x - 1, y - 2);
                        Color pixel3 = ((Bitmap)resizedImage).GetPixel(x, y - 2);
                        Color pixel4 = ((Bitmap)resizedImage).GetPixel(x - 2, y - 1);
                        Color pixel5 = ((Bitmap)resizedImage).GetPixel(x - 1, y - 1);
                        Color pixel6 = ((Bitmap)resizedImage).GetPixel(x, y - 1);
                        Color pixel7 = ((Bitmap)resizedImage).GetPixel(x - 2, y);
                        Color pixel8 = ((Bitmap)resizedImage).GetPixel(x - 1, y);
                        Color pixel9 = ((Bitmap)resizedImage).GetPixel(x, y);
                        int closestColor = 0;
                        int closest = 500;
                        int colorDistance;
                        int r = 0, g = 0, b = 0, a = 0, div = 0;
                        if (!(pixel1.R == 0 && pixel1.G == 0 && pixel1.B == 0 && pixel1.A == 0) &&
                            !(pixel2.R == 0 && pixel2.G == 0 && pixel2.B == 0 && pixel2.A == 0) &&
                            !(pixel3.R == 0 && pixel3.G == 0 && pixel3.B == 0 && pixel3.A == 0) &&
                            !(pixel4.R == 0 && pixel4.G == 0 && pixel4.B == 0 && pixel4.A == 0) &&
                            !(pixel5.R == 0 && pixel5.G == 0 && pixel5.B == 0 && pixel5.A == 0) &&
                            !(pixel6.R == 0 && pixel6.G == 0 && pixel6.B == 0 && pixel6.A == 0) &&
                            !(pixel7.R == 0 && pixel7.G == 0 && pixel7.B == 0 && pixel7.A == 0) &&
                            !(pixel8.R == 0 && pixel8.G == 0 && pixel8.B == 0 && pixel8.A == 0) &&
                            !(pixel9.R == 0 && pixel9.G == 0 && pixel9.B == 0 && pixel9.A == 0))
                        {
                            if (pixel1.A != 0)
                            {
                                r += pixel1.R;
                                g += pixel1.G;
                                b += pixel1.B;
                                a += pixel1.A;
                                div++;
                            }
                            if (pixel2.A != 0)
                            {
                                r += pixel2.R;
                                g += pixel2.G;
                                b += pixel2.B;
                                a += pixel2.A;
                                div++;
                            }
                            if (pixel3.A != 0)
                            {
                                r += pixel3.R;
                                g += pixel3.G;
                                b += pixel3.B;
                                a += pixel3.A;
                                div++;
                            }
                            if (pixel4.A != 0)
                            {
                                r += pixel4.R;
                                g += pixel4.G;
                                b += pixel4.B;
                                a += pixel4.A;
                                div++;
                            }
                            if (pixel5.A != 0)
                            {
                                r += pixel5.R;
                                g += pixel5.G;
                                b += pixel5.B;
                                a += pixel5.A;
                                div++;
                            }
                            if (pixel6.A != 0)
                            {
                                r += pixel6.R;
                                g += pixel6.G;
                                b += pixel6.B;
                                a += pixel6.A;
                                div++;
                            }
                            if (pixel7.A != 0)
                            {
                                r += pixel7.R;
                                g += pixel7.G;
                                b += pixel7.B;
                                a += pixel7.A;
                                div++;
                            }
                            if (pixel8.A != 0)
                            {
                                r += pixel8.R;
                                g += pixel8.G;
                                b += pixel8.B;
                                a += pixel8.A;
                                div++;
                            }
                            if (pixel9.A != 0)
                            {
                                r += pixel9.R;
                                g += pixel9.G;
                                b += pixel9.B;
                                a += pixel9.A;
                                div++;
                            }

                            r = r / div;
                            g = g / div;
                            b = b / div;
                            a = a / div;

                            for (int c = 1; c < usedColors.Count; c++)
                            {
                                colorDistance = Math.Abs(r - deathnoteColors[(int)usedColors[c]].r) + Math.Abs(g - deathnoteColors[(int)usedColors[c]].g) + Math.Abs(b - deathnoteColors[(int)usedColors[c]].b);
                                if (colorDistance < closest)
                                {
                                    closest = colorDistance;
                                    closestColor = (int)usedColors[c];
                                }
                            }

                            if (closestColor > 0)
                                convertedImageBrush3.SetPixel((int)Math.Ceiling(x / 3.0) - 1, (int)Math.Ceiling(y / 3.0) - 1, Color.FromArgb(deathnoteColors[closestColor].r, deathnoteColors[closestColor].g, deathnoteColors[closestColor].b));
                            else
                                convertedImageBrush3.SetPixel((int)Math.Ceiling(x / 3.0) - 1, (int)Math.Ceiling(y / 3.0) - 1, Color.Transparent);

                            convertedImageColorsBrush3[(int)Math.Ceiling(x / 3.0) - 1, (int)Math.Ceiling(y / 3.0) - 1] = (DeathnoteColors)closestColor;
                        }
                        else
                            convertedImageColorsBrush3[(int)Math.Ceiling(x / 3.0) - 1, (int)Math.Ceiling(y / 3.0) - 1] = DeathnoteColors.TRANSPARENT;
                    }
                }
            }
            Array.Clear(convertedImageColorsBrush3Drawn, 0, convertedImageColorsBrush3Drawn.Length);

            if (cb_smoothing.Checked)
            {
                List<Tuple<int, int>> makeTransparent = new List<Tuple<int, int>>();
                for (int x = 0; x < convertedImageBrush3.Width; x++)
                {
                    for (int y = 0; y < convertedImageBrush3.Height; y++)
                    {
                        if (IsPointToSmooth(x, y, 3))
                        {
                            makeTransparent.Add(new Tuple<int, int>(x, y));
                        }
                    }
                }
                for (int i = 0; i < makeTransparent.Count; i++)
                {
                    convertedImageBrush3.SetPixel(makeTransparent[i].Item1, makeTransparent[i].Item2, Color.Transparent);
                    convertedImageColorsBrush3[makeTransparent[i].Item1, makeTransparent[i].Item2] = DeathnoteColors.NODRAW;
                }
            }
            #endregion

            #region Brush 4 Image
            convertedImageBrush4 = new Bitmap((int)(deathnote.width / 4), (int)(deathnote.height / 4));
            convertedImageBrush4.MakeTransparent();
            for (int x = 0; x < resizedImage.Width; x++)
            {
                for (int y = 0; y < resizedImage.Height; y++)
                {
                    if (x % 4 == 3 && y % 4 == 3)
                    {
                        Color pixel1 = ((Bitmap)resizedImage).GetPixel(x - 3, y - 3);
                        Color pixel2 = ((Bitmap)resizedImage).GetPixel(x - 2, y - 3);
                        Color pixel3 = ((Bitmap)resizedImage).GetPixel(x - 1, y - 3);
                        Color pixel4 = ((Bitmap)resizedImage).GetPixel(x, y - 3);
                        Color pixel5 = ((Bitmap)resizedImage).GetPixel(x - 3, y - 2);
                        Color pixel6 = ((Bitmap)resizedImage).GetPixel(x - 2, y - 2);
                        Color pixel7 = ((Bitmap)resizedImage).GetPixel(x - 1, y - 2);
                        Color pixel8 = ((Bitmap)resizedImage).GetPixel(x, y - 2);
                        Color pixel9 = ((Bitmap)resizedImage).GetPixel(x - 3, y - 1);
                        Color pixel10 = ((Bitmap)resizedImage).GetPixel(x - 2, y - 1);
                        Color pixel11 = ((Bitmap)resizedImage).GetPixel(x - 1, y - 1);
                        Color pixel12 = ((Bitmap)resizedImage).GetPixel(x, y - 1);
                        Color pixel13 = ((Bitmap)resizedImage).GetPixel(x - 3, y);
                        Color pixel14 = ((Bitmap)resizedImage).GetPixel(x - 2, y);
                        Color pixel15 = ((Bitmap)resizedImage).GetPixel(x - 1, y);
                        Color pixel16 = ((Bitmap)resizedImage).GetPixel(x, y);
                        int closestColor = 0;
                        int closest = 500;
                        int colorDistance;
                        int r = 0, g = 0, b = 0, a = 0, div = 0;
                        if (!(pixel1.R == 0 && pixel1.G == 0 && pixel1.B == 0 && pixel1.A == 0) &&
                            !(pixel2.R == 0 && pixel2.G == 0 && pixel2.B == 0 && pixel2.A == 0) &&
                            !(pixel3.R == 0 && pixel3.G == 0 && pixel3.B == 0 && pixel3.A == 0) &&
                            !(pixel4.R == 0 && pixel4.G == 0 && pixel4.B == 0 && pixel4.A == 0) &&
                            !(pixel5.R == 0 && pixel5.G == 0 && pixel5.B == 0 && pixel5.A == 0) &&
                            !(pixel6.R == 0 && pixel6.G == 0 && pixel6.B == 0 && pixel6.A == 0) &&
                            !(pixel7.R == 0 && pixel7.G == 0 && pixel7.B == 0 && pixel7.A == 0) &&
                            !(pixel8.R == 0 && pixel8.G == 0 && pixel8.B == 0 && pixel8.A == 0) &&
                            !(pixel9.R == 0 && pixel9.G == 0 && pixel9.B == 0 && pixel9.A == 0) &&
                            !(pixel10.R == 0 && pixel10.G == 0 && pixel10.B == 0 && pixel10.A == 0) &&
                            !(pixel11.R == 0 && pixel11.G == 0 && pixel11.B == 0 && pixel11.A == 0) &&
                            !(pixel12.R == 0 && pixel12.G == 0 && pixel12.B == 0 && pixel12.A == 0) &&
                            !(pixel13.R == 0 && pixel13.G == 0 && pixel13.B == 0 && pixel13.A == 0) &&
                            !(pixel14.R == 0 && pixel14.G == 0 && pixel14.B == 0 && pixel14.A == 0) &&
                            !(pixel15.R == 0 && pixel15.G == 0 && pixel15.B == 0 && pixel15.A == 0) &&
                            !(pixel16.R == 0 && pixel16.G == 0 && pixel16.B == 0 && pixel16.A == 0))
                        {
                            if (pixel1.A != 0)
                            {
                                r += pixel1.R;
                                g += pixel1.G;
                                b += pixel1.B;
                                a += pixel1.A;
                                div++;
                            }
                            if (pixel2.A != 0)
                            {
                                r += pixel2.R;
                                g += pixel2.G;
                                b += pixel2.B;
                                a += pixel2.A;
                                div++;
                            }
                            if (pixel3.A != 0)
                            {
                                r += pixel3.R;
                                g += pixel3.G;
                                b += pixel3.B;
                                a += pixel3.A;
                                div++;
                            }
                            if (pixel4.A != 0)
                            {
                                r += pixel4.R;
                                g += pixel4.G;
                                b += pixel4.B;
                                a += pixel4.A;
                                div++;
                            }
                            if (pixel5.A != 0)
                            {
                                r += pixel5.R;
                                g += pixel5.G;
                                b += pixel5.B;
                                a += pixel5.A;
                                div++;
                            }
                            if (pixel6.A != 0)
                            {
                                r += pixel6.R;
                                g += pixel6.G;
                                b += pixel6.B;
                                a += pixel6.A;
                                div++;
                            }
                            if (pixel7.A != 0)
                            {
                                r += pixel7.R;
                                g += pixel7.G;
                                b += pixel7.B;
                                a += pixel7.A;
                                div++;
                            }
                            if (pixel8.A != 0)
                            {
                                r += pixel8.R;
                                g += pixel8.G;
                                b += pixel8.B;
                                a += pixel8.A;
                                div++;
                            }
                            if (pixel9.A != 0)
                            {
                                r += pixel9.R;
                                g += pixel9.G;
                                b += pixel9.B;
                                a += pixel9.A;
                                div++;
                            }
                            if (pixel10.A != 0)
                            {
                                r += pixel10.R;
                                g += pixel10.G;
                                b += pixel10.B;
                                a += pixel10.A;
                                div++;
                            }
                            if (pixel11.A != 0)
                            {
                                r += pixel11.R;
                                g += pixel11.G;
                                b += pixel11.B;
                                a += pixel11.A;
                                div++;
                            }
                            if (pixel12.A != 0)
                            {
                                r += pixel12.R;
                                g += pixel12.G;
                                b += pixel12.B;
                                a += pixel12.A;
                                div++;
                            }
                            if (pixel13.A != 0)
                            {
                                r += pixel13.R;
                                g += pixel13.G;
                                b += pixel13.B;
                                a += pixel13.A;
                                div++;
                            }
                            if (pixel14.A != 0)
                            {
                                r += pixel14.R;
                                g += pixel14.G;
                                b += pixel14.B;
                                a += pixel14.A;
                                div++;
                            }
                            if (pixel15.A != 0)
                            {
                                r += pixel15.R;
                                g += pixel15.G;
                                b += pixel15.B;
                                a += pixel15.A;
                                div++;
                            }
                            if (pixel16.A != 0)
                            {
                                r += pixel16.R;
                                g += pixel16.G;
                                b += pixel16.B;
                                a += pixel16.A;
                                div++;
                            }

                            r = r / div;
                            g = g / div;
                            b = b / div;
                            a = a / div;

                            for (int c = 1; c < usedColors.Count; c++)
                            {
                                colorDistance = Math.Abs(r - deathnoteColors[(int)usedColors[c]].r) + Math.Abs(g - deathnoteColors[(int)usedColors[c]].g) + Math.Abs(b - deathnoteColors[(int)usedColors[c]].b);
                                if (colorDistance < closest)
                                {
                                    closest = colorDistance;
                                    closestColor = (int)usedColors[c];
                                }
                            }

                            if (closestColor > 0)
                                convertedImageBrush4.SetPixel((int)Math.Ceiling(x / 4.0) - 1, (int)Math.Ceiling(y / 4.0) - 1, Color.FromArgb(deathnoteColors[closestColor].r, deathnoteColors[closestColor].g, deathnoteColors[closestColor].b));
                            else
                                convertedImageBrush4.SetPixel((int)Math.Ceiling(x / 4.0) - 1, (int)Math.Ceiling(y / 4.0) - 1, Color.Transparent);

                            convertedImageColorsBrush4[(int)Math.Ceiling(x / 4.0) - 1, (int)Math.Ceiling(y / 4.0) - 1] = (DeathnoteColors)closestColor;
                        }
                        else
                            convertedImageColorsBrush4[(int)Math.Ceiling(x / 4.0) - 1, (int)Math.Ceiling(y / 4.0) - 1] = DeathnoteColors.TRANSPARENT;
                    }
                }
            }
            Array.Clear(convertedImageColorsBrush4Drawn, 0, convertedImageColorsBrush4Drawn.Length);

            if (cb_smoothing.Checked)
            {
                List<Tuple<int, int>> makeTransparent = new List<Tuple<int, int>>();
                for (int x = 0; x < convertedImageBrush4.Width; x++)
                {
                    for (int y = 0; y < convertedImageBrush4.Height; y++)
                    {
                        if (IsPointToSmooth(x, y, 4))
                        {
                            makeTransparent.Add(new Tuple<int, int>(x, y));
                        }
                    }
                }
                for (int i = 0; i < makeTransparent.Count; i++)
                {
                    convertedImageBrush4.SetPixel(makeTransparent[i].Item1, makeTransparent[i].Item2, Color.Transparent);
                    convertedImageColorsBrush4[makeTransparent[i].Item1, makeTransparent[i].Item2] = DeathnoteColors.NODRAW;
                }
            }
            #endregion

            if (!cb_only_selected_color.Checked)
            {
                if (trackBar_brushsize.Value == 1)
                    pb_converted.Image = convertedImageBrush1;
                else if (trackBar_brushsize.Value == 2)
                    pb_converted.Image = convertedImageBrush2;
                else if (trackBar_brushsize.Value == 3)
                    pb_converted.Image = convertedImageBrush3;
                else if (trackBar_brushsize.Value == 4)
                    pb_converted.Image = convertedImageBrush4;
            }
            else
            {
                if (cb_only_selected_color.Checked && clb_colors.SelectedIndex > 0)
                {
                    for (int c = 0; c < Enum.GetNames(typeof(DeathnoteColors)).Length; c++)
                    {
                        if (((DeathnoteColors)c).ToString() == clb_colors.SelectedItem.ToString())
                        {
                            ShowOneColorPicture(trackBar_brushsize.Value, (DeathnoteColors)c);
                            break;
                        }
                    }
                }
            }

            updateNeeded = false;
            btn_update.Enabled = false;
        }

        void ShowOneColorPicture(int brushShize, DeathnoteColors color)
        {
            Bitmap singleColor = null;

            if (trackBar_brushsize.Value == 1)
            {
                singleColor = new Bitmap(convertedImageBrush1.Width, convertedImageBrush1.Height);
                for (int x = 0; x < convertedImageBrush1.Width; x++)
                {
                    for (int y = 0; y < convertedImageBrush1.Height; y++)
                    {
                        if (convertedImageColorsBrush1[x, y] == color)
                        {
                            singleColor.SetPixel(x, y, Color.FromArgb(deathnoteColors[(int)color].r, deathnoteColors[(int)color].g, deathnoteColors[(int)color].b));
                        }
                    }
                }
            }
            else if (trackBar_brushsize.Value == 2)
            {
                singleColor = new Bitmap(convertedImageBrush2.Width, convertedImageBrush2.Height);
                for (int x = 0; x < convertedImageBrush2.Width; x++)
                {
                    for (int y = 0; y < convertedImageBrush2.Height; y++)
                    {
                        if (convertedImageColorsBrush2[x, y] == color)
                        {
                            singleColor.SetPixel(x, y, Color.FromArgb(deathnoteColors[(int)color].r, deathnoteColors[(int)color].g, deathnoteColors[(int)color].b));
                        }
                    }
                }
            }
            else if (trackBar_brushsize.Value == 3)
            {
                singleColor = new Bitmap(convertedImageBrush3.Width, convertedImageBrush3.Height);
                for (int x = 0; x < convertedImageBrush3.Width; x++)
                {
                    for (int y = 0; y < convertedImageBrush3.Height; y++)
                    {
                        if (convertedImageColorsBrush3[x, y] == color)
                        {
                            singleColor.SetPixel(x, y, Color.FromArgb(deathnoteColors[(int)color].r, deathnoteColors[(int)color].g, deathnoteColors[(int)color].b));
                        }
                    }
                }
            }
            else if (trackBar_brushsize.Value == 4)
            {
                singleColor = new Bitmap(convertedImageBrush4.Width, convertedImageBrush4.Height);
                for (int x = 0; x < convertedImageBrush4.Width; x++)
                {
                    for (int y = 0; y < convertedImageBrush4.Height; y++)
                    {
                        if (convertedImageColorsBrush4[x, y] == color)
                        {
                            singleColor.SetPixel(x, y, Color.FromArgb(deathnoteColors[(int)color].r, deathnoteColors[(int)color].g, deathnoteColors[(int)color].b));
                        }
                    }
                }
            }
            pb_converted.Image = singleColor;
        }

        #endregion

        #region Events

        private void btn_select_image_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK && ImageExtensions.Contains(Path.GetExtension(openFileDialog1.FileName).ToUpperInvariant()))
            {
                tb_path.Text = openFileDialog1.FileName;
                pb_original.ImageLocation = openFileDialog1.FileName;

                btn_draw.Enabled = true;

                Image originalImage = Image.FromFile(openFileDialog1.FileName);

                if (originalImage.Width > deathnote.width || originalImage.Height > deathnote.height)
                    resizedImage = ResizeImageFixedSize(originalImage, deathnote.width, deathnote.height);
                else
                {
                    resizedImage = new Bitmap(deathnote.width, deathnote.height) as Image;
                    ((Bitmap)resizedImage).MakeTransparent();
                    Graphics g = Graphics.FromImage(resizedImage);
                    g.DrawImage(originalImage, deathnote.width / 2 - originalImage.Width / 2, deathnote.height / 2 - originalImage.Height / 2, originalImage.Width, originalImage.Height);
                }

                ResetDrawPosition();

                CreateImages(resizedImage);
            }
        }

        private void btn_draw_Click(object sender, EventArgs e)
        {
            trackBar_brushsize.Enabled = false;

            KeyboardHook.ABORT = false;
            MakeWindowActive("Throne of Lies");

            if (selectedColor == DeathnoteColors.TRANSPARENT)
            {
                if (updateNeeded)
                    CreateImages(resizedImage);

                LClick(deathnote.clearButtonX, deathnote.clearButtonY);
                LClick(deathnote.clearButtonYesX, deathnote.clearButtonYesY);

                LClick(deathnote.brushButtonX, deathnote.brushButtonY);
                if (trackBar_brushsize.Value == 1)
                    LClick(deathnote.brushSizeOneX, deathnote.brushSizeOneY);
                else if (trackBar_brushsize.Value == 2)
                    LClick(deathnote.brushSizeTwoX, deathnote.brushSizeTwoY);
                else if (trackBar_brushsize.Value == 3)
                    LClick(deathnote.brushSizeThreeX, deathnote.brushSizeThreeY);
                else if (trackBar_brushsize.Value == 4)
                    LClick(deathnote.brushSizeFourX, deathnote.brushSizeFourY);
            }

            drawX = int.Parse(tb_drawX.Text) - 1;
            drawY = int.Parse(tb_drawY.Text) - 1;

            for (int c = usedColorIndex; c < usedColors.Count; c++)
            {
                if (usedColors[c] == DeathnoteColors.TRANSPARENT)
                    continue;

                if (trackBar_brushsize.Value == 1)
                {
                    #region Brush Size 1
                    for (int y = drawY; y < convertedImageColorsBrush1.GetLength(1); y++)
                    {
                        for (int x = drawX; x < convertedImageColorsBrush1.GetLength(0); x++)
                        {
                            if (convertedImageColorsBrush1[x, y] == usedColors[c])
                            {
                                if (IsValidDrawPoint(x, y, 1, false))
                                {
                                    if (convertedImageColorsBrush1[x, y] != selectedColor)
                                    {
                                        LClick(deathnoteColors[(int)convertedImageColorsBrush1[x, y]].x, deathnoteColors[(int)convertedImageColorsBrush1[x, y]].y);
                                        selectedColor = convertedImageColorsBrush1[x, y];
                                        usedColorIndex = c;
                                    }

                                    Tuple<int, int> newPoint;
                                    bool noAdjactentPoints = true;

                                    int xAdj = x;
                                    int yAdj = y;
                                    do
                                    {
                                        if (KeyboardHook.ABORT)
                                            break;
                                        newPoint = SearchAdjacentPoint(xAdj, yAdj, 1, false);
                                        if (newPoint.Item1 != -1 && newPoint.Item2 != -1 && noAdjactentPoints)
                                        {
                                            noAdjactentPoints = false;
                                            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(deathnote.x + x, deathnote.y + y);
                                            Wait(10);
                                            mouse_event((System.Int32)(MouseActionAdresses.LEFTDOWN), 0, 0, 0, 0);
                                            Wait(10);
                                            convertedImageColorsBrush1Drawn[x, y] = true;
                                        }
                                        if (newPoint.Item1 != -1 && newPoint.Item2 != -1)
                                        {
                                            xAdj = newPoint.Item1;
                                            yAdj = newPoint.Item2;
                                            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(deathnote.x + xAdj, deathnote.y + yAdj);
                                            Wait(10);
                                            convertedImageColorsBrush1Drawn[xAdj, yAdj] = true;
                                        }

                                    } while (newPoint.Item1 != -1 && newPoint.Item2 != -1);

                                    if (!noAdjactentPoints)
                                    {
                                        mouse_event((System.Int32)(MouseActionAdresses.LEFTUP), 0, 0, 0, 0);
                                        Wait(40);
                                    }
                                    else
                                    {
                                        LClickFast(deathnote.x + x, deathnote.y + y);
                                        convertedImageColorsBrush1Drawn[x, y] = true;
                                    }
                                }
                            }

                            if (KeyboardHook.ABORT)
                            {
                                drawX = x;
                                break;
                            }
                        }

                        if (KeyboardHook.ABORT)
                        {
                            drawY = y;
                            break;
                        }
                        else
                            drawX = 0;
                    }
                    #endregion
                }
                else if (trackBar_brushsize.Value == 2)
                {
                    #region Brush Size 2
                    for (int y = drawY; y < convertedImageColorsBrush2.GetLength(1); y++)
                    {
                        for (int x = drawX; x < convertedImageColorsBrush2.GetLength(0); x++)
                        {
                            if (convertedImageColorsBrush2[x, y] == usedColors[c])
                            {
                                if (IsValidDrawPoint(x, y, 2, false))
                                {
                                    if (convertedImageColorsBrush2[x, y] != selectedColor)
                                    {
                                        LClick(deathnoteColors[(int)convertedImageColorsBrush2[x, y]].x, deathnoteColors[(int)convertedImageColorsBrush2[x, y]].y);
                                        selectedColor = convertedImageColorsBrush2[x, y];
                                        usedColorIndex = c;
                                    }

                                    Tuple<int, int> newPoint;
                                    bool noAdjactentPoints = true;

                                    int xAdj = x;
                                    int yAdj = y;
                                    do
                                    {
                                        if (KeyboardHook.ABORT)
                                            break;
                                        newPoint = SearchAdjacentPoint(xAdj, yAdj, 2, false);
                                        if (newPoint.Item1 != -1 && newPoint.Item2 != -1 && noAdjactentPoints)
                                        {
                                            noAdjactentPoints = false;
                                            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(deathnote.x + 2 * x, deathnote.y + 2 * y);
                                            Wait(10);
                                            mouse_event((System.Int32)(MouseActionAdresses.LEFTDOWN), 0, 0, 0, 0);
                                            Wait(10);
                                            convertedImageColorsBrush2Drawn[x, y] = true;
                                        }
                                        if (newPoint.Item1 != -1 && newPoint.Item2 != -1)
                                        {
                                            xAdj = newPoint.Item1;
                                            yAdj = newPoint.Item2;
                                            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(deathnote.x + 2 * xAdj, deathnote.y + 2 * yAdj);
                                            Wait(10);
                                            convertedImageColorsBrush2Drawn[xAdj, yAdj] = true;
                                        }

                                    } while (newPoint.Item1 != -1 && newPoint.Item2 != -1);

                                    if (!noAdjactentPoints)
                                    {
                                        mouse_event((System.Int32)(MouseActionAdresses.LEFTUP), 0, 0, 0, 0);
                                        Wait(40);
                                    }
                                    else
                                    {
                                        LClickFast(deathnote.x + 2 * x, deathnote.y + 2 * y);
                                        convertedImageColorsBrush2Drawn[x, y] = true;
                                    }
                                }
                            }

                            if (KeyboardHook.ABORT)
                            {
                                drawX = x;
                                break;
                            }
                        }

                        if (KeyboardHook.ABORT)
                        {
                            drawY = y;
                            break;
                        }
                        else
                            drawX = 0;
                    }
                    #endregion
                }
                else if (trackBar_brushsize.Value == 3)
                {
                    #region Brush Size 3
                    for (int y = drawY; y < convertedImageColorsBrush3.GetLength(1); y++)
                    {
                        for (int x = drawX; x < convertedImageColorsBrush3.GetLength(0); x++)
                        {
                            if (convertedImageColorsBrush3[x, y] == usedColors[c])
                            {
                                if (IsValidDrawPoint(x, y, 3, false))
                                {
                                    if (convertedImageColorsBrush3[x, y] != selectedColor)
                                    {
                                        LClick(deathnoteColors[(int)convertedImageColorsBrush3[x, y]].x, deathnoteColors[(int)convertedImageColorsBrush3[x, y]].y);
                                        selectedColor = convertedImageColorsBrush3[x, y];
                                        usedColorIndex = c;
                                    }

                                    Tuple<int, int> newPoint;
                                    bool noAdjactentPoints = true;

                                    int xAdj = x;
                                    int yAdj = y;
                                    do
                                    {
                                        if (KeyboardHook.ABORT)
                                            break;
                                        newPoint = SearchAdjacentPoint(xAdj, yAdj, 3, false);
                                        if (newPoint.Item1 != -1 && newPoint.Item2 != -1 && noAdjactentPoints)
                                        {
                                            noAdjactentPoints = false;
                                            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(deathnote.x + 3 * x, deathnote.y + 3 * y);
                                            Wait(10);
                                            mouse_event((System.Int32)(MouseActionAdresses.LEFTDOWN), 0, 0, 0, 0);
                                            Wait(10);
                                            convertedImageColorsBrush3Drawn[x, y] = true;
                                        }
                                        if (newPoint.Item1 != -1 && newPoint.Item2 != -1)
                                        {
                                            xAdj = newPoint.Item1;
                                            yAdj = newPoint.Item2;
                                            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(deathnote.x + 3 * xAdj, deathnote.y + 3 * yAdj);
                                            Wait(10);
                                            convertedImageColorsBrush3Drawn[xAdj, yAdj] = true;
                                        }

                                    } while (newPoint.Item1 != -1 && newPoint.Item2 != -1);

                                    if (!noAdjactentPoints)
                                    {
                                        mouse_event((System.Int32)(MouseActionAdresses.LEFTUP), 0, 0, 0, 0);
                                        Wait(40);
                                    }
                                    else
                                    {
                                        LClickFast(deathnote.x + 3 * x, deathnote.y + 3 * y);
                                        convertedImageColorsBrush3Drawn[x, y] = true;
                                    }
                                }
                            }

                            if (KeyboardHook.ABORT)
                            {
                                drawX = x;
                                break;
                            }
                        }

                        if (KeyboardHook.ABORT)
                        {
                            drawY = y;
                            break;
                        }
                        else
                            drawX = 0;
                    }
                    #endregion
                }
                else if (trackBar_brushsize.Value == 4)
                {
                    #region Brush Size 4
                    for (int y = drawY; y < convertedImageColorsBrush4.GetLength(1); y++)
                    {
                        for (int x = drawX; x < convertedImageColorsBrush4.GetLength(0); x++)
                        {
                            if (convertedImageColorsBrush4[x, y] == usedColors[c])
                            {
                                if (IsValidDrawPoint(x, y, 4, false))
                                {
                                    if (convertedImageColorsBrush4[x, y] != selectedColor)
                                    {
                                        LClick(deathnoteColors[(int)convertedImageColorsBrush4[x, y]].x, deathnoteColors[(int)convertedImageColorsBrush4[x, y]].y);
                                        selectedColor = convertedImageColorsBrush4[x, y];
                                        usedColorIndex = c;
                                    }

                                    Tuple<int, int> newPoint;
                                    bool noAdjactentPoints = true;

                                    int xAdj = x;
                                    int yAdj = y;
                                    do
                                    {
                                        if (KeyboardHook.ABORT)
                                            break;
                                        newPoint = SearchAdjacentPoint(xAdj, yAdj, 4, false);
                                        if (newPoint.Item1 != -1 && newPoint.Item2 != -1 && noAdjactentPoints)
                                        {
                                            noAdjactentPoints = false;
                                            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(deathnote.x + 4 * x, deathnote.y + 4 * y);
                                            Wait(10);
                                            mouse_event((System.Int32)(MouseActionAdresses.LEFTDOWN), 0, 0, 0, 0);
                                            Wait(10);
                                            convertedImageColorsBrush4Drawn[x, y] = true;
                                        }
                                        if (newPoint.Item1 != -1 && newPoint.Item2 != -1)
                                        {
                                            xAdj = newPoint.Item1;
                                            yAdj = newPoint.Item2;
                                            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(deathnote.x + 4 * xAdj, deathnote.y + 4 * yAdj);
                                            Wait(10);
                                            convertedImageColorsBrush4Drawn[xAdj, yAdj] = true;
                                        }

                                    } while (newPoint.Item1 != -1 && newPoint.Item2 != -1);

                                    if (!noAdjactentPoints)
                                    {
                                        mouse_event((System.Int32)(MouseActionAdresses.LEFTUP), 0, 0, 0, 0);
                                        Wait(40);
                                    }
                                    else
                                    {
                                        LClickFast(deathnote.x + 4 * x, deathnote.y + 4 * y);
                                        convertedImageColorsBrush4Drawn[x, y] = true;
                                    }
                                }
                            }

                            if (KeyboardHook.ABORT)
                            {
                                drawX = x;
                                break;
                            }
                        }

                        if (KeyboardHook.ABORT)
                        {
                            drawY = y;
                            break;
                        }
                        else
                            drawX = 0;
                    }
                    #endregion
                }

                if (KeyboardHook.ABORT)
                    break;
                else
                {
                    drawX = 0;
                    drawY = 0;
                }
            }
            tb_drawX.Text = (drawX + 1).ToString();
            tb_drawY.Text = (drawY + 1).ToString();

            KeyboardHook.ABORT = false;
        }

        private void btn_reset_draw_pos_Click(object sender, EventArgs e)
        {
            ResetDrawPosition();
        }

        private void btn_colors_up_Click(object sender, EventArgs e)
        {
            int i = clb_colors.SelectedIndex;

            if (i > 1)
            {
                bool check = clb_colors.GetItemChecked(i);
                clb_colors.Items.Insert(i - 1, clb_colors.Items[i]);
                clb_colors.SetItemChecked(i - 1, check);

                clb_colors.Items.RemoveAt(i + 1);
                clb_colors.SelectedIndex = i - 1;

                btn_update.Enabled = true;
                updateNeeded = true;
            }
        }

        private void btn_colors_down_Click(object sender, EventArgs e)
        {
            int i = clb_colors.SelectedIndex;

            if (i > 0 && i < clb_colors.Items.Count - 1)
            {
                bool check = clb_colors.GetItemChecked(i);
                clb_colors.Items.Insert(i + 2, clb_colors.Items[i]);
                clb_colors.SetItemChecked(i + 2, check);

                clb_colors.Items.RemoveAt(i);
                clb_colors.SelectedIndex = i + 1;

                btn_update.Enabled = true;
                updateNeeded = true;
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            CreateImages(resizedImage);

            ResetDrawPosition();
        }

        private void clb_colors_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            btn_update.Enabled = true;
            updateNeeded = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyboardHook.Hook();
        }

        private void trackBar_brushsize_Scroll(object sender, EventArgs e)
        {
            lbl_brushsize.Text = ((TrackBar)sender).Value.ToString();

            if (!cb_only_selected_color.Checked)
            {
                if (((TrackBar)sender).Value == 1)
                    pb_converted.Image = convertedImageBrush1;
                else if (((TrackBar)sender).Value == 2)
                    pb_converted.Image = convertedImageBrush2;
                else if (((TrackBar)sender).Value == 3)
                    pb_converted.Image = convertedImageBrush3;
                else if (((TrackBar)sender).Value == 4)
                    pb_converted.Image = convertedImageBrush4;
            }
            else
            {
                if (cb_only_selected_color.Checked && clb_colors.SelectedIndex > 0)
                {
                    for (int c = 0; c < Enum.GetNames(typeof(DeathnoteColors)).Length; c++)
                    {
                        if (((DeathnoteColors)c).ToString() == clb_colors.SelectedItem.ToString())
                        {
                            ShowOneColorPicture(((TrackBar)sender).Value, (DeathnoteColors)c);
                            break;
                        }
                    }
                }
            }
        }

        private void cb_only_selected_color_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked && clb_colors.SelectedIndex > 0)
            {
                for (int c = 0; c < Enum.GetNames(typeof(DeathnoteColors)).Length; c++)
                {
                    if (((DeathnoteColors)c).ToString() == clb_colors.SelectedItem.ToString())
                    {
                        ShowOneColorPicture(trackBar_brushsize.Value, (DeathnoteColors)c);
                        break;
                    }
                }
            } 
            else if (!((CheckBox)sender).Checked)
            {
                if (trackBar_brushsize.Value == 1)
                    pb_converted.Image = convertedImageBrush1;
                else if (trackBar_brushsize.Value == 2)
                    pb_converted.Image = convertedImageBrush2;
                else if (trackBar_brushsize.Value == 3)
                    pb_converted.Image = convertedImageBrush3;
                else if (trackBar_brushsize.Value == 4)
                    pb_converted.Image = convertedImageBrush4;
            }
        }

        private void clb_colors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_only_selected_color.Checked && ((CheckedListBox)sender).SelectedIndex > 0)
            {
                for (int c = 0; c < Enum.GetNames(typeof(DeathnoteColors)).Length; c++)
                {
                    if (((DeathnoteColors)c).ToString() == ((CheckedListBox)sender).SelectedItem.ToString())
                    {
                        ShowOneColorPicture(trackBar_brushsize.Value, (DeathnoteColors)c);
                        break;
                    }
                }
            }
        }

        private void cb_smoothing_CheckedChanged(object sender, EventArgs e)
        {
            btn_update.Enabled = true;
            updateNeeded = true;
        }

        private void trackBar_smoothing_Scroll(object sender, EventArgs e)
        {
            btn_update.Enabled = true;
            updateNeeded = true;
        }

        private void trackBar_smooth_transparent_Scroll(object sender, EventArgs e)
        {
            btn_update.Enabled = true;
            updateNeeded = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            KeyboardHook.UnHook();
        }

        #endregion
    }
}
