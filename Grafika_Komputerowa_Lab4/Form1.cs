using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using Grafika_Komputerowa_Lab4.Lights;

namespace Grafika_Komputerowa_Lab4
{
    public enum Camera
    {
        Static, Watch, Move
    }
    public partial class Form1 : Form
    {
        DirectBitmap DrawArea;
        Timer timer = new Timer();
        List<Cube> cubes;
        List<Square> squares;
        List<HalfSphere> halfSpheres;
        List<PointLight> pointLights;
        List<SpotLight> spotLights;
        Matrix4x4 Mview;
        Matrix4x4 Mproj;
        Vector3 staticCam = new Vector3(3, -5, 5);
        Vector3 moveCam;
        Vector3 staticLookAt;
        Camera camera = Camera.Static;
        Shading shading = Shading.Constant;
        int[,] orderOfVertices;
        double[,] zBufor;
        Color[] colors1 = new Color[6] {Color.Red,Color.Yellow,Color.Blue,Color.Black,Color.Pink,Color.Green };
        Color[] colors2 = new Color[6] { Color.Brown, Color.Yellow, Color.Aquamarine, Color.Black, Color.Pink, Color.Green };
        int FOV;
        int edgeOfCube = 4;
        int edgeOfSquareFloor =4;
        int floorHeight = -30;
        int xCenter, zCenter;
        int xDynamic, zDynamic;
        double a = 0.1; double b = 0.2;
        double minTheta; double dTheta; double theta;
        double alpha;
        double angleDynamic = 0.0;
        double fogValue;
        bool centerToPeriphery = true;
        bool isPoint1;
        bool isPoint2;
        bool isSpot;
        public Form1()
        {
            InitializeComponent();
            cameraComboBox.SelectedItem = "Static";
            shadingComboBox.SelectedItem = "Constant";
            staticLookAt = new Vector3( 0, floorHeight, 10);
            xCenter = 0;
            zCenter = 20;
            xDynamic = 0; zDynamic = 20;
            minTheta = Math.Log(0.2 / a) / b;
            dTheta = (10.0 * Math.PI / 180);
            theta = minTheta + 60 * dTheta;
            timer.Tick += new EventHandler(timerEventProcessor);
            timer.Interval = 50;
            timer.Start();
            FOV = FOVhScrollBar.Value;
            fogValue =((double)fogHScrollBar.Value)/1000;
            isPoint1 = pointLight1CheckBox.Checked;
            isPoint2 = pointLight2CheckBox.Checked;
            isSpot = spotLightCheckBox.Checked;
        }
        private void DrawCube()
        {
            timer.Stop();
            InitializeZBufor();
            InitializeDrawArea();
            InitializeLights();
            InitializeCubes();
            InitializeHalfSpheres();
            InitializeFloor();
            InitializeMatrixes();
            MultiplyMatrixes();
            FillTriangles();
            pictureBox1.Image = DrawArea.Bitmap;
            pictureBox1.Refresh();
            timer.Start();
            return;
        }
        private void InitializeZBufor()
        {
            int rowCount = pictureBox1.Width;
            zBufor = new double[pictureBox1.Width, pictureBox1.Height];
            Task[] tasks1 = new Task[rowCount];
            int i;
            for (i = 0; i < rowCount; i++)
            {
                int k = i;
                tasks1[k] = Task.Factory.StartNew(() => IRowOfZBufor(k));
            }
        }
        private void IRowOfZBufor(int i)
        {
            for (int j = 0; j < pictureBox1.Height; j++)
                zBufor[i, j] = int.MaxValue;
        }

        private void InitializeDrawArea()
        {
            if (DrawArea != null)
                DrawArea.Dispose();
            DrawArea = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
            DrawBackground();
        }
        private void DrawBackground()
        {
            Graphics g = Graphics.FromImage(DrawArea.Bitmap);
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(Color.Gray);
            g.FillRectangle(myBrush, 0, 0, pictureBox1.Width, pictureBox1.Height);
            myBrush.Dispose();
            g.Dispose();
        }
        private void InitializeLights()
        {
            pointLights = new List<PointLight>();
            spotLights = new List<SpotLight>();
            Color color = Color.White;
            Vector3 colorVect = new Vector3((float)((double)color.R) / 255,
                                    (float)((double)color.G) / 255,
                                    (float)((double)color.B) / 255);
            if(isPoint1){
                pointLights.Add(new PointLight(new Vector3(0, -15, 4 * edgeOfCube),
                    1.0, 0.014, 0.0007, (float)0.2 * colorVect, (float)1.0 * colorVect, (float)0.5 * colorVect));
            }
            if (isPoint2)
            {
                color = Color.Yellow;
                colorVect = new Vector3((float)((double)color.R) / 255,
                                       (float)((double)color.G) / 255,
                                       (float)((double)color.B) / 255);

                pointLights.Add(new PointLight(new Vector3(-15 * edgeOfCube, 0, 11 * edgeOfCube),
                    1.0, 0.007, 0.0002, (float)0.2 * colorVect, (float)1.0 * colorVect, (float)1 * colorVect));
            }
            if (isSpot)
            {
                color = Color.White;
                colorVect = new Vector3((float)((double)color.R) / 255,
                                        (float)((double)color.G) / 255,
                                        (float)((double)color.B) / 255);

                colorVect = new Vector3((float)((double)color.R),
                                        (float)((double)color.G),
                                        (float)((double)color.B));
                spotLights.Add(new SpotLight(new Vector3(xDynamic, floorHeight + edgeOfCube, zDynamic),
                     Functions.Normalize(new Vector3((float)Math.Cos(alpha * Math.PI / 180.0),
                     (float)-2.5, (float)Math.Sin(alpha * Math.PI / 180.0))),
                     (float)Math.Cos(Math.PI / 6), (float)Math.Cos(Math.PI / 3),
                     (float)1.0, (float)0.35, (float)0.44,
                     (float)0.2 * colorVect, (float)0.5 * colorVect, (float)0.5 * colorVect));
            }
        }
        private void InitializeCubes()
        {
            cubes = new List<Cube>();
            cubes.Add(new Cube(5*edgeOfCube, floorHeight + 4, 11* edgeOfCube, 8));
            cubes.Add(new Cube(-5* edgeOfCube, floorHeight + edgeOfCube / 2, edgeOfCube, edgeOfCube));
            cubes.Add(new Cube(xDynamic, floorHeight + edgeOfCube / 2, zDynamic, edgeOfCube));
        }
        private void InitializeHalfSpheres()
        {
            halfSpheres = new List<HalfSphere>();
            halfSpheres.Add(new HalfSphere(7 * edgeOfCube, floorHeight, 4 * edgeOfCube, 9));
        }
        private void InitializeFloor()
        {
            squares = new List<Square>();
            double temp = edgeOfSquareFloor / 2;
            for(int i=-20;i<20;i+=1)
                for(int j=0;j<20;j += 1)
                {
                    squares.Add(new Square(i * edgeOfSquareFloor, floorHeight, j * edgeOfSquareFloor, edgeOfSquareFloor));
                }
        }
        private void InitializeMatrixes()
        {
            Vector3 dynamic = new Vector3(xDynamic, 0, zDynamic);
            switch (camera)
            {
                case Camera.Static:
                    {
                        Mview = InitializeViewMatrix(staticCam, staticLookAt);
                        break;
                    }
                case Camera.Watch:
                    {
                        Mview = InitializeViewMatrix(staticCam, dynamic +
                            new Vector3(0, floorHeight - 20 + edgeOfCube, 0));
                        break;
                    }
                case Camera.Move:
                    {
                        Vector3 temp = new Vector3(-5, -5, -5);
                        moveCam = dynamic + temp;
                        Mview = InitializeViewMatrix(moveCam, dynamic +
                            new Vector3(0, floorHeight, 0));
                        break;
                    }
            }
            float n = 1;
            float f = 100;
            float e = (float)(1.0 / Math.Tan((double)FOV * Math.PI / (double)(180*2)));
            float a = (float)((double)pictureBox1.Height / (double)pictureBox1.Width);
            Mproj = new Matrix4x4(   
                e,0,0,0,
                0,e/a,0,0,
                0,0,-(f + n) / (f - n), -2 * f * n / (f - n),
                0,0,-1,0
            );

        }
        private void MultiplyMatrixes()
        {
            Matrix4x4 MFloorRotation = new  Matrix4x4(
                1,0,0,0,
                0,1,0,0,
                0,0,1,0,
                0,0,0,1
                );
            Matrix4x4 MFloorScalling = new Matrix4x4(
               
                1,0,0,0,
                0,1,0,0,
                0,0,1,0,
                0,0,0,1
                );

            Matrix4x4 MCube1Translation =new Matrix4x4(
                
                1,0,0, cubes[0].GetCenter().x,
                0,1,0, cubes[0].GetCenter().y,
                0,0,1, cubes[0].GetCenter().z,
                0,0,0,1
                );
            Matrix4x4 MCube2Translation = new Matrix4x4(
                
                1,0,0, cubes[1].GetCenter().x,
                0,1,0, cubes[1].GetCenter().y,
                0,0,1, cubes[1].GetCenter().z,
                0,0,0,1
                );
            Matrix4x4 MCube3Translation =new Matrix4x4(
                
                1,0,0, cubes[2].GetCenter().x,
                0,1,0, cubes[2].GetCenter().y,
                0,0,1, cubes[2].GetCenter().z,
                0,0,0,1
                );
            Matrix4x4 MHalfSphere1Translation = new Matrix4x4(
                
                1,0,0, halfSpheres[0].GetCenter().x,
                0,1,0, halfSpheres[0].GetCenter().y,
                0,0,1, halfSpheres[0].GetCenter().z,
                0,0,0,1
                );
            Matrix4x4 MmodelHalfSphere = MHalfSphere1Translation * MFloorRotation * MFloorScalling;
            halfSpheres[0].MultiplyMatrixes(MmodelHalfSphere, Mview, Mproj, DrawArea.Width / 2, DrawArea.Height / 2);

            Matrix4x4 MCube3Rotation = InitializeRotationMatrix(angleDynamic,0,1,0);
            Matrix4x4[] MmodelCube = new Matrix4x4[cubes.Count];
            MmodelCube[0] = MCube1Translation * MFloorRotation * MFloorScalling;
            MmodelCube[1] = MCube2Translation * MFloorRotation * MFloorScalling;
            MmodelCube[2] = MCube3Translation * MCube3Rotation * MFloorScalling;

            Task[] tasks1 = new Task[cubes.Count];
            Task[] tasks2 = new Task[squares.Count];
            int i;
            for (i = 0; i < cubes.Count; i++)
            {
                Cube temp1 = cubes[i];
                Matrix4x4 matrix = MmodelCube[i];
                tasks1[i] = Task.Factory.StartNew(() => temp1.MultiplyMatrixes(matrix, Mview, Mproj, DrawArea.Width / 2, DrawArea.Height / 2));
            }

            Matrix4x4[] MFloorTranslation = new Matrix4x4[squares.Count];
            Matrix4x4[] MmodelFloor = new Matrix4x4[squares.Count];
            for (i = 0; i < squares.Count; i++)
            {
                MFloorTranslation[i] =new  Matrix4x4(
                
                1,0,0, squares[i].GetCenter().x,
                0,1,0, squares[i].GetCenter().y,
                0,0,1, squares[i].GetCenter().z,
                0,0,0,1
                );
                MmodelFloor[i] = MFloorTranslation[i]* MFloorRotation * MFloorScalling;
            }
            int j;
            for (j = 0; j < squares.Count; j++)
            {
                Square square = squares[j];
                Matrix4x4 MmodelTemp = MmodelFloor[j];
                Square temp2 = square;
                tasks2[squares.IndexOf(temp2)] = Task.Factory.StartNew(() =>
                temp2.MultiplyMatrixes(MmodelTemp, Mview, Mproj, DrawArea.Width / 2, DrawArea.Height / 2));
            }

            Task.WaitAll(tasks1);
            Task.WaitAll(tasks2);

        }
        private Matrix4x4 InitializeRotationMatrix(double angle, int x, int y, int z)
        {
            double a = (angle * Math.PI) / 180.0;
            Vector3 temp = new Vector3(x,y,z);

            temp = Functions.Normalize(temp);
            x =(int) temp.X;
            y = (int)temp.Y;
            z =  (int)temp.Z;
            return new Matrix4x4(

                (float)( Math.Cos(a) + (1 - Math.Cos(a)) * x * x), (float)((1 - Math.Cos(a)) * x * y - Math.Sin(a) * z), (float)((1 - Math.Cos(a)) * x * z + Math.Sin(a) * y),0,
                (float)((1 - Math.Cos(a)) * x * y + Math.Sin(a) * z), (float)(Math.Cos(a) + (1 - Math.Cos(a)) * y * y), (float)((1 - Math.Cos(a)) * y * z - Math.Sin(a) * x),0,
                (float)((1 - Math.Cos(a)) * x * z - Math.Sin(a) * y), (float)((1 - Math.Cos(a)) * y * z + Math.Sin(a) * x), (float)(Math.Cos(a) + (1 - Math.Cos(a)) * z * z), 0,  
                0,0,0,1
            );
        }
        private Matrix4x4 InitializeViewMatrix(Vector3 camPos,
            Vector3 lookAt)
        {
            Vector3 direction = camPos - lookAt;
            direction = Functions.Normalize(direction);
            Vector3 right = Vector3.Cross(
            new Vector3(0, 1, 0), direction);

            Vector3 up = Vector3.Cross(direction, right);

            Matrix4x4 temp1 = new Matrix4x4(
            
                right.X, right.Y, right.Z,0,
                up.X, up.Y, up.Z,0,
                direction.X, direction.Y, direction.Z,0,
                0,0,0,1
            );
            Matrix4x4 temp2 = new Matrix4x4(
            
                1,0,0, -camPos.X,
                0,1,0, -camPos.Y,
                0,0,1, -camPos.Z,
                0,0,0, 1
            );
            return temp1*temp2;
        }
        private void FillTriangles()
        {
            Vector3 temp = staticCam;
            switch(camera)
                {
                case Camera.Move:
                    {
                        temp = moveCam;
                        break;
                    }
                default:
                    break;
            }
            foreach (var item in halfSpheres)
                item.Draw(zBufor, DrawArea, temp, shading, pointLights, spotLights, fogValue);
            Task[] tasks1 = new Task[cubes.Count];
            Task[] tasks2 = new Task[squares.Count];
            int i;
            for (i = 0; i < cubes.Count; i++)
            {
                Cube cube = cubes[i];
                Cube temp1 = cube;
                tasks1[cubes.IndexOf(temp1)] = Task.Factory.StartNew(() 
                    => temp1.Draw(zBufor, DrawArea, temp, shading, pointLights, spotLights, fogValue));
            }
            Task.WaitAll(tasks1);
            int j;
            for (j = 0; j < squares.Count; j++)
            {
                Square square = squares[j];
                Square temp2 = square;
                tasks2[squares.IndexOf(temp2)] = Task.Factory.StartNew(()
                    => temp2.Draw(zBufor, DrawArea, temp, shading, pointLights, spotLights, fogValue));
            }

            Task.WaitAll(tasks2);
        }
        private void FOVhScrollBar_ValueChanged(object sender, EventArgs e)
        {
            FOV = FOVhScrollBar.Value;
            DrawCube();
        }
        private void fogHScrollBar_ValueChanged(object sender, EventArgs e)
        {
            fogValue = ((double)fogHScrollBar.Value) / 1000;
        }
        private void cameraComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (((string)cameraComboBox.SelectedItem).Contains("Static"))
            {
                camera = Camera.Static;
            }
            else if (((string)cameraComboBox.SelectedItem).Contains("Watch"))
            {
                camera = Camera.Watch;
            }
            else if (((string)cameraComboBox.SelectedItem).Contains("Move"))
            {
                camera = Camera.Move;
            }
        }
        private void shadingComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (((string)shadingComboBox.SelectedItem).Contains("Constant"))
            {
                shading = Shading.Constant;
            }
            else if (((string)shadingComboBox.SelectedItem).Contains("Phong"))
            {
                shading = Shading.Phong;
            }
            else if (((string)shadingComboBox.SelectedItem).Contains("Gourad"))
            {
                shading = Shading.Gouraud;
            }
        }
        private void pointLight1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isPoint1 = pointLight1CheckBox.Checked;
        }
        private void pointLight2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isPoint2 = pointLight2CheckBox.Checked;
        }
        private void spotLightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isSpot = spotLightCheckBox.Checked;
        }
        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            DrawCube();
        }
        private void timerEventProcessor(object sender, EventArgs e)
        {
            alpha += 10;
            angleDynamic += 15.0;
            if (centerToPeriphery)
            {
                double len = Math.Sqrt(Math.Pow(xDynamic - xCenter, 2) + Math.Pow(zDynamic - zCenter, 2));
                if (len <= 30)
                {
                    theta += dTheta;
                    xDynamic = xCenter + (int)(a * Math.Exp(b * theta) * Math.Cos(theta));
                    zDynamic = zCenter + (int)(a * Math.Exp(b * theta) * Math.Sin(theta));
                }
                else
                {
                    centerToPeriphery = false;
                }
            }
            else
            {
                double len = Math.Sqrt(Math.Pow(xDynamic - xCenter, 2) + Math.Pow(zDynamic - zCenter, 2));
                if (len >= 2)
                {
                    theta -= dTheta;
                    xDynamic = xCenter + (int)(a * Math.Exp(b * theta) * Math.Cos(theta));
                    zDynamic = zCenter + (int)(a * Math.Exp(b * theta) * Math.Sin(theta));
                }
                else
                    centerToPeriphery = true;
            }
            DrawCube();
        }
    }
}
