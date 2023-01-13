
using DiningCombat;
using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.FoodObj
{
    internal class FoodObj
    {
        private static readonly Random sr_Rand = new Random();
        public static FoodTypeData Builder()
        {
            var v = Enum.GetValues(typeof(eDamageType));
            return Builder((eDamageType) v.GetValue(sr_Rand.Next(v.Length)));
        }

        public static FoodTypeData Builder(eDamageType i_Type)
        {
            int index = 0;

            switch (i_Type)
            {
                case eDamageType.Hit:
                    index = sr_Rand.Next(Hit.sr_FoodTypeData.Length);
                    return Hit.sr_FoodTypeData[index];
                case eDamageType.Dispersing:
                    index = sr_Rand.Next(Dispersing.sr_FoodTypeData.Length);
                    return Dispersing.sr_FoodTypeData[index];
                case eDamageType.SmokeSrenade:
                    index = sr_Rand.Next(SmokeSrenade.sr_FoodTypeData.Length);
                    return SmokeSrenade.sr_FoodTypeData[index];
                    default:
                    return Hit.sr_FoodTypeData[Hit.k_Apple];
            }
        }
        public static class Hit
        {
            public const byte k_Tomato = GameGlobal.FoodObjsNames.k_TomatoVar,
                k_Apple = GameGlobal.FoodObjsNames.k_AppleVar;

            public static readonly FoodTypeData[] sr_FoodTypeData = {
                new FoodTypeData(eDamageType.Hit,
                                   5f,
                                   1f,
                                   5f,
                                   5f,
                                   1f,
                                   GameGlobal.TagNames.k_FoodObj,
                                   GameGlobal.FoodObjsNames.k_Apple),
                                new FoodTypeData(eDamageType.Hit,
                                   5f,
                                   1f,
                                   5f,
                                   5f,
                                   1f,
                                   GameGlobal.TagNames.k_FoodObj,
                                   GameGlobal.FoodObjsNames.k_Tomato)
            };
        }

        public class Dispersing
        {
            public const byte k_Cabbage = GameGlobal.FoodObjsNames.k_CabbageVar;

            public static readonly FoodTypeData[] sr_FoodTypeData = {
                new FoodTypeData(eDamageType.Dispersing,
                                   0.5f,
                                   50f,
                                   0.5f,
                                   5f,
                                   0f,
                                   GameGlobal.TagNames.k_FoodObj,
                                   GameGlobal.FoodObjsNames.k_Cabbage)
            };
        }
        public class SmokeSrenade
        {
            public const byte k_Flour = GameGlobal.FoodObjsNames.k_FlourVar;
            public static readonly FoodTypeData[] sr_FoodTypeData = {
                new FoodTypeData(eDamageType.SmokeSrenade,
                                   0f,
                                   50f,
                                   5,
                                   0.9f,
                                   0f,
                                   GameGlobal.TagNames.k_FoodObj,
                                   GameGlobal.FoodObjsNames.k_Flour)
            };
        }
    }
}
