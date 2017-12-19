using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Orientation
    {
        public Vector3 H { get; private set; }
        public Vector3 L { get; private set; }
        public Vector3 U { get; private set; }

        public Orientation()
        {
            H = new Vector3(0, 1, 0);
            L = new Vector3(-1, 0, 0);
            U = new Vector3(0, 0, -1);
        }
        public void RotateU(float delta)
        {
            double radians = (Math.PI / 180) * delta;
            H = Vector3.Scale(H, new Vector3((float)Math.Cos(radians), -(float)Math.Sin(radians), 0));
            L = Vector3.Scale(L, new Vector3((float)Math.Sin(radians), (float)Math.Cos(radians),0));
            
        }

        public void RotateL(float delta)
        {
            double radians = (Math.PI / 180) * delta;
            H = Vector3.Scale(H, new Vector3((float)Math.Cos(radians), 0, (float)Math.Sin(radians)));
            U = Vector3.Scale(U, new Vector3(-(float)Math.Sin(radians), 0, (float)Math.Cos(radians)));  
        }

        public void RotateH(float delta)
        {
            double radians = (Math.PI / 180) * delta;
            L = Vector3.Scale(L, new Vector3(0, (float)Math.Cos(radians), (float)Math.Sin(radians)));
            U = Vector3.Scale(U, new Vector3(0, -(float)Math.Sin(radians), (float)Math.Cos(radians)));          
        }
        public void DollarRotation()
        {
            Vector3 V = new Vector3(0, 1, 0);
            U = Vector3.Scale(H, L);
            L = Vector3.Scale(V, H) / Vector3.Scale(V, H).magnitude;            
        }
    }
}
