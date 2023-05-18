using DiningCombat.Environment;
using DiningCombat.FoodObject;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Util.ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(fileName = "PoolPackagesPrefabListSO", menuName = "Custom Objects/Pool/PoolPackagesPrefabListSO")]
    public class PoolPackagesPrefabListSO : PoolPrefabListSO<IPackage>
    {
    }
}
