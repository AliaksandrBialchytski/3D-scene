using System.Numerics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa_Lab4
{
    class Square : Figure
    {
        double edgeLength;
        public Square(float x, float y, float z, int edgeLength)
        {
            Center = new Vertice(x, y, z);
            p = new Vector4[4];
            pPrim = new Vector4[4];
            colors = new Color[1] { Color.White };
            this.edgeLength = edgeLength;
            orderOfVertices = new int[2, 3]
                {
                {0,2,1},
                {0,3,2},
                };
            InitializeVertices();
        }
        public override void InitializeVertices()
        {
            int temp;
            if (1 == edgeLength % 2.0)
                temp = (int)(edgeLength / 2 + 1);
            else
                temp = (int)(edgeLength / 2);
            verticesStatic.Add(new Vertice(-temp, 0, -temp));
            verticesStatic.Add(new Vertice(-temp, 0, +temp));
            verticesStatic.Add(new Vertice(+temp, 0, +temp));
            verticesStatic.Add(new Vertice(+temp, 0, -temp));
        }
    }
}
