using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Util.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ClassOS", menuName = "Custom Objects/ClassOS")]
    public class ClassOS :ScriptableObject
    {
        [SerializeField]
        public string m_TypeName;
        public string m_LoPoNameS;

    }
}
