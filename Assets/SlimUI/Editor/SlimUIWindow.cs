using UnityEditor;
using UnityEngine;

namespace SlimUI
{
    public class SlimUIWindow : EditorWindow
    {

        [MenuItem("Window/SlimUI/Online Documentation")]
        public static void ShowWindow()
        {
            Application.OpenURL("https://www.slimui.com/documentation");
        }
    }
}
