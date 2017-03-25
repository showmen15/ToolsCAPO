using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApplication7
{
    public class Vector3D 
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double Z { get; set; }

        public Vector3D(double x, double y,double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
