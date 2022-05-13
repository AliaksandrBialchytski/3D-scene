using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa_Lab4.Lights
{
    public struct PointLight
    {
        public  PointLight(Vector3 pos, double constant, double linear,
            double quadratic, Vector3 ambient, Vector3 diffuse, 
            Vector3 specular)
        {
            this.position = pos; this.constant = constant;
            this.linear = linear; this.quadratic = quadratic;
            this.ambient = ambient; this.diffuse = diffuse; this.specular = specular;
        }
        public Vector3 position;

        public double constant;
        public double linear;
        public double quadratic;

        public Vector3 ambient;
        public Vector3 diffuse;
        public Vector3 specular;
    }
    public struct SpotLight
    {
        public SpotLight(Vector3 pos, Vector3 dir, float cutOff, float outerCutOff,
            float constant, float linear,float quadratic, 
            Vector3 ambient, Vector3 diffuse,Vector3 specular)
        {
            this.position = pos; this.direction = dir; this.constant = constant;
            this.cutOff = cutOff; this.outerCutOff = outerCutOff;
            this.linear = linear; this.quadratic = quadratic;
            this.ambient = ambient; this.diffuse = diffuse; this.specular = specular;
        }
        public Vector3 position;
        public Vector3 direction;

        public float cutOff;
        public float outerCutOff;


        public float constant;
        public float linear;
        public float quadratic;

        public Vector3 ambient;
        public Vector3 diffuse;
        public Vector3 specular;
    }
}
