using System;
using UnityEngine;
using System.Collections.Generic;

namespace Util
{
    public class ChannelObserver<T> : ScriptableObject
    {
        public event Action<List<T>> ViewingElements;
        public event Action<List<T>> Viewer;

        public void Invoke()
        {
            List<T> list = GetAllDataFromViewingElements();
            Viewer?.Invoke(list);
        }

        private List<T> GetAllDataFromViewingElements()
        {
            List<T> list = new List<T>();
            ViewingElements?.Invoke(list);
            return list;
        }
    }
}
