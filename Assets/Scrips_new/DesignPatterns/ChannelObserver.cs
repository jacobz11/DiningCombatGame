using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrips_new.DesignPatterns
{
    public class ChannelObserver<T> : ScriptableObject
    {
        public event Action<List<T>> ViewingElements;
        public event Action<List<T>> Viewer;

        public void Invoke()
        {
            List<T> list = new List<T>();
            ViewingElements?.Invoke(list);
            Viewer?.Invoke(list);
        }
    }
}
