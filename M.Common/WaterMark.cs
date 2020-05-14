using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace M.Common
{
    public class WaterMark
    {
        /// <summary>
        /// 图片添加图片水印
        /// </summary>

        #region 变量声明开始
        /// <summary>
        /// 枚举: 水印位置
        /// </summary>
        public enum WatermarkPosition
        {
            /// <summary>
            /// 左上
            /// </summary>
            LeftTop,
            /// <summary>
            /// 左中
            /// </summary>
            Left,
            /// <summary>
            /// 左下
            /// </summary>
            LeftBottom,
            /// <summary>
            /// 正上
            /// </summary>
            Top,
            /// <summary>
            /// 正中
            /// </summary>
            Center,
            /// <summary>
            /// 正下
            /// </summary>
            Bottom,
            /// <summary>
            /// 右上
            /// </summary>
            RightTop,
            /// <summary>
            /// 右中
            /// </summary>
            RightCenter,
            /// <summary>
            /// 右下
            /// </summary>
            RigthBottom
        }
        #endregion 变量声明结束
        #region 构造函数开始
        /// <summary>
        /// 构造函数: 默认
        /// </summary>
        public WaterMark()
        { }
        #endregion 构造函数结束
        #region 私有函数开始
        /// <summary>
        /// 获取: 图片去扩展名(包含完整路径及其文件名)小写字符串
        /// </summary>
        /// <param name="path">图片路径(包含完整路径,文件名及其扩展名): string</param>
        /// <returns>返回: 图片去扩展名(包含完整路径及其文件名)小写字符串: string</returns>
        private string GetFileName(string path)
        {
            return path.Remove(path.LastIndexOf('.')).ToLower();
        }
        /// <summary>
        /// 获取: 图片以'.'开头的小写字符串扩展名
        /// </summary>
        /// <param name="path">图片路径(包含完整路径,文件名及其扩展名): string</param>
        /// <returns>返回: 图片以'.'开头的小写字符串扩展名: string</returns>
        private string GetExtension(string path)
        {
            return path.Remove(0, path.LastIndexOf('.')).ToLower();
        }
        /// <summary>
        /// 获取: 图片以 '.' 开头的小写字符串扩展名对应的 System.Drawing.Imaging.ImageFormat 对象
        /// </summary>
        /// <param name="format">以 '. '开头的小写字符串扩展名: string</param>
        /// <returns>返回: 图片以 '.' 开头的小写字符串扩展名对应的 System.Drawing.Imaging.ImageFormat 对象: System.Drawing.Imaging.ImageFormat</returns>
        private ImageFormat GetImageFormat(string format)
        {
            switch (format)
            {
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".emf":
                    return ImageFormat.Emf;
                case ".exif":
                    return ImageFormat.Exif;
                case ".gif":
                    return ImageFormat.Gif;
                case ".ico":
                    return ImageFormat.Icon;
                case ".png":
                    return ImageFormat.Png;
                case ".tif":
                    return ImageFormat.Tiff;
                case ".tiff":
                    return ImageFormat.Tiff;
                case ".wmf":
                    return ImageFormat.Wmf;
                default:
                    return ImageFormat.Jpeg;
            }
        }
        /// <summary>
        /// 获取: 枚举 Uinatlex.ToolBox.ImageManager.WatermarkPosition 对应的 System.Drawing.Rectangle 对象
        /// </summary>
        /// <param name="positon">枚举 Uinatlex.ToolBox.ImageManager.WatermarkPosition: Uinatlex.ToolBox.ImageManager.WatermarkPosition</param>
        /// <param name="X">原图宽度: int</param>
        /// <param name="Y">原图高度: int</param>
        /// <param name="x">水印宽度: int</param>
        /// <param name="y">水印高度: int</param>
        /// <param name="i">边距: int</param>
        /// <returns>返回: 枚举 Uinatlex.ToolBox.ImageManager.WatermarkPosition 对应的 System.Drawing.Rectangle 对象: System.Drawing.Rectangle</returns>
        private Rectangle GetWatermarkRectangle(WatermarkPosition positon, int X, int Y, int x, int y, int i)
        {
            switch (positon)
            {
                case WatermarkPosition.LeftTop:
                    return new Rectangle(i, i, x, y);
                case WatermarkPosition.Left:
                    return new Rectangle(i, (Y - y) / 2, x, y);
                case WatermarkPosition.LeftBottom:
                    return new Rectangle(i, Y - y - i, x, y);
                case WatermarkPosition.Top:
                    return new Rectangle((X - x) / 2, i, x, y);
                case WatermarkPosition.Center:
                    return new Rectangle((X - x) / 2, (Y - y) / 2, x, y);
                case WatermarkPosition.Bottom:
                    return new Rectangle((X - x) / 2, Y - y - i, x, y);
                case WatermarkPosition.RightTop:
                    return new Rectangle(X - x - i, i, x, y);
                case WatermarkPosition.RightCenter:
                    return new Rectangle(X - x - i, (Y - y) / 2, x, y);
                default:
                    return new Rectangle(X - x - i, Y - y - i, x, y);
            }
        }
        #endregion 私有函数结束
        #region 文字生成开始
        #endregion 文字生成结束
        #region 设置透明度开始
        /// <summary>
        /// 设置: 图片 System.Drawing.Bitmap 对象透明度
        /// </summary>
        /// <param name="sBitmap">图片 System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="transparence">水印透明度(值越高透明度越低,范围在0.0f~1.0f之间): float</param>
        /// <returns>图片 System.Drawing.Bitmap: System.Drawing.Bitmap</returns>
        public Bitmap SetTransparence(Bitmap bm, float transparence)
        {
            if (transparence == 0.0f || transparence == 1.0f)
                throw new ArgumentException("透明度值只能在0.0f~1.0f之间");
            float[][] floatArray =
            {
                new float[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f },
                new float[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f },
                new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f },
                new float[] { 0.0f, 0.0f, 0.0f, transparence, 0.0f },
                new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
            };
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(new ColorMatrix(floatArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Bitmap bitmap = new Bitmap(bm.Width, bm.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(bm, new Rectangle(0, 0, bm.Width, bm.Height), 0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel, imageAttributes);
            graphics.Dispose();
            imageAttributes.Dispose();
            bm.Dispose();
            return bitmap;
        }
        /// <summary>
        ///  设置: 图片 System.Drawing.Bitmap 对象透明度
        /// </summary>
        /// <param name="readpath">图片路径(包含完整路径,文件名及其扩展名): string</param>
        /// <param name="transparence">水印透明度(值越高透明度越低,范围在0.0f~1.0f之间): float</param>
        /// <returns>图片 System.Drawing.Bitmap: System.Drawing.Bitmap</returns>
        public Bitmap SetTransparence(string readpath, float transparence)
        {
            return SetTransparence(new Bitmap(readpath), transparence);
        }
        #endregion 设置透明度结束
        #region 添加水印开始
        /// <summary>
        /// 生成: 原图绘制水印的 System.Drawing.Bitmap 对象
        /// </summary>
        /// <param name="sBitmap">原图 System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="wBitmap">水印 System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="position">枚举 Uinatlex.ToolBox.ImageManager.WatermarkPosition : Uinatlex.ToolBox.ImageManager.WatermarkPosition</param>
        /// <param name="margin">水印边距: int</param>
        /// <returns>返回: 原图绘制水印的 System.Drawing.Bitmap 对象 System.Drawing.Bitmap</returns>
        public Bitmap CreateWatermark(Bitmap sBitmap, Bitmap wBitmap, WatermarkPosition position, int margin)
        {
            Graphics graphics = Graphics.FromImage(sBitmap);
            graphics.DrawImage(wBitmap, GetWatermarkRectangle(position, sBitmap.Width, sBitmap.Height, wBitmap.Width, wBitmap.Height, margin));
            graphics.Dispose();
            wBitmap.Dispose();
            return sBitmap;
        }
        #endregion 添加水印结束
        #region 图片切割开始
        #endregion 图片切割结束
        #region 图片缩放开始
        #endregion 图片缩放结束
        #region 保存图片到文件开始
        #region 普通保存开始
        /// <summary>
        /// 保存: System.Drawing.Bitmap 对象到图片文件
        /// </summary>
        /// <param name="bitmap">System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="writepath">保存路径(包含完整路径,文件名及其扩展名): string</param>
        public void Save(Bitmap bitmap, string writepath)
        {
            try
            {
                bitmap.Save(writepath, GetImageFormat(GetExtension(writepath)));
                bitmap.Dispose();
            }
            catch
            {
                throw new ArgumentException("图片保存错误");
            }
        }
        /// <summary>
        /// 保存: 对象到图片文件
        /// </summary>
        /// <param name="readpath">原图路径(包含完整路径,文件名及其扩展名): string</param>
        /// <param name="writepath">保存路径(包含完整路径,文件名及其扩展名): string</param>
        public void Save(string readpath, string writepath)
        {
            if (string.Compare(readpath, writepath) == 0)
                throw new ArgumentException("源图片与目标图片地址相同");
            try
            {
                Save(new Bitmap(readpath), writepath);
            }
            catch
            {
                throw new ArgumentException("图片读取错误");
            }
        }
        #endregion 普通保存结束
        #region 文字绘图保存开始
        #endregion 文字绘图保存结束
        #region 透明度调整保存开始
        /// <summary>
        /// 保存: 设置透明度的对象到图片文件
        /// </summary>
        /// <param name="sBitmap">图片 System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="transparence">水印透明度(值越高透明度越低,范围在0.0f~1.0f之间): float</param>
        /// <param name="writepath">保存路径(包含完整路径,文件名及其扩展名): string</param>
        public void SaveTransparence(Bitmap bitmap, float transparence, string writepath)
        {
            Save(SetTransparence(bitmap, transparence), writepath);
        }
        /// <summary>
        /// 保存: 设置透明度的象到图片文件
        /// </summary>
        /// <param name="readpath">原图路径(包含完整路径,文件名及其扩展名): string</param>
        /// <param name="transparence">水印透明度(值越高透明度越低,范围在0.0f~1.0f之间): float</param>
        /// <param name="writepath">保存路径(包含完整路径,文件名及其扩展名): string</param>
        public void SaveTransparence(string readpath, float transparence, string writepath)
        {
            Save(SetTransparence(readpath, transparence), writepath);
        }
        #endregion 透明度调整保存结束
        #region 水印图片保存开始
        /// <summary>
        /// 保存: 绘制水印的对象到图片文件
        /// </summary>
        /// <param name="sBitmap">原图 System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="wBitmap">水印 System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="position">枚举 Uinatlex.ToolBox.ImageManager.WatermarkPosition : Uinatlex.ToolBox.ImageManager.WatermarkPosition</param>
        /// <param name="margin">水印边距: int</param>
        /// <param name="writepath">保存路径(包含完整路径,文件名及其扩展名): string</param>
        public void SaveWatermark(Bitmap sBitmap, Bitmap wBitmap, WatermarkPosition position, int margin, string writepath)
        {
            Save(CreateWatermark(sBitmap, wBitmap, position, margin), writepath);
        }
        /// <summary>
        /// 保存: 绘制水印的对象到图片文件
        /// </summary>
        /// <param name="readpath">图片路径(包含完整路径,文件名及其扩展名): string</param>
        /// <param name="watermarkpath">水印图片路径(包含完整路径,文件名及其扩展名): string</param>
        /// <param name="transparence">水印透明度(值越高透明度越低,范围在0.0f~1.0f之间): float</param>
        /// <param name="position">枚举 Uinatlex.ToolBox.ImageManager.WatermarkPosition : Uinatlex.ToolBox.ImageManager.WatermarkPosition</param>
        /// <param name="margin">水印边距: int</param>
        /// <param name="writepath">保存路径(包含完整路径,文件名及其扩展名): string</param>
        public void SaveWatermark(string readpath, string watermarkpath, float transparence, WatermarkPosition position, int margin, string writepath)
        {
            if (string.Compare(readpath, writepath) == 0)
                throw new ArgumentException("源图片与目标图片地址相同");
            if (transparence == 0.0f)
                Save(readpath, writepath);
            else if (transparence == 1.0f)
                SaveWatermark(new Bitmap(readpath), new Bitmap(watermarkpath), position, margin, writepath);
            else
                SaveWatermark(new Bitmap(readpath), SetTransparence(watermarkpath, transparence), position, margin, writepath);
        }
        #endregion 水印图片保存结束
        #region 图片切割保存开始
        #endregion 图片切割保存结束
        #region 图片缩放保存开始
        #endregion 图片缩放保存开始
        #endregion 保存图片到文件结束


        /// <summary>
        /// 加图片水印--sailor provider
        /// </summary>
        /// <param name="OriginalImg">要加水印的原图</param>
        /// <param name="WaterMarkFileName">水印图</param>
        /// <param name="filename">加水印后的图片保存位置</param>
        /// <param name="watermarkStatus">图片水印位置1=左上 2=中上 3=右上 4=左中  5=中中 6=右中 7=左下 8=右中 9=右下</param>
        /// <param name="quality">加水印后的质量0~100,数字越大质量越高</param>
        /// <param name="watermarkTransparency">水印图片的透明度1~10,数字越小越透明,10为不透明</param>
        /// <returns></returns>
        public static void ImageWaterMark(Image OriginalImg, string WaterMarkFileName, string filename, int watermarkStatus, int quality, int watermarkTransparency)
        {
            Graphics g = Graphics.FromImage(OriginalImg);
            //设置高质量插值法
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Image watermark = new Bitmap(WaterMarkFileName);

            if (watermark.Height >= OriginalImg.Height || watermark.Width >= OriginalImg.Width)
                return;

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();

            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float transparency = 0.5F;
            if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
                transparency = (watermarkTransparency / 10.0F);


            float[][] colorMatrixElements = {
                                                new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                            };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            int xpos = 0;
            int ypos = 0;

            switch (watermarkStatus)
            {
                case 1:
                    xpos = (int)(OriginalImg.Width * (float).01);
                    ypos = (int)(OriginalImg.Height * (float).01);
                    break;
                case 2:
                    xpos = (int)((OriginalImg.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)(OriginalImg.Height * (float).01);
                    break;
                case 3:
                    xpos = (int)((OriginalImg.Width * (float).99) - (watermark.Width));
                    ypos = (int)(OriginalImg.Height * (float).01);
                    break;
                case 4:
                    xpos = (int)(OriginalImg.Width * (float).01);
                    ypos = (int)((OriginalImg.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 5:
                    xpos = (int)((OriginalImg.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)((OriginalImg.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 6:
                    xpos = (int)((OriginalImg.Width * (float).99) - (watermark.Width));
                    ypos = (int)((OriginalImg.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 7:
                    xpos = (int)(OriginalImg.Width * (float).01);
                    ypos = (int)((OriginalImg.Height * (float).99) - watermark.Height);
                    break;
                case 8:
                    xpos = (int)((OriginalImg.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)((OriginalImg.Height * (float).99) - watermark.Height);
                    break;
                case 9:
                    xpos = (int)((OriginalImg.Width * (float).99) - (watermark.Width));
                    ypos = (int)((OriginalImg.Height * (float).99) - watermark.Height);
                    break;
            }

            g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType.IndexOf("jpeg") > -1)
                    ici = codec;
            }
            EncoderParameters encoderParams = new EncoderParameters();
            long[] qualityParam = new long[1];
            if (quality < 0 || quality > 100)
                quality = 80;

            qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
            encoderParams.Param[0] = encoderParam;

            if (ici != null)
                OriginalImg.Save(filename, ici, encoderParams);
            else
                OriginalImg.Save(filename);

            g.Dispose();
            OriginalImg.Dispose();
            watermark.Dispose();
            imageAttributes.Dispose();
        }
    }
}
