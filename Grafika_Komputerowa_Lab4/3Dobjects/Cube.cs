using System.Numerics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa_Lab4
{
    class Cube : Figure
    {
        double edgeLength;
        public Cube(float x, float y, float z, int edgeLength)
        {
            this.Center = new Vertice(x, y, z);
            this.edgeLength = edgeLength;
            this.p = new Vector4[8];
            this.pPrim = new Vector4[8];
            //colors = new Color[6] { Color.Red, Color.Yellow, Color.Blue, Color.Black, Color.Pink, Color.Green };
            //colors = new Color[6] { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow };
            colors = new Color[6] { Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue };
            orderOfVertices = new int[12, 3]
            {
                {7,3,2},
                {7,6,3},
                {6,1,3},
                {6,5,1},
                {5,0,1},
                {5,4,0},

                {4,2,0},
                {4,7,2},
                {7,4,6},
                {4,5,6},
                {2,0,3},
                {0,1,3},
            };
        InitializeVertices();
        }
        public override void InitializeVertices()
        {
            int temp = (int)Math.Round(edgeLength / 2);
            verticesStatic.Add(new Vertice(- temp,  - temp, - temp));
            verticesStatic.Add(new Vertice(+ temp,  - temp,  - temp));
            verticesStatic.Add(new Vertice(- temp, - temp, + temp));
            verticesStatic.Add(new Vertice(+ temp,  - temp,  + temp));
            verticesStatic.Add(new Vertice(- temp,  + temp, - temp));
            verticesStatic.Add(new Vertice(+ temp,  + temp,  - temp));
            verticesStatic.Add(new Vertice( + temp,  + temp,  + temp));
            verticesStatic.Add(new Vertice(- temp,  + temp, + temp)); ;
        }
    }
}
