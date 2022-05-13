using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa_Lab4.Lights
{
    static class LightFunctions
    {
        public static Vector3 CalcPointLight(PointLight light, Vector3 viewDir,
            Vector3 norm, Vector3 pos)
        {
            Vector3 lightDir = light.position - pos;
            lightDir = Functions.Normalize(lightDir);
            float diff = (float) Math.Max(Functions.DotProduct(norm , lightDir), 0.0);
            Vector3 reflectDir = Reflect(-lightDir, norm);
            Vector3 temp = light.position - pos;
            float distance = (float)Math.Sqrt(Math.Pow(temp.X,2)+ Math.Pow(temp.Y, 2)+
                Math.Pow(temp.Z, 2));
            float attenuation = (float)( 1.0 / (light.constant + light.linear * distance 
                + light.quadratic * (distance * distance)));
            Vector3 ambient = light.ambient;
            Vector3 diffuse = diff *light.diffuse ;
            float spec = (float) Math.Pow(Math.Max(Functions.DotProduct(viewDir , reflectDir), 0.0), 32);
            Vector3 specular = spec * light.specular ;
            ambient *= attenuation;
            diffuse *= attenuation;
            specular *= attenuation;
            return (ambient + diffuse + specular);
        }
        public static Vector3 CalcSpotLight(SpotLight light, Vector3 viewDir, Vector3 norm, Vector3 pos)
        {
            Vector3 lightDir = light.position - pos;
            lightDir = Functions.Normalize(lightDir);
            float diff = (float)Math.Max(Functions.DotProduct(norm, lightDir), 0.0);
            Vector3 reflectDir = Reflect(-lightDir, norm);
            Vector3 temp = light.position - pos;
            float distance = (float)Math.Sqrt(Math.Pow(temp.X, 2) + Math.Pow(temp.Y, 2) +
                Math.Pow(temp.Z, 2));
            float attenuation = (float)(1.0 / (light.constant + light.linear * distance
                + light.quadratic * (distance * distance)));
            float theta = Functions.DotProduct(lightDir, Functions.Normalize(-light.direction));
            float epsilon = light.cutOff - light.outerCutOff;
            float intensity =(float) Math.Clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);
            Vector3 ambient = light.ambient;
            Vector3 diffuse = diff * light.diffuse;
            float spec = (float)Math.Pow(Math.Max(Functions.DotProduct(viewDir, reflectDir), 0.0), 32);
            Vector3 specular = spec * light.specular;
            ambient *= attenuation * intensity;
            diffuse *= attenuation * intensity;
            specular *= attenuation * intensity;
            return (ambient + diffuse + specular);
        }
        public static Vector3 ClampColor(Vector3 vect)
        {
            return new Vector3(Math.Clamp(vect.X, 0, 1),
                Math.Clamp(vect.Y, 0, 1),
                Math.Clamp(vect.Z, 0, 1)
                );
        }
        private static Vector3 Reflect(Vector3 I, Vector3 N)
        {
            float temp = Functions.DotProduct(N , I);
            return Functions.Normalize(I - (float)(2.0 * temp) * N);
        }
           
    }
}
