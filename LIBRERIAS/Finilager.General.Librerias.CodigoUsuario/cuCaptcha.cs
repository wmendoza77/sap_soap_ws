using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using Finilager.General.Librerias.EntidadesNegocio;


namespace Finilager.General.Librerias.CodigoUsuario
{
    public class cuCaptcha
    {
        private static char generarCaracterAzar()
        {
            //del 65 al 90 son las letras mayúsculas
            //del 97 al 122 son las letras en minusculas
            //del 48 al 57 son numeros
            Random oAzar = new Random();
            int a = 0, b = 0, tipo = oAzar.Next(3) + 1;
            if (tipo == 1)
            { a = 48; b = 9; }
            if (tipo == 2)
            { a = 65; b = 25; }
            if (tipo == 3)
            { a = 97; b = 25; }
            oAzar = new Random();
            int n = oAzar.Next(b) + a;
            char c = (char)n;
            System.Threading.Thread.Sleep(15);
            return c;
        }

        public static enCaptcha CrearCaptcha()
        {
            enCaptcha oenCaptcha = new enCaptcha();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                sb.Append(generarCaracterAzar());
            }
            oenCaptcha.Codigo = sb.ToString();
            Rectangle rec = new Rectangle(0, 0, 200, 80);
            Bitmap bmp = new Bitmap(200, 80);
            Graphics grafico = Graphics.FromImage(bmp);
            LinearGradientBrush degradado = new LinearGradientBrush(rec, Color.Aqua, Color.Blue, LinearGradientMode.BackwardDiagonal);
            grafico.FillRectangle(degradado,rec);
            grafico.DrawString(sb.ToString(), new Font("Arial", 30), Brushes.White, 10, 10);
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Png);
                oenCaptcha.Imagen = ms.ToArray();
            }
            return oenCaptcha;
        }
    }
}
