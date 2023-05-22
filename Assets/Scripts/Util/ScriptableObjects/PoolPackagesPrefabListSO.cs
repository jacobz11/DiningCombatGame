using DiningCombat.Environment;
using System;
using UnityEngine;

namespace Assets.Scripts.Util.ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(fileName = "PoolPackagesPrefabListSO", menuName = "Custom Objects/Pool/PoolPackagesPrefabListSO")]
    public class PoolPackagesPrefabListSO : PoolPrefabListSO<IPackage>
    {
    }
}
