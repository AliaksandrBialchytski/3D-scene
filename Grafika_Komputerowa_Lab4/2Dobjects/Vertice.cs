using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa_Lab4
{
    public class Vertice
    {
        public float x;
        public float y;
        public float z;
        public Color color;
        public Vector3 globalPos;
        public Vector3 normal;
        public Vertice(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
            color = Color.Red;
        }
        public bool ContainPixel(int X, int Y)
        {
            if (x - 2 <= X & X <= x + 2 & y - 2 <= Y & y + 2 >= Y)
                return true;
            return false;
        }
        public void Draw(Bitmap DrawArea)
        {
            Graphics g = Graphics.FromImage(DrawArea);
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(color);
            g.FillRectangle(myBrush,(int)( x - 2),(int)( y - 2), 5, 5);
            myBrush.Dispose();
            g.Dispose();
            return;
        }
    }
}
