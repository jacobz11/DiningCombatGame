using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Util.Channels.Abstracts
{
    internal interface IViewingElements<T>
    {
        void ViewElement(List<T> elements);
    }

    interface IUesableElements
    {
        bool IsUesed { get; }

        bool Unsed();

        void OnEndUsing();
    }

    internal interface IViewingElementsPosition : IViewingElements<Vector3>, IUesableElements
    {
    }
}