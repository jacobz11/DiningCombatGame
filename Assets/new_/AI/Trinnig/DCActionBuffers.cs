using System;
using Unity.MLAgents.Actuators;
using UnityEngine;

namespace Assets.Scripts.Player.Trinnig
{
    public struct DCActionBuffers
    {
        public static readonly DCActionBuffers Empty =
            new DCActionBuffers(EmptyActionBuffers());
        public const string k_Formt = @"Horizontal: {0}
Vertical    : {1}
IsJumping   : {2}
Rotate      : {3}
IsBoost     : {4}
IsPress     : {5}";
        public float Horizontal => m_Buffers.ContinuousActions.Array[0];
        public float Vertical => m_Buffers.ContinuousActions.Array[1];
        public float Rotate => m_Buffers.ContinuousActions.Array[2];
        public bool IsJumping => m_Buffers.DiscreteActions.Array[0] == 1;
        public bool IsBoost => m_Buffers.DiscreteActions.Array[1] == 1;
        public bool IsPress => m_Buffers.DiscreteActions.Array[2] == 1;

        private ActionBuffers m_Buffers;
        public DCActionBuffers(ActionBuffers i_Buffers)
        {
            if (i_Buffers.IsEmpty())
            {
                Debug.Log("in DCActionBuffers empty");
                m_Buffers = EmptyActionBuffers(); 
                return;
            }
            m_Buffers = i_Buffers;
        }

        private static ActionBuffers EmptyActionBuffers()
        {
            return new ActionBuffers(new float[] { 0f, 0f, 0f },
                            new int[] { 0, 0, 0 });
        }

        public override string ToString()
        {
            //            string res = string.Empty;//k_Formt;
            //            string formt = @"i : {0} val : {1} Type: {2}
            //";
            //            int i = 0;
            //            foreach (var buffer in m_Buffers.ContinuousActions)
            //            {
            //                res +=string.Format(formt, i, buffer.ToString() , buffer.GetType());
            //                i++;
            //            }
            //            i = 0;

            //            foreach (var buffer in m_Buffers.DiscreteActions)
            //            {
            //                res += string.Format(formt, i, buffer.ToString(), buffer.GetType());
            //                i++;
            //            }
            //            return res;
            return string.Format("Horizontal {0}, Vertical {1} IsJumping {2} Rotate {3} IsBoost {4} IsPress {5}", Horizontal,
                Vertical, IsJumping, Rotate, IsBoost, IsPress);
        }

        internal void Used()
        {
            bool isUesd = true;
        }
    }
}