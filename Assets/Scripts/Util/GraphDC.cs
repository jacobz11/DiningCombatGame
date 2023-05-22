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
            Debug.Log("i_StrtingPoint" + i_StrtingPoint);
            m_Higt = i_Higt;
            m_Width = i_Width;
            int higt = 2 * i_Higt - 2;
            int width = 2 * i_Width - 2;
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
                //m_Ring[x] = Cerate1DArr(x);
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

        //public void SpawnWater()
        //{
        //    #region var
        //    NodeGraph[][] grid = new NodeGraph[5][5];
        //    List<Vector2Int> Not_full_list = new List<Vector2Int>();

        //    #endregion
        //    Vector2Int positionInTheGraph = GetAndRandomEndPint();
        //    grid[positionInTheGraph] = Get_Water_type_by_how_many_neighbors(positionInTheGraph);

        //    foreach (Vector2Int new_neighbors_pos_in_Graph in GetNeighborsList(positionInTheGraph))
        //    {
        //        if (!grid[neighbors_pos_in_Graph].IsFree())
        //        {
        //            Not_full_list.Add(new_neighbors_pos_in_Graph);
        //        }
        //    }

        //    foreach(Vector2Int not_Full in Not_full_list)
        //    {
        //        grid[not_Full] = Get_Water_type_by_how_many_neighbors(positionInTheGraph);

        //        if (grid[not_Full].IsFuul)
        //        {
        //            Not_full_list.Remove(not_Full);
        //        }
        //    }
        //    #region func
        //    Vector2Int GetAndRandomEndPint()
        //    {
        //        return new Vector2Int(0,0)l
        //    }

        //    NodeGraph Get_Water_type_by_how_many_neighbors(Vector2Int positionInTheGraph)
        //    {
        //        return null;
        //    }

        //    #endregion

        //}
    }
}

/*public class GraphDC
{
    private Action<Vector3> m_Action;
    private Vector3 m_StrtingPoint;
    private List<Vector2Int> m_EndPints;
    private NodeGraph[,] m_Ring;
    private int m_Higt;
    private int m_Width;
    public Vector2Int m_TopLfet;
    public Vector2Int m_BottomRight;
    public Action OnEnding;

    public GraphDC(int i_Higt, int i_Width, Vector3 i_StrtingPoint, Action<Vector3> action)
    {
        Debug.Log("i_StrtingPoint" + i_StrtingPoint);
        m_Higt = i_Higt;
        m_Width = i_Width;
        int higt = 2 * i_Higt - 2;
        int width = 2 * i_Width - 2;
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
        int higt = 2 * i_Higt - 2;
        int width = 2 * i_Width - 2;
        m_Ring = new NodeGraph[higt, width];

        for (int x = 0; x < higt; x++)
        {
            //m_Ring[x] = Cerate1DArr(x);
            for (int z = 0; z < width; z++)
            {
                m_Ring[x, z] = new NodeGraph(MakeAction(x, z));
            }
        }
    }

    public Action MakeAction(int i_PosX, int i_PosZ)
    {
        return () =>
        {
            Vector3 posAction = new Vector3(i_PosX, 0, i_PosX) + m_StrtingPoint;
            Debug.Log(posAction);
            m_Action.Invoke(posAction);
            //Debug.Log("x :" + i_PosX + " z :" + i_PosZ);
            Vector2Int[] neighborsList = GetNeighborsList(i_PosX, i_PosZ, false);
            //Array.ForEach(neighborsList, (Vector2Int v) => Debug.Log(v));
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

        Debug.Log("m_EndPints.Count " + m_EndPints.Count);
    }

    private bool IsInReng(Vector2Int i_Pos)
    {
        return IsInReng(i_Pos.x, i_Pos.y);
    }

    private bool IsInReng(float i_PosX, float i_PosZ)
    {
        bool isLowreRengm = i_PosX > -1 && i_PosZ > -1;
        bool isUpperRengm = i_PosX < m_Ring.Rank && i_PosZ < m_Ring.Length;

        return isLowreRengm && isUpperRengm;
    }

    public bool IsFree(Vector2 i_Pos)
    {
        return IsFree(i_Pos.x, i_Pos.y);
    }

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
            //Debug.Log("m_EndPints.Count " + m_EndPints.Count);
            //int randIndex = Random.Range(0, m_EndPints.Count);
            //Debug.Log("randIndex :" + randIndex);
            //Vector2Int randEndPoint = m_EndPints[randIndex];
            //m_EndPints.RemoveAt(randIndex);
            Vector2Int randEndPoint = m_EndPints[0];
            m_EndPints.RemoveAt(0);
            return randEndPoint;
        }
    }

    public NodeGraph this[Vector3 i_Pos]
    {
        get => this[i_Pos.x, i_Pos.z];
    }
    private NodeGraph this[Vector2 i_Pos]
    {
        get => m_Ring[(int)i_Pos.x, (int)i_Pos.y];
    }
    public NodeGraph this[float x, float z]
    {
        get
        {
            Debug.Log("x  :" + x + "z  :" + z);
            return m_Ring[(int)x, (int)z];
        }
    }*/