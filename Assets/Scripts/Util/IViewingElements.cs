using System.Collections.Generic;

namespace DiningCombat.Util
{
    public interface IViewingElements<T>
    {
        void ViewElement(List<T> elements);
    }
}