﻿using UnityEngine;
namespace DiningCombat.Environment
{
    public sealed class Word
    {
        private static readonly Word sr_Word = new Word();
        private static readonly GameObject[] sr_HidigSpot;

        static Word()
        {
            sr_HidigSpot = GameObject.FindGameObjectsWithTag(GameGlobal.TagNames.k_Hide);
        }

        public static Word Instance { get { return sr_Word; } }

        public GameObject[] GetHidigSpot()
        {
            return sr_HidigSpot;
        }
    }
}
