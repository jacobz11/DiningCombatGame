using Assets.Scripts.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiningCombat.Player
{
    [Serializable]
    [CreateAssetMenu(fileName = "AllPlayerSkinsSO", menuName = "Custom Objects/Player/AllPlayerSkinsSO")]
    public class AllPlayerSkinsSO : ScriptableObject
    {
        [SerializeField]
        private List<Material> m_SkinsMaterials;
        public List<Material> Skins
        {
            get => m_SkinsMaterials;
        }
        public Material this[int index]
        {
            get
            {
                return m_SkinsMaterials[index % m_SkinsMaterials.Count];
            }
            set { /* set the specified index to value here */ }
        }

        public static implicit operator Material(AllPlayerSkinsSO i_AllPlayer)
        {
            return RandomFromArray.GetRandomElement<Material>(i_AllPlayer.Skins);
        }
    }
}