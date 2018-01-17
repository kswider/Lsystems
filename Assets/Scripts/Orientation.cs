using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    public class Orientation
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
        public Orientation(Orientation o)
        {
            H = o.H;
            L = o.L;
            U = o.U;
        }
        public void RotateU(float delta)
        {
            H = Quaternion.AngleAxis(-delta, U) * H;
            L = Quaternion.AngleAxis(-delta, U) * L;
        }

        public void RotateL(float delta)
        {
            H = Quaternion.AngleAxis(-delta, L) * H;
            U = Quaternion.AngleAxis(-delta, L) * U;
        }

        public void RotateH(float delta)
        {
            L = Quaternion.AngleAxis(delta, H) * L;
            U = Quaternion.AngleAxis(delta, H) * U;
        }
        public void DollarRotation()
        {
            Vector3 V = new Vector3(0, 1, 0);
            L = Vector3.Scale(V, H) / Vector3.Scale(V, H).magnitude;
            U = Vector3.Scale(H, L);
    }
    
}
