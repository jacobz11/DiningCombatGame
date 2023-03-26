using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Test
{
    internal class AllTets : ScriptableObject
    {

        //List<Terrain> terrainList;
        public List<Type> TestTypes = new List<Type>()
        {
            typeof(PlyerMomnemtTest),
        };
    }
}
