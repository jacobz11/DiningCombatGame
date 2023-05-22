using Unity.Netcode;
using UnityEngine;

namespace DiningCombat.Util.DesignPatterns
{
    public class Singleton<T> : NetworkBehaviour where T : NetworkBehaviour
    {
        private static T s_Instance;
        public static T Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType<T>();

                    if (s_Instance == null)
                    {
                        GameObject singleton = new GameObject(typeof(T).Name);
                        s_Instance = singleton.AddComponent<T>();
                        DontDestroyOnLoad(singleton);
                    }
                }

                return s_Instance;
            }
        }

        protected virtual void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                s_Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
