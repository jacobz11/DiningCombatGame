using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.PickUpItem
{
    public abstract class ThrowingGameObj : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_Parent;
        internal abstract void SetGameFoodObj(GameObject i_GameObject);
        internal abstract void ThrowObj();

        public abstract float ForceMulti
        {
            get;
            set;
        }

        public abstract Transform GetPoint();

        internal bool DidIHurtMyself(Collision i_Collision)
        {
            return m_Parent.name == i_Collision.gameObject.name;
        }
    }
}
