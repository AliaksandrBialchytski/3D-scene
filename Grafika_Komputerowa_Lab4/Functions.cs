using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa_Lab4
{
    public static class Functions
    {
        public static Vector4 Multiply(Matrix4x4 matrix, Vector4 vector)
        {
            return new Vector4(
            
                matrix.M11 * vector.X+matrix.M12*vector.Y+matrix.M13*vector.Z+matrix.M14*vector.W,
                matrix.M21 * vector.X+matrix.M22*vector.Y+matrix.M23*vector.Z+matrix.M24*vector.W,
                matrix.M31 * vector.X+matrix.M32*vector.Y+matrix.M33*vector.Z+matrix.M34*vector.W,
                matrix.M41 * vector.X+matrix.M42*vector.Y+matrix.M43*vector.Z+matrix.M44*vector.W
            );
        }
        public static Vector3 Normalize(Vector3 vector)
        {
            float length = (float)Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2) +
                Math.Pow(vector.Z, 2));
            if (length == 0)
                length = 1;
            return new Vector3(
                vector.X/length,
                vector.Y/length,
                vector.Z/length
            );
        }
        public static float DotProduct(Vector3 vector1, Vector3 vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
        }
        public static Vector3 CountInterpolation(float Square, List<Vertice> vertices,
                       Vector3 vect1, Vector3 vect2, Vector3 vect3, int x, int y)
        {
            Vertice vertP = new Vertice(x, y,0);
            float a = SquareOfTriangle(vertices[1], vertP, vertices[2]) / Square;
            float temp = SquareOfTriangle(vertices[0], vertP, vertices[2]);
            float b = SquareOfTriangle(vertices[0], vertP, vertices[2]) / Square;
            float c = SquareOfTriangle(vertices[0], vertP, vertices[1]) / Square;
            Vector3 tempVector = a * vect1 + b * vect2 + c * vect3;
            return new Vector3(tempVector.X, tempVector.Y, tempVector.Z);

        }
        private static float SquareOfTriangle(Vertice vert1, Vertice vert2, Vertice vert3)
        {
            double edge1X = vert2.x - vert1.x;
            double edge1Y = vert2.y - vert1.y;
            double len1 = Math.Sqrt(Math.Pow(edge1X, 2) + Math.Pow(edge1Y, 2));
            edge1X /= len1; edge1Y /= len1;

            double edge2X = vert2.x - vert3.x;
            double edge2Y = vert2.y - vert3.y;
            double len2 = Math.Sqrt(Math.Pow(edge2X, 2) + Math.Pow(edge2Y, 2));
            edge2X /= len2; edge2Y /= len2;
            double cos = edge1X * edge2X + edge1Y * edge2Y;
            double sin = Math.Sqrt(1 - Math.Pow(cos, 2));
            return (float) (0.5 * len1 * len2 * sin);
        }
        public static double CountInterpolationZ(double Square, List<Vertice> vertices,
                                        int x, int y)
        {
            Vertice vertP = new Vertice(x, y, 0);
            double a = SquareOfTriangle(vertices[1], vertP, vertices[2]) / Square;
            double temp = SquareOfTriangle(vertices[0], vertP, vertices[2]);
            double b = SquareOfTriangle(vertices[0], vertP, vertices[2]) / Square;
            double c = SquareOfTriangle(vertices[0], vertP, vertices[1]) / Square;
            return a * vertices[0].z + b * vertices[1].z + c * vertices[2].z;
        }

        public static void AddNode(List<Node> nodes, Vertice vert1, Vertice vert2)
        {
            if (vert1.y != vert2.y)
            {
                int yMax; double x; double a;
                yMax = (int)vert1.y;
                a = ((double)(vert2.x - vert1.x)) / ((double)(vert2.y - vert1.y));
                x = (double)vert2.x + a;
                nodes.Add(new Node(yMax, x, a));
            }
        }
        public static void RemoveNode(List<Node> nodes, Vertice vert1, Vertice vert2)
        {
            int yMax; double x; double a;
            yMax = (int)vert2.y;
            a = ((double)(vert2.x - vert1.x)) / ((double)(vert2.y - vert1.y));
            x = (double)vert2.x + a;
            int ind = -1;
            foreach (var node in nodes)
                if (node.Compare(yMax, x, a))
                    ind = nodes.IndexOf(node);
            if (ind >= 0)
                nodes.RemoveAt(ind);
        }
    }
}
