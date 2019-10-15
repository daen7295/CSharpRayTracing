using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HeavyRayTracing
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // initialize settings
            Settings.Setup();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // start Form1 and show image rendered by GenerateBitMap
            Application.Run(new Form1(GenerateBitMap()));
        }

        static Bitmap GenerateBitMap()
        {
            // create the render bitmap
            Bitmap disp = new Bitmap(Settings.width, Settings.height);

            // for each pixel in the bitmap, shoot a ray and figure out what color it should be
            for (int x = 0; x < disp.Width; x++)
                for (int y = 0; y < disp.Height; y++)
                {
                    // create a new vector based on what pixel it is and set that pixel to the right color
                    // all the math is just figuring out the vector angle based on the specific pixel
                    Vector ray = new Vector(Math.Tan(((x - (0.5 * Settings.width)) * (Settings.xFov / Settings.width)) / 180 * Math.PI), Math.Tan((((0.5 * Settings.height) - y) * (Settings.yFov / Settings.height)) / 180 * Math.PI), 1);
                    disp.SetPixel(x, y, Scene.ShootRay(ray));
                }

            // return the complete render
            return disp;
        }
    }
}
