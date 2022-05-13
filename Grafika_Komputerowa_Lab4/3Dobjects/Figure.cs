using System.Numerics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grafika_Komputerowa_Lab4.Lights;

namespace Grafika_Komputerowa_Lab4
{
    public enum Shading
    {
        Constant , Phong, Gouraud
    }
    public abstract class Figure
    {
        protected List<Triangle> triangles = new List<Triangle>();
        protected List<Vertice> verticesStatic = new List<Vertice>();
        protected List<Vertice> vertices = new List<Vertice>();
        protected Vertice Center;
        protected Vertice top;
        protected Vector4[] p;
        protected Vector4[] pPrim;
        protected Color[] colors;
        protected int[,] orderOfVertices;
        public virtual void InitializeVertices() { }
        public Vertice GetCenter()
        {
            return this.Center;
        }
        public void MultiplyMatrixes(Matrix4x4 Mmodel, Matrix4x4 Mview,
            Matrix4x4 Mproj, double temp1, double temp2)
        {
            vertices = new List<Vertice>();
            Vector4 temp;
            Vector3 pos;
            float x, y, z;
            for (int i = 0; i < verticesStatic.Count; i++)
            {
                p[i] = new Vector4( verticesStatic[i].x, verticesStatic[i].y, verticesStatic[i].z, 1 );
                temp = Functions.Multiply(Mmodel, p[i]);
                pos = new Vector3(temp.X, temp.Y, temp.Z);
                temp = Functions.Multiply(Mproj, Functions.Multiply(
                    Mview, temp));
                pPrim[i] = new Vector4( temp.X, temp.Y, temp.Z, temp.W);
                x = pPrim[i].X / (pPrim[i].W);
                y = pPrim[i].Y / (pPrim[i].W);
                z = pPrim[i].Z / (pPrim[i].W);
                Vertice vert = new Vertice((int)(-x * temp1 + temp1), (int)(-y * temp2 + temp2), z);
                vert.globalPos = pos;
                vertices.Add(vert);
            }
            if(top!=null)
            {
                Vector4 PP = new Vector4(top.x, top.y, top.z,1);
                temp = Functions.Multiply(Mmodel, PP);
                pos = new Vector3(temp.X, temp.Y, temp.Z);
                temp = Functions.Multiply(Mproj, Functions.Multiply(
                    Mview, temp));
                Vector4 PPrim  = new Vector4(temp.X, temp.Y, temp.Z, temp.W);
                x = PPrim.X / (PPrim.W);
                y = PPrim.Y / (PPrim.W);
                z = PPrim.Z / (PPrim.W);
                top = new Vertice((int)(-x * temp1 + temp1), (int)(-y * temp2 + temp2), z);
                top.globalPos = pos;
            }
            InitializeTriangles();
        }
        public virtual void InitializeTriangles()
        {
            triangles = new List<Triangle>();
            for (int i = 0; i < orderOfVertices.GetLength(0); i++)
            {
                CheckTriangle(new Triangle(vertices[orderOfVertices[i, 0]],
                         vertices[orderOfVertices[i, 1]],
                         vertices[orderOfVertices[i, 2]]));
            }
        }
        public virtual void CheckTriangle(Triangle triangle)
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
                triangle.normal = triangle.vertices[0].normal =
                    triangle.vertices[1].normal = triangle.vertices[2].normal =
                        Functions.Normalize(Vector3.Cross(V2, V1));
                triangles.Add(triangle);
            }
        }
        public void Draw(double[,] zBufor, DirectBitmap DrawArea, Vector3 camPos,
            Shading shading, List<PointLight> pointLights, List<SpotLight> spotLights,
            double fogValue)
        {
            Task[] tasks1 = new Task[triangles.Count];
            int i;
            for (i = 0; i < triangles.Count; i++)
            {
                Triangle temp1 = triangles[i];
                tasks1[i] = Task.Factory.StartNew(() =>
                    FillPolygon(temp1, DrawArea,
                        colors[triangles.IndexOf(temp1) % colors.Length],
                        zBufor,camPos,shading, pointLights, spotLights, fogValue));
            }
            Task.WaitAll(tasks1);
        }
        public void FillPolygon(Triangle triangle, DirectBitmap DrawArea,
            Color color, double[,] zBufor, Vector3 camPos, Shading shading,
            List<PointLight> pointLights, List<SpotLight> spotLights, double fogValue)
        {
           Vector3[] colors = new Vector3[3];
           if (shading == Shading.Gouraud)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector3 colorLightVert = new Vector3(0, 0, 0);
                    Vector3 colorTriangleVert = new Vector3(color.R, color.G, color.B);
                    foreach (var light in pointLights)
                    {
                        colorLightVert = colorLightVert + LightFunctions.CalcPointLight(light,
                             Functions.Normalize(triangle.vertices[i].globalPos - camPos),
                             triangle.vertices[i].normal, triangle.vertices[i].globalPos);
                    }
                    foreach (var light in spotLights)
                    {
                        colorLightVert = colorLightVert + LightFunctions.CalcSpotLight(light,
                             Functions.Normalize(triangle.vertices[i].globalPos - camPos),
                             triangle.vertices[i].normal, triangle.vertices[i].globalPos);
                    }
                    colorLightVert = LightFunctions.ClampColor(colorLightVert);
                    colorTriangleVert = colorLightVert * colorTriangleVert;
                    Color temp = Color.FromArgb(
                                        Math.Max(Math.Min((int)colorTriangleVert.X, 255), 0),
                                         Math.Max(Math.Min((int)colorTriangleVert.Y, 255), 0),
                                         Math.Max(Math.Min((int)colorTriangleVert.Z, 255), 0));
                    colors[i] = new Vector3(temp.R, temp.G, temp.B);
                }
            }

            List<Node> nodes = new List<Node>();
            List<Vertice> list = new List<Vertice>(triangle.vertices);
            list.Sort(delegate (Vertice vert1, Vertice vert2)
            {
                return vert1.y.CompareTo(vert2.y);
            });
            double squareOfPolygon = triangle.SquareOfPolygon();
            int[] ind = new int[list.Count];
            for (int i = 0; i < list.Count; i++)
                ind[i] = triangle.vertices.IndexOf(list[i]);
            int yMin = (int)triangle.vertices[ind[0]].y;
            int yMax = (int)triangle.vertices[ind[ind.Count() - 1]].y;
            int k = 0;
            for (int y = yMin ; y <= yMax; y++)
            {
                foreach (var vert in triangle.vertices)
                    if (triangle.vertices[ind[k]].y == y - 1)
                    {
                        if (ind[k] > 0 && ind[k] < list.Count - 1)
                        {
                            if (triangle.vertices[ind[k] - 1].y >= triangle.vertices[ind[k]].y)
                            {
                                Functions.AddNode(nodes, triangle.vertices[ind[k] - 1],
                                    triangle.vertices[ind[k]]);
                            }
                            else
                            {
                                Functions.RemoveNode(nodes, triangle.vertices[ind[k] - 1], 
                                    triangle.vertices[ind[k]]);
                            }
                            if (triangle.vertices[ind[k] + 1].y >= triangle.vertices[ind[k]].y)
                            {
                                Functions.AddNode(nodes, triangle.vertices[ind[k] + 1],
                                    triangle.vertices[ind[k]]);
                            }
                            else
                            {
                                Functions.RemoveNode(nodes, triangle.vertices[ind[k] + 1],
                                    triangle.vertices[ind[k]]);
                            }
                        }
                        else
                        {
                            if (ind[k] == 0)
                            {
                                if (triangle.vertices[list.Count - 1].y >= triangle.vertices[ind[k]].y)
                                {
                                    Functions.AddNode(nodes, triangle.vertices[list.Count - 1],
                                                    triangle.vertices[ind[k]]);
                                }
                                else
                                {
                                    Functions.RemoveNode(nodes, triangle.vertices[list.Count - 1],
                                                    triangle.vertices[ind[k]]);
                                }
                                if (triangle.vertices[ind[k] + 1].y >= triangle.vertices[ind[k]].y)
                                {
                                    Functions.AddNode(nodes, triangle.vertices[ind[k] + 1], 
                                        triangle.vertices[ind[k]]);
                                }
                                else
                                {
                                    Functions.RemoveNode(nodes, triangle.vertices[ind[k] + 1],
                                        triangle.vertices[ind[k]]);
                                }
                            }
                            else if (ind[k] == list.Count - 1)
                            {
                                if (triangle.vertices[ind[k] - 1].y >= triangle.vertices[ind[k]].y)
                                {
                                    Functions.AddNode(nodes, triangle.vertices[ind[k] - 1], 
                                        triangle.vertices[ind[k]]);
                                }
                                else
                                {
                                    Functions.RemoveNode(nodes, triangle.vertices[ind[k] - 1],
                                        triangle.vertices[ind[k]]);
                                }
                                if (triangle.vertices[0].y >= triangle.vertices[ind[k]].y)
                                {
                                    Functions.AddNode(nodes, triangle.vertices[0],
                                        triangle.vertices[ind[k]]);
                                }
                                else
                                {
                                    Functions.RemoveNode(nodes, triangle.vertices[0], 
                                        triangle.vertices[ind[k]]);
                                }
                            }
                        }
                        k++;
                    }
                nodes.Sort(delegate (Node node1, Node node2)
                {
                    return node1.x.CompareTo(node2.x);
                });
                for (int i = 0; i < nodes.Count; i += 2)
                {
                    for (int j = (int)nodes[i].x; j < nodes[i + 1].x; j++)
                    {
                        if (j > 0 && j < DrawArea.Width && y > 0 && y < DrawArea.Height)
                        {
                            double z = Functions.CountInterpolationZ(squareOfPolygon, triangle.vertices, j, y);
                            if (z <= zBufor[j, y] && z >= -1 && z <= 1)
                            {
                                Vector3 position = Functions.CountInterpolation(
                                    (float)squareOfPolygon, triangle.vertices,
                                    triangle.vertices[0].globalPos, triangle.vertices[1].globalPos,
                                    triangle.vertices[2].globalPos, j, y);
                                Vector3 colorLight = new Vector3(0, 0, 0);
                                Vector3 colorTriangle = new Vector3(color.R, color.G, color.B);
                                switch (shading)
                                {
                                    case Shading.Constant:
                                        {
                                            foreach (var light in pointLights)
                                            {
                                                colorLight = colorLight + 
                                                    LightFunctions.CalcPointLight(light,
                                                     Functions.Normalize(position - camPos),
                                                     triangle.normal, position);
                                            }
                                            foreach(var light in spotLights)
                                            {
                                                colorLight = colorLight + 
                                                    LightFunctions.CalcSpotLight(light,
                                                     Functions.Normalize(position - camPos),
                                                     triangle.normal, position);
                                            }
                                            colorLight = LightFunctions.ClampColor(colorLight);
                                            colorTriangle =  colorTriangle * colorLight;
                                            break;
                                        }
                                    case Shading.Phong:
                                        {
                                            Vector3 vectNormal = Functions.CountInterpolation(
                                                (float)squareOfPolygon, triangle.vertices,
                                                triangle.vertices[0].normal, triangle.vertices[1].normal,
                                                triangle.vertices[2].normal, j, y);
                                            foreach (var light in pointLights)
                                            {
                                                colorLight = colorLight + LightFunctions.CalcPointLight(light,
                                                     Functions.Normalize(position - camPos),
                                                     vectNormal, position);
                                            }
                                            foreach (var light in spotLights)
                                            {
                                                colorLight = colorLight + LightFunctions.CalcSpotLight(light,
                                                     Functions.Normalize(position - camPos),
                                                     vectNormal, position);
                                            }
                                            colorLight = LightFunctions.ClampColor(colorLight);
                                            colorTriangle = colorLight * colorTriangle;
                                            break;
                                        }
                                    case Shading.Gouraud:
                                        {
                                            colorTriangle = Functions.CountInterpolation(
                                                (float)squareOfPolygon, triangle.vertices,
                                                colors[0], colors[1],
                                                colors[2], j, y);
                                            break;
                                        }
                                }
                                float distance = Vector3.Distance(camPos, position);
                                float visibility = (float)Math.Exp(-Math.Pow(distance * fogValue, 1.5));
                                colorTriangle = (float)(1.0 - visibility) * (new Vector3(255, 255, 255)) + visibility * colorTriangle;
                                DrawArea.SetPixel(j, y, Color.FromArgb(
                                    Math.Clamp((int)colorTriangle.X, 0, 255),
                                    Math.Clamp((int)colorTriangle.Y, 0, 255),
                                    Math.Clamp((int)colorTriangle.Z, 0, 255))); 
                                zBufor[j, y] = z;
                            }
                        }
                    }
                }
                foreach (var node in nodes)
                    node.IncreaseX();
            }
        }
    }
}
