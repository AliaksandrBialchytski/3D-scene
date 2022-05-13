using System.Numerics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa_Lab4
{
    class HalfSphere : Figure
    {
        int Radius;
        int countCircles = 8;
        int countVerticesInCircle = 8;
        public HalfSphere(float x, float y, float z, int radius)
        {
            this.Center = new Vertice(x, y, z);
            this.Radius = radius;
            this.top = new Vertice(0, Radius, 0);
            this.p = new Vector4[countCircles * countVerticesInCircle];
            this.pPrim = new Vector4[countCircles * countVerticesInCircle];
            colors = new Color[1] { Color.Red };
            InitializeVertices();
        }
        public override void InitializeVertices()
        {
            int r = (Radius - 1) / countCircles;
            double alfa = 360 / countVerticesInCircle;
            int j; int i;
            
            for (j = 1; j < countCircles + 1; j++)
                for (i = 0; i < countVerticesInCircle; i++)
                {
                    double x = Math.Round(j * r * Math.Sin((double)(i * alfa + j * alfa / 2) * Math.PI / 180));
                    double z = Math.Round(j * r * Math.Cos((double)(i * alfa + j * alfa / 2) * Math.PI / 180));
                    double y = Math.Sqrt(Math.Max(Math.Pow(Radius, 2) - Math.Pow(x, 2) - Math.Pow(z, 2), 0.0));
                    verticesStatic.Add(new Vertice((float)x, (float)y, (float)z));
                }
        }
        public override void InitializeTriangles()
        {
            triangles = new List<Triangle>();
            int k, j, i;
            for (k = 0; k < countVerticesInCircle - 1; k++)
            {
                CheckTriangle(new Triangle(top, vertices[k + 1], vertices[k]));
            }
            CheckTriangle(new Triangle(top, vertices[0], vertices[k]));
            for (j = 0; j < countCircles; j++)
            {
                for (i = 0; i < countVerticesInCircle - 1; i++)
                {
                    if (j < countCircles - 1)
                    {

                        CheckTriangle(new Triangle(vertices[j * countVerticesInCircle + i],
                                            vertices[j * countVerticesInCircle + i + 1],
                                            vertices[(j + 1) * countVerticesInCircle + i]));
                        CheckTriangle(new Triangle(vertices[j * countVerticesInCircle + i + 1],
                                                vertices[(j + 1) * countVerticesInCircle + i + 1],
                                             vertices[(j + 1) * countVerticesInCircle + i]));
                    }
                }
                if (j < countCircles - 1)
                {
                    CheckTriangle(new Triangle(vertices[j * countVerticesInCircle + i],
                                            vertices[j * countVerticesInCircle],
                                            vertices[(j + 1) * countVerticesInCircle + i]));
                    CheckTriangle(new Triangle(vertices[j * countVerticesInCircle],
                                                vertices[(j + 1) * countVerticesInCircle],
                                             vertices[(j + 1) * countVerticesInCircle + i]));
                }
            }
        }
        public override void CheckTriangle(Triangle triangle)
        {
            bool flag = true;
            foreach (var vertice in triangle.vertices)
            {

                if (!(vertice.z >= -1 && vertice.z <= 1))
                    flag = false;
            }
            if (flag)
            {
                Vector3 V1 = triangle.vertices[1].globalPos -
                    triangle.vertices[0].globalPos;
                Vector3 V2 = triangle.vertices[2].globalPos -
                    triangle.vertices[0].globalPos;
                triangle.normal = 
                        Functions.Normalize(Vector3.Cross(V2, V1));
                Vector3 center = new Vector3(Center.x, Center.y, Center.z);
                triangle.vertices[0].normal = 
                    Functions.Normalize(triangle.vertices[0].globalPos - center);
                triangle.vertices[1].normal =
                    Functions.Normalize(triangle.vertices[1].globalPos - center);
                triangle.vertices[2].normal =
                    Functions.Normalize(triangle.vertices[2].globalPos - center);
                triangles.Add(triangle);
            }
        }
    }
}
