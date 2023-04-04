using DiningCombat.Player;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts.NPC
{
    internal class NonPlayerCharacter : BridgeImplementor3DMovement
    {
        [SerializeField]
        private Vector3 m_Target;

        private void Update()
        {
            
        }
    }
}
