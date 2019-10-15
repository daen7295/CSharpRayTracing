using System;

namespace HeavyRayTracing
{
    internal class Vector
    {
        // 6 parts of vector, three for starting position and 3 for angle
        public double xAng, yAng, zAng, xPos, yPos, zPos;
        public Vector(double x, double y, double z, double xp = 0, double yp = 0, double zp = 0)
        {
            // initializes vector components, default position is 0 because some vectors dont need it
            this.xAng = x;
            this.yAng = y;
            this.zAng = z;
            this.xPos = xp;
            this.yPos = yp;
            this.zPos = zp;
        }

        // returns the dot product of two vectors
        public double DotProduct(Vector other)
        {
            return xAng * other.xAng + yAng * other.yAng + zAng * other.zAng;
        }

        // returns the magnitude of a vector
        public double Magnitude()
        {
            return Math.Sqrt(Math.Pow(xAng, 2) + Math.Pow(yAng, 2) + Math.Pow(zAng, 2));
        }
        
        // returns the cross product of two vectors
        public Vector CrossProduct(Vector other)
        {
            return new Vector(yAng * other.zAng - other.yAng * zAng, other.xAng * zAng - xAng * other.zAng, xAng * other.yAng - other.xAng * yAng);
        }
    }
}