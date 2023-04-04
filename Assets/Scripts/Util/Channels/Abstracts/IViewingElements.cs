using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Util.Channels.Abstracts
{
    internal interface IViewingElements<T>
    {
        void ViewElement(List<T> elements);
    }

    internal interface IViewingElementsPosition : IViewingElements<Vector3>
    {
        void ViewElement(List<Vector3> elements);
    }
}
