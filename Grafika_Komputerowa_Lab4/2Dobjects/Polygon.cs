using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa_Lab4
{
    public abstract class Polygon
    {
        public List<Vertice> vertices;
        public abstract double SquareOfPolygon();
    }
    public class Node
    {
        int ymax;
        public double x;
        double a; //1/m
        public Node(int yMax, double X, double A)
        {
            ymax = yMax;
            x = X;
            a = A;
        }
        public bool Compare(int yMax, double X, double A)
        {
            return ymax == yMax && Math.Round(x) == Math.Round(X) && a == A;
        }
        public void IncreaseX()
        {
            x += a;
        }
    }
}
