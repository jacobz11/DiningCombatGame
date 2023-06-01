using System;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace DiningCombat.Util
{
    public class GraphDC
    {
        private Action<Vector3> m_Action;
        public Action OnEnding;

        public Vector2Int m_TopLfet;
        public Vector2Int m_BottomRight;

        private int m_Higt;
        private int m_Width;
        private Vector3 m_StrtingPoint;
        private List<Vector2Int> m_EndPints;
        private NodeGraph[][] m_Ring;
        public NodeGraph this[Vector3 i_Pos]
        {
            get => this[i_Pos.x, i_Pos.z];
        }
        private NodeGraph this[Vector2 i_Pos]
        {
            get => m_Ring[(int)i_Pos.x][(int)i_Pos.y];
        }
        public NodeGraph this[float x, float z]
        {
            get => m_Ring[(int)x][(int)z];
        }
        public GraphDC(int i_Higt, int i_Width, Vector3 i_StrtingPoint, Action<Vector3> action)
        {
            m_Higt = i_Higt;
            m_Width = i_Width;
            int higt = (2 * i_Higt) - 2;
            int width = (2 * i_Width) - 2;
            m_StrtingPoint = i_StrtingPoint;
            m_Action = action;
            Init(i_Higt, i_Width);

            m_EndPints = new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, width-1),
                new Vector2Int(higt -1, 0),
                new Vector2Int(higt -1, width-1),
            };
        }

        public void Init(int i_Higt, int i_Width)
        {
            Debug.Assert(i_Higt > 0, "Higt is last or equle to 0");
            Debug.Assert(i_Width > 0, "Width is last or equle to 0");

            int higt = 2 * i_Higt;
            int width = 2 * i_Width;

            m_Ring = new NodeGraph[higt][];

            for (int x = 0; x < higt; x++)
            {
                m_Ring[x] = new NodeGraph[width];
                for (int z = 0; z < width; z++)
                {
                    m_Ring[x][z] = new NodeGraph(MakeAction(x, z));
                }
            }
        }

        public Action MakeAction(int i_PosX, int i_PosZ)
        {
            return () =>
            {
                float x = i_PosX - m_Higt;
                float z = i_PosZ - m_Width;
                Vector3 posAction = new Vector3(x, 0, z) + m_StrtingPoint;
                m_Action.Invoke(posAction);
                Vector2Int[] neighborsList = GetNeighborsList(i_PosX, i_PosZ, false);
                AddNeighborsToList(neighborsList);
            };
        }

        public static Vector2Int[] GetNeighborsList(int i_PosX, int i_PosZ, bool i_IsDiagonals)
        {
            Vector2Int[] neighbors = new Vector2Int[]
               {
                    // Neighbor one above
                    new Vector2Int(i_PosX +1, i_PosZ),
                    // Neighbor one below
                    new Vector2Int(i_PosX -1, i_PosZ),
                    // Neighbor one to the right
                    new Vector2Int(i_PosX, i_PosZ +1),
                    // One neighbor to the left
                    new Vector2Int(i_PosX, i_PosZ -1),
               };

            return neighbors;
        }

        private void AddNeighborsToList(Vector2Int[] i_Neighbors)
        {
            m_EndPints.AddRange(Array.FindAll<Vector2Int>(i_Neighbors, (Vector2Int pos) => IsFree(pos)));
        }

        private bool IsInReng(float i_PosX, float i_PosZ)
        {
            bool isLowreRengm = i_PosX > -1 && i_PosZ > -1;
            bool isUpperRengm = i_PosX < m_Ring.Length && i_PosZ < m_Ring[0].Length;

            return isLowreRengm && isUpperRengm;
        }

        public bool IsFree(Vector2 i_Pos) => IsFree(i_Pos.x, i_Pos.y);

        public bool IsFree(float i_PosX, float i_PosZ)
        {
            bool res = false;
            if (IsInReng(i_PosX, i_PosZ))
            {
                res = this[(int)i_PosX, (int)i_PosZ].IsFree;
            }

            return res;
        }

        public void Activate()
        {
            Vector2Int vector = GetAndRemoveRandomEndPint();
            try
            {
                this[vector].SetNotFree();

            }
            catch (Exception e)
            {
                Debug.LogException(e);
                Debug.Log(vector);
            }

            Vector2Int GetAndRemoveRandomEndPint()
            {
                Vector2Int randEndPoint = m_EndPints[0];
                m_EndPints.RemoveAt(0);
                return randEndPoint;
            }
        }
    }
}