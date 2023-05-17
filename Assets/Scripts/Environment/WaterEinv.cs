using DiningCombat.Player;
using System.Diagnostics;
using UnityEngine;

namespace DiningCombat.Environment
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class WaterEinv : MonoBehaviour
    {
        // TODO : Arrange that life will go down less quickly 
        private const float k_Damage = 0.01f;
        private void OnTriggerStay(Collider other) => PlayerLifePoint.TryToDamagePlayer(other.gameObject, k_Damage, out bool _);
        
        #region System And Debuging
        private string GetDebuggerDisplay()
        {
            return ToString();
        }

        public override string ToString()
        {
            return "WaterEinv";
        }
        #endregion
    }
}