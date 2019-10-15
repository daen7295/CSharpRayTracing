using System;
using System.Drawing;
using static HeavyRayTracing.Scene;

namespace HeavyRayTracing
{
    internal class Sphere : ISurface
    {
        // x, y, and z are the coordinates for the center of the sphere, radius is the radius
        private readonly double x;
        private readonly double y;
        private readonly double z;
        private readonly double radius;
        // shinyness is a fractional percentage for how much light is reflected regardless of it or the object's color
        private readonly double shinyness;
        // the color of the sphere
        public readonly Color color;
        
        // initialize all of the sphere's properties
        public Sphere(double x, double y, double z, double radius, Color color, double shinyness)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.radius = radius;
            this.color = color;
            this.shinyness = shinyness;
        }

        // takes a vector and returns the distance it travels to the sphere and the vector reflected off the sphere
        // if the vector does not hit the sphere it returns -1 distance and an empty vector
        public (double, Vector) Touch(Vector vector)
        {
            // scalar for the vector to be at the closest point to the center of the sphere
            // based on the plane normal to the vector and coincedent with the center of the sphere
            double lambda = (vector.xAng * x + vector.yAng * y + vector.zAng * z - vector.xAng * vector.xPos - vector.yAng * vector.yPos - vector.zAng * vector.zPos) / (vector.xAng * vector.xAng + vector.yAng * vector.yAng + vector.zAng * vector.zAng);
            // distance between the center of the sphere and the closest point of the vector
            double distance = Math.Sqrt(Math.Pow(vector.xAng * lambda + vector.xPos - x, 2) + Math.Pow(vector.yAng * lambda + vector.yPos - y, 2) + Math.Pow(vector.zAng * lambda + vector.zPos - z, 2));

            // if the vector does not hit the sphere
            if (distance > radius || lambda <= 0)
                // return -1 distance and an empty vector
                return (-1, new Vector(0, 0, 0, 0, 0, 0));
            // if it does hit the sphere
            else
            {
                // calculate the scalar to the two intersection points (thanks wolfram alpha)
                double lam1 = (Math.Sqrt(Math.Pow((-2 * vector.xAng * x + 2 * vector.xPos * vector.xAng + 2 * vector.yAng * vector.yPos - 2 * vector.yAng * y + 2 * vector.zAng * vector.zPos - 2 * vector.zAng * z), 2) - 4 * (Math.Pow(vector.xAng, 2) + Math.Pow(vector.yAng, 2) + Math.Pow(vector.zAng, 2)) * (Math.Pow(vector.yPos, 2) - 2 * vector.yPos * y + Math.Pow(vector.zPos, 2) - 2 * vector.zPos * z + Math.Pow(x, 2) - 2 * vector.xPos * x + Math.Pow(y, 2) + Math.Pow(z, 2) - Math.Pow(radius, 2) + Math.Pow(vector.xPos, 2))) + 2 * vector.xAng * x - 2 * vector.xPos * vector.xAng - 2 * vector.yAng * vector.yPos + 2 * vector.yAng * y - 2 * vector.zAng * vector.zPos + 2 * vector.zAng * z) / (2 * (Math.Pow(vector.xAng, 2) + Math.Pow(vector.yAng, 2) + Math.Pow(vector.zAng, 2)));
                double lam2 = ((-1 * Math.Sqrt(Math.Pow((-2 * vector.xAng * x + 2 * vector.xPos * vector.xAng + 2 * vector.yAng * vector.yPos - 2 * vector.yAng * y + 2 * vector.zAng * vector.zPos - 2 * vector.zAng * z), 2) - 4 * (Math.Pow(vector.xAng, 2) + Math.Pow(vector.yAng, 2) + Math.Pow(vector.zAng, 2)) * (Math.Pow(vector.yPos, 2) - 2 * vector.yPos * y + Math.Pow(vector.zPos, 2) - 2 * vector.zPos * z + Math.Pow(x, 2) - 2 * vector.xPos * x + Math.Pow(y, 2) + Math.Pow(z, 2) - Math.Pow(radius, 2) + Math.Pow(vector.xPos, 2)))) + 2 * vector.xAng * x - 2 * vector.xPos * vector.xAng - 2 * vector.yAng * vector.yPos + 2 * vector.yAng * y - 2 * vector.zAng * vector.zPos + 2 * vector.zAng * z) / (2 * (Math.Pow(vector.xAng, 2) + Math.Pow(vector.yAng, 2) + Math.Pow(vector.zAng, 2)));
                // the real scalar is the shorter one
                lambda = (lam1 < lam2) ? lam1 : lam2;

                // find the coordinates of the intersection point
                double xBit = vector.xAng * lambda + vector.xPos;
                double yBit = vector.yAng * lambda + vector.yPos;
                double zBit = vector.zAng * lambda + vector.zPos;

                // distance from the start of the vector to where it intersects the sphere
                distance = vector.Magnitude() * lambda;

                // return the distance of the vector and the new reflected vector
                Vector normal = new Vector(xBit - x, yBit - y, zBit - z);
                double scalar = 2 * vector.DotProduct(normal) / normal.Magnitude();
                return (distance, new Vector(vector.xAng - scalar * normal.xAng, vector.yAng - scalar * normal.yAng, vector.zAng - scalar * normal.zAng, xBit, yBit, zBit));
            }
        }

        // change the color of the light based on its own color and its shinyness
        public Color ColorMod(Color light)
        {
            return Color.FromArgb(Convert.ToInt32(shinyness * light.R + (1 - shinyness) * light.R * (color.R / Color.White.R)), Convert.ToInt32(shinyness * light.G + (1 - shinyness) * light.G * (color.G / Color.White.G)), Convert.ToInt32(shinyness * light.B + (1 - shinyness) * light.B * (color.B / Color.White.B)));
        }
    }
}