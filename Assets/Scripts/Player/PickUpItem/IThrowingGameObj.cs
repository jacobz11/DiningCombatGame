using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.PickUpItem
{
    public abstract class IThrowingGameObj : MonoBehaviour
    {
        internal abstract void SetGameFoodObj(GameObject i_GameObject);
        internal abstract void ThrowObj();

    }
}
