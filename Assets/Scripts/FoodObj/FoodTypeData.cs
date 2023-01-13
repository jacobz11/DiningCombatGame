using System;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.FoodObj
{
    public struct FoodTypeData
    {
        public readonly eDamageType r_DamageType;
        public readonly float r_Damage;
        public readonly float r_Range;
        public readonly float r_Mass;
        public readonly float r_Drag;
        public readonly float r_Material;
        public readonly string r_Tag;
        public readonly string r_Name;

        public FoodTypeData(eDamageType i_DamageType,
            float i_Damage,
            float i_Range,
            float i_Mass,
            float i_Material,
            float i_Drag,
            string i_Tag,
            string i_Name
            )
        {
            r_DamageType = i_DamageType;
            r_Damage = i_Damage;
            r_Range = i_Range;
            r_Mass = i_Mass;
            r_Drag = i_Drag;
            r_Material = i_Material;
            r_Tag = i_Tag;
            r_Name = i_Name;
        }
    }
}
