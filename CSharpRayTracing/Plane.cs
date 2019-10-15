using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HeavyRayTracing.Scene;

namespace HeavyRayTracing
{
    class Plane : ISurface
    {
        public readonly Vector vector1, vector2;
        public readonly Color color;
        public readonly double shinyness;

        // takes a point of origin and two vector and creates a plane that is the triangle formed by them
        public Plane(double x, double y, double z, Vector vector1, Vector vector2, Color color, double shinyness)
        {
            // the two vectors have positions equal to the origin point
            this.vector1 = new Vector(vector1.xAng, vector1.yAng, vector1.zAng, x, y, z);
            this.vector2 = new Vector(vector2.xAng, vector2.yAng, vector2.zAng, x, y, z);
            this.color = color;
            this.shinyness = shinyness;
        }
        // determines if the vector hits the plane and returns the distance to the plane and the 
        public (double, Vector) Touch(Vector vector)
        {
            // normal vector for the plane
            Vector normal = vector1.CrossProduct(vector2);
            // distance from the light vector to the plane
            double lambda = (normal.xAng * vector1.xPos + normal.yAng * vector1.yPos + normal.zAng * vector1.zPos - vector.xPos * normal.xAng - vector.yPos) / vector.DotProduct(normal);
            // point of intersection between the plane and the light vector
            double xBit = vector.xAng * lambda + vector.xPos;
            double yBit = vector.yAng * lambda + vector.yPos;
            double zBit = vector.zAng * lambda + vector.zPos;

            //if it is within the triangle
            Vector u = new Vector(vector1.xAng + vector1.xPos - xBit, vector1.yAng + vector1.yPos - yBit, vector1.zAng + vector1.zPos - zBit, xBit, yBit, zBit);
            Vector v = new Vector(vector2.xAng + vector2.xPos - xBit, vector2.yAng + vector2.yPos - yBit, vector2.zAng + vector2.zPos - zBit, xBit, yBit, zBit);
            double area = vector1.CrossProduct(vector2).Magnitude();
            if (Math.Abs(vector1.CrossProduct(u).Magnitude() + vector2.CrossProduct(v).Magnitude() + u.CrossProduct(v).Magnitude() - area) < 0.03 * area)
            {
                // if it is return the distance and reflected vector
                double distance = vector.Magnitude() * lambda;
                double scalar = 2 * vector.DotProduct(normal) / normal.Magnitude();
                return (distance, new Vector(vector.xAng - scalar * normal.xAng, vector.yAng - scalar * normal.yAng, vector.zAng - scalar * normal.zAng, xBit, yBit, zBit));
            }
            else
                // if not return negative distance and an empty vector
                return (-1, new Vector(0, 0, 0, 0, 0, 0));
        }

        public Color ColorMod(Color light)
        {
            return Color.FromArgb(Convert.ToInt32(shinyness * light.R + (1 - shinyness) * light.R * (color.R / Color.White.R)), Convert.ToInt32(shinyness * light.G + (1 - shinyness) * light.G * (color.G / Color.White.G)), Convert.ToInt32(shinyness * light.B + (1 - shinyness) * light.B * (color.B / Color.White.B)));
        }
    }
}
