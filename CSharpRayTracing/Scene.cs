using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyRayTracing
{
    class Scene
    {
        // list of all the surfaces to render
        // IMPORTANT
        // Sphere takes Sphere(x, y, z, radius, color, shinyness)
        // Plane is really a triangle and takes Plane(x, y, z, vector1(x, y, z), vector2(x, y, z), color, shinyness)
        // shinyness is a fractional percentage, ie: 0 < shinyness < 1
        public static List<ISurface> stuff = new List<ISurface>() {

        };

        // interface for all objects that the light bounces off of
        public interface ISurface
        {
            // method that returns the distance to the surface and the reflected vector
            (double, Vector) Touch(Vector vector);
            // returns the reflected color of the surface
            Color ColorMod(Color color);
        }
        
        // returns the color that a ray of light is, uses recursion to send rays until they do not intersect with a surface (hit the sky)
        public static Color ShootRay(Vector vector)
        {
            // array of the disctances to each object
            double[] distances = new double[stuff.Count];
            // array for the resultant vectors from encountering each object
            Vector[] vectors = new Vector[stuff.Count];
            // fills the arrays with distances and resultant vectors (-1 distance if no contact)
            for (int i = 0; i < stuff.Count; i++)
                (distances[i], vectors[i]) = stuff[i].Touch(vector);

            // finds the index of the closest object (-1 if it doesn't hit any)
            double minDist;
            int hitIndex = -1;
            if (distances.Length > 0)
            {
                minDist = distances[0];
                for (int i = 0; i < distances.Length; i++)
                    if (distances[i] > 0 && (distances[i] <= minDist || minDist < 0))
                    {
                        minDist = distances[i];
                        hitIndex = i;
                    }
            }
            // if it hits an object, shoot another ray and return the color, but modified by the albedo of the surface
            if (hitIndex >= 0)
                return stuff[hitIndex].ColorMod(ShootRay(vectors[hitIndex]));
            // return white if it does not hit another surface
            else
                return Settings.skylight;
        }
    }
}
