using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace rr_library.Models.CaptchaService
{
    /// <summary>
    /// 随机生成设定验证码，并随机旋转一定角度，字体颜色不同
    /// </summary>
    public class RRCaptchaClass
    {
        /// <summary>
        /// 定义随机数
        /// </summary>
        private static Random SrRandom { get; } = new();
        private static int IsNegative => SrRandom.Next(10) > 4 ? 1 : -1;
        public static string CreateRandomCode(int length)
        {
            int rand;
            char code;
            string randomcode = string.Empty;
            //生成一定长度的验证码
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                rand = random.Next();
                if (rand % 3 == 0)
                {
                    code = (char)('A' + (char)(rand % 26));
                }
                else
                {
                    code = (char)('0' + (char)(rand % 10));
                }
                randomcode += code.ToString();
            }
            return randomcode;
        }
        /// <summary>
        /// 创建随机码图片
        /// </summary>
        /// <param name="srs">需生成图片的字符串</param>
        /// <returns></returns>
        public static async Task<string> CreateSvgOutput(string srs, string[] srFonts, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("RRCaptchaClass.CreateSvgOutput");
            var stream = new MemoryStream();
            var reader = new StreamReader(stream);
            SKCanvas? canvas = null;
            string xml = string.Empty;
            try
            {
                //定义图象
                using var srBmp = new SKBitmap(srs.Length * 33, 50);
                using var srBmp4Output = new SKBitmap(srBmp.Width + 10, srBmp.Height);
                //画图
                canvas = SKSvgCanvas.Create(SKRect.Create(srBmp4Output.Width, srBmp4Output.Height), stream);

                #region Draw the image
                //清空图象
                canvas.Clear(SKColors.AliceBlue);
                //给图象画边框
                canvas.DrawRect(1f, 1f, srBmp4Output.Width - 2, srBmp4Output.Height - 2, new SKPaint { Color = SKColors.Silver });

                //define the width and height of the slice
                float hSLice = srBmp.Height / 20f;
                int iHSlice = Convert.ToInt32(hSLice);
                float wSlice = srBmp.Width / 20f;
                int iWSlice = Convert.ToInt32(wSlice);

                //定义图片弯曲的角度
                int srseedangle = 45;
                //画图片的干扰线
                for (int i = 0; i < 10; i++)
                {
                    int x1 = SrRandom.Next(srBmp4Output.Width);
                    int x2 = SrRandom.Next(srBmp4Output.Width);
                    int y1 = SrRandom.Next(srBmp4Output.Height);
                    int y2 = SrRandom.Next(srBmp4Output.Height);
                    canvas.DrawLine(x1, y1, x2, y2, new SKPaint
                    {
                        Color = SKColors.LightGray,
                        Style = SKPaintStyle.Stroke, // Set the paint style to Stroke
                        StrokeWidth = 1, // Set the stroke width
                    });
                }

                //画噪点
                for (int i = 0; i < 100; i++)
                {
                    canvas.DrawRect(SrRandom.Next(1, srBmp4Output.Width - 2), SrRandom.Next(1, srBmp4Output.Height - 2), 1f, 1f, new SKPaint
                    {
                        Color = SKColors.LightGray,
                        Style = SKPaintStyle.Fill, // Set the paint style to Stroke
                        StrokeWidth = 1, // Set the stroke width
                    });
                }
                //将字符串转化为字符数组
                char[] srchars = srs.ToCharArray();
                //定义字体颜色
                SKColor[] srColors = { SKColors.Black, SKColors.Red, SKColors.DarkBlue, SKColor.Parse("#cc6633"), SKColors.Blue, SKColors.Brown, SKColors.DarkCyan, SKColors.Purple };
                //定义字体
                //string[] srFonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
                int linecolorindex = SrRandom.Next(srColors.Length);
                //定义画笔
                var tf = SKTypeface.FromFile(srFonts[SrRandom.Next(srFonts.Length)]);
                using var paint = new SKPaint()
                {
                    Color = SKColors.Black,
                    //TextSize = 40 - SrRandom.Next(20), // random font size below
                    Typeface = tf,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    TextAlign = SKTextAlign.Center,
                };

                canvas.Translate(0, 0);
                //定义坐标
                float srPointX = srBmp.Width / (srs.Length + 1f);
                float srPointY = hSLice * 17f;
                //循环画出每个字符
                int cindex = -1;
                for (int i = 0, j = srchars.Length; i < j; i++)
                {
                    //填充图形
                    int cindex1 = -1;
                    while (cindex1 == -1 || cindex1 == cindex || linecolorindex == cindex1)
                    {
                        cindex1 = SrRandom.Next(srColors.Length);
                    }
                    cindex = cindex1;
                    paint.Color = srColors[cindex];

                    //定义倾斜角度
                    float srangle = SrRandom.Next(-srseedangle, srseedangle);
                    //倾斜
                    if (i == 0)
                    {
                        canvas.Translate(srPointX / 2f, srPointY);
                    }
                    else
                    {
                        canvas.Translate(srPointX, srPointY);
                    }
                    canvas.RotateDegrees(srangle);

                    //填充字符
                    float x = 0;
                    int y = 0;
                    x = 0;// SrRandom.Next(-3, 3);
                    y = SrRandom.Next(-8, 2);
                    paint.TextSize = 44 - SrRandom.Next(7) * 2;
                    logger.LogDebug($"x: {x}, y: {y}, char: {srchars[i]}, size: {paint.TextSize}");
                    SKPath textPath = paint.GetTextPath(srchars[i].ToString(), x, y);
                    canvas.DrawPath(textPath, paint);
                    //回归正常
                    canvas.RotateDegrees(-srangle);
                    canvas.Translate(3f, -srPointY);
                }
                //return canvas to the start point
                canvas.Translate(-srPointX / 2f - srPointX * (srs.Length - 1) - 5f * 3f, 0f);

                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = SrRandom.Next(srBmp4Output.Width);
                    int y = SrRandom.Next(srBmp4Output.Height);
                    byte alpha = (byte)SrRandom.Next(256);
                    byte red = (byte)SrRandom.Next(256);
                    byte green = (byte)SrRandom.Next(256);
                    byte blue = (byte)SrRandom.Next(256);

                    SKColor color = new(red, green, blue, alpha);
                    canvas.DrawRect(x, y, 1f, 1f, new SKPaint
                    {
                        Color = color,
                        Style = SKPaintStyle.Fill, // Set the paint style to Stroke
                        StrokeWidth = 1, // Set the stroke width
                    });
                }

                //画一条图片的前景干扰线
                float x3 = SrRandom.Next(-5, iWSlice * 3);
                float x4 = wSlice * (20 + SrRandom.Next(3));
                float y3 = SrRandom.Next(srBmp.Height);
                float y4;
                if (y3 > srBmp.Height / 2)
                {
                    y4 = SrRandom.Next(srBmp.Height / 2);
                }
                else
                {
                    y4 = srBmp.Height - SrRandom.Next(srBmp.Height / 2);
                }
                canvas.DrawLine(x3, y3, x4, y4, new SKPaint
                {
                    Color = srColors[SrRandom.Next(srColors.Length)],
                    Style = SKPaintStyle.Stroke, // Set the paint style to Stroke
                    StrokeWidth = 1, // Set the stroke width
                });

                using var paintPath = new SKPaint
                {
                    Color = srColors[SrRandom.Next(srColors.Length)],
                    Style = SKPaintStyle.Stroke, // Set the paint style to Stroke
                    StrokeWidth = 1, // Set the stroke width
                };

                using var path = new SKPath();
                // Define the properties of the wave.
                float amplitude = hSLice * 2f + SrRandom.Next(Convert.ToInt32(hSLice * 5f));  // The height of the wave.
                float period = wSlice * 10f + SrRandom.Next(Convert.ToInt32(wSlice * 8f));    // The distance between wave peaks.
                float phase = SrRandom.Next(-100, 100);       // The horizontal offset of the wave.
                logger.LogDebug($"amplitude: {amplitude}, period: {period}, phase: {phase}");

                bool SineOrCosine = false;
                if (IsNegative == 1) SineOrCosine = true;
                // Calculate the points along the wave.
                for (int x = iWSlice; x < srBmp.Width + 15; x++)
                {
                    float y = 0;
                    if (SineOrCosine)
                        y = amplitude * (float)Math.Sin(2 * Math.PI * x / period + phase);
                    else
                        y = amplitude * (float)Math.Cos(2 * Math.PI * x / period + phase);
                    y += srBmp.Height / 2;  // Offset the y-coordinate so the wave is in the middle of the bitmap.

                    if (x == iWSlice)
                    {
                        path.MoveTo(x, y);
                    }
                    else
                    {
                        path.LineTo(x, y);
                    }
                }

                // Draw the wave.
                canvas.DrawPath(path, paintPath);
                #endregion
                canvas.Flush();
                canvas.Save();

                stream.Position = 0;
                xml = await reader.ReadToEndAsync();
                xml += "</svg>";
            }
            catch
            {
                throw;
            }
            finally
            {
                canvas?.Dispose();
                reader.Dispose();
                stream.Dispose();
            }

            //输出svg
            return await Task.FromResult(xml);
        }

        /// <summary>
        /// 创建随机码图片
        /// </summary>
        /// <param name="srs">需生成图片的字符串</param>
        /// <returns></returns>
        public static byte[] CreateImageOutput(string srs, string[] srFonts, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("RRCaptchaClass.CreateImageOutput");
            //定义图象
            using var srBmp = new SKBitmap(srs.Length * 35, 50);
            using var srBmp4Output = new SKBitmap(srBmp.Width + 35, srBmp.Height);
            //画图
            using var canvas = new SKCanvas(srBmp4Output);
            #region Draw the image
            //清空图象
            canvas.Clear(SKColors.AliceBlue);
            //给图象画边框
            canvas.DrawRect(0f, 0f, srBmp4Output.Width, srBmp4Output.Height, new SKPaint { Color = SKColors.Silver });

            //define the width and height of the slice
            float hSLice = srBmp.Height / 20;
            int iHSlice = Convert.ToInt32(hSLice);
            float wSlice = srBmp.Width / 20;
            int iWSlice = Convert.ToInt32(wSlice);

            //定义图片弯曲的角度
            int srseedangle = 45;
            //画图片的干扰线
            for (int i = 0; i < 10; i++)
            {
                int x1 = SrRandom.Next(srBmp4Output.Width);
                int x2 = SrRandom.Next(srBmp4Output.Width);
                int y1 = SrRandom.Next(srBmp4Output.Height);
                int y2 = SrRandom.Next(srBmp4Output.Height);
                canvas.DrawLine(x1, y1, x2, y2, new SKPaint { Color = SKColors.LightGray });
            }

            //画噪点
            for (int i = 0; i < 100; i++)
            {
                canvas.DrawRect(SrRandom.Next(1, srBmp4Output.Width - 2), SrRandom.Next(1, srBmp4Output.Height - 2), 1f, 1f, new SKPaint { Color = SKColors.LightGray });
            }
            //将字符串转化为字符数组
            char[] srchars = srs.ToCharArray();
            //定义字体颜色
            SKColor[] srColors = [SKColors.Black, SKColors.Red, SKColors.DarkBlue, SKColor.Parse("#cc6633"), SKColors.Blue, SKColors.Brown, SKColors.DarkCyan, SKColors.Purple];
            //定义字体
            //string[] srFonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            int linecolorindex = SrRandom.Next(srColors.Length);
            //定义画笔
            var tf = SKTypeface.FromFile(srFonts[SrRandom.Next(srFonts.Length)]);
            using var paint = new SKPaint()
            {
                Color = SKColors.Black,
                //TextSize = 40 - SrRandom.Next(20), // random font size below
                Typeface = tf,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
            };

            canvas.Translate(5, 0);
            //定义坐标
            float srPointX = srBmp.Width / (srs.Length + 1f);
            float srPointY = hSLice * 17f;
            //循环画出每个字符
            int cindex = -1;
            for (int i = 0, j = srchars.Length; i < j; i++)
            {
                //填充图形
                int cindex1 = -1;
                while (cindex1 == -1 || cindex1 == cindex || linecolorindex == cindex1)
                {
                    cindex1 = SrRandom.Next(srColors.Length);
                }
                cindex = cindex1;
                paint.Color = srColors[cindex];

                //定义倾斜角度
                float srangle = SrRandom.Next(-srseedangle, srseedangle);
                //倾斜
                canvas.Translate(srPointX, srPointY);
                canvas.RotateDegrees(srangle);

                //填充字符
                float x = 0;
                int y = 0;
                x = SrRandom.Next(3) * IsNegative;
                y = SrRandom.Next(8) * IsNegative;
                paint.TextSize = 44 - SrRandom.Next(7) * 2;
                logger.LogDebug($"x: {x}, y: {y}, char: {srchars[i]}, size: {paint.TextSize}");
                canvas.DrawText(srchars[i].ToString(), x, y, paint);
                //回归正常
                canvas.RotateDegrees(-srangle);
                canvas.Translate(5, -srPointY);
            }
            //return canvas to the start point
            canvas.Translate(-srPointX * srs.Length - 5f * 5f, 0f);

            //画图片的前景干扰点
            for (int i = 0; i < 100; i++)
            {
                int x = SrRandom.Next(srBmp4Output.Width);
                int y = SrRandom.Next(srBmp4Output.Height);
                byte alpha = (byte)SrRandom.Next(256);
                byte red = (byte)SrRandom.Next(256);
                byte green = (byte)SrRandom.Next(256);
                byte blue = (byte)SrRandom.Next(256);

                SKColor color = new(red, green, blue, alpha);
                canvas.DrawRect(x, y, 1f, 1f, new SKPaint
                {
                    Color = color,
                    Style = SKPaintStyle.Fill, // Set the paint style to Stroke
                    StrokeWidth = 1, // Set the stroke width
                });
            }

            //画一条图片的前景干扰线
            float x3 = wSlice + SrRandom.Next(iWSlice * 3);
            float x4 = wSlice * (20 + SrRandom.Next(3));
            float y3 = SrRandom.Next(srBmp.Height);
            float y4;
            if (y3 > srBmp.Height / 2)
            {
                y4 = SrRandom.Next(srBmp.Height / 2);
            }
            else
            {
                y4 = srBmp.Height - SrRandom.Next(srBmp.Height / 2);
            }
            canvas.DrawLine(x3, y3, x4, y4, new SKPaint { Color = srColors[SrRandom.Next(srColors.Length)] });

            using var paintPath = new SKPaint
            {
                Color = srColors[SrRandom.Next(srColors.Length)],
                Style = SKPaintStyle.Stroke, // Set the paint style to Stroke
                StrokeWidth = 1, // Set the stroke width
            };

            using var path = new SKPath();
            // Define the properties of the wave.
            float amplitude = hSLice * 2f + SrRandom.Next(Convert.ToInt32(hSLice * 5f));  // The height of the wave.
            float period = wSlice * 10f + SrRandom.Next(Convert.ToInt32(wSlice * 8f));    // The distance between wave peaks.
            float phase = SrRandom.Next(-100, 100);       // The horizontal offset of the wave.
            logger.LogDebug($"amplitude: {amplitude}, period: {period}, phase: {phase}");

            bool SineOrCosine = false;
            if (IsNegative == 1) SineOrCosine = true;
            // Calculate the points along the wave.
            for (int x = iWSlice; x < srBmp.Width + 15; x++)
            {
                float y = 0;
                if (SineOrCosine)
                    y = amplitude * (float)Math.Sin(2 * Math.PI * x / period + phase);
                else
                    y = amplitude * (float)Math.Cos(2 * Math.PI * x / period + phase);
                y += srBmp.Height / 2;  // Offset the y-coordinate so the wave is in the middle of the bitmap.

                if (x == iWSlice)
                {
                    path.MoveTo(x, y);
                }
                else
                {
                    path.LineTo(x, y);
                }
            }

            // Draw the wave.
            canvas.DrawPath(path, paintPath);
            #endregion

            //保存图片数据
            using var image = SKImage.FromBitmap(srBmp4Output);
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 100);
            using var stream = new MemoryStream();
            data.SaveTo(stream);

            //输出图片流
            return stream.ToArray();
        }
    }
}
