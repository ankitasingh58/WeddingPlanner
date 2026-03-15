using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;


namespace Wedding_Planner.App_Code
{
    public class HumanDetection
    {

        internal class CaptchaDetails
        {
            internal string CaptchaImageName, FolderName, CaptchaCode;
        }
        internal string GetRandomCode()
        {

            string code = "";
            char ch;
            Random r = new Random();
            int x = r.Next(1, 10);
            if (x % 2 == 0)
            {
                ch = (char)r.Next(65, 90);
                code = code + ch;
            }

            ch = (char)r.Next(100, 120);
            code = code + ch;
            ch = (char)r.Next(70, 90);
            code = code + ch;
            ch = (char)r.Next(49, 57);
            code = code + ch;
            ch = (char)r.Next(105, 120);
            code = code + ch;
            ch = (char)r.Next(75, 90);
            code = code + ch;
            int n = r.Next(1, 6);
            if (n % 2 == 0)
            {

                ch = (char)r.Next(52, 57);
                code = code + ch;
            }
            int cn = r.Next(1, 50);
            if (cn % 2 == 0)
            {

                ch = (char)r.Next(110, 120);
                code = code + ch;
            }

            return code;


        }
        internal CaptchaDetails GenerateNewCaptcha()
        {

            CaptchaDetails cd = new CaptchaDetails();
            SolidBrush sbMaroon = new SolidBrush(Color.Maroon);
            Pen BluePen = new Pen(Color.Blue);
            Font f = new Font("Vardana", 16, FontStyle.Strikeout);
            Bitmap bmp = new Bitmap(150, 40);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.LightPink);
            g.DrawRectangle(BluePen, 2, 2, 145, 35);
            cd.CaptchaCode = GetRandomCode();
            g.DrawString(cd.CaptchaCode, f, sbMaroon, 24, 5);
            g.Flush();
            cd.CaptchaImageName = Path.GetRandomFileName() + ".jpg";
            cd.FolderName = "Captcha_Images";
            string FolderPath = HttpContext.Current.Server.MapPath("~/Content/" + cd.FolderName);
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            bmp.Save(FolderPath + "/" + cd.CaptchaImageName, ImageFormat.Jpeg);
            return cd;
        }
    }
}