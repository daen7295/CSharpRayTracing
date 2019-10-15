using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyRayTracing
{
    class Settings
    {
        public static int height, width, margin;
        public static double xFov, yFov, gravity;
        public static Color skylight;

        // initialize all the useful global variables
        public static void Setup()
        {
            // vertical and horizontal field of view in degrees
            xFov = 100;
            yFov = 100;
            // vertical and horizontal resolution
            height = 1000;
            width = 1000;
            // placeholder for giving light gravity
            gravity = 0;
            // color of the sky
            skylight = Color.White;
            // pixels of margin for the display
            margin = 2;
        }
    }
}
