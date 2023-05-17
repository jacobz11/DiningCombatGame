using System.Collections.Generic;
using UnityEngine;

namespace DiningCombat.FoodObj.Managers
{
    public interface IElementsViewer
    {
        /// <returns>List of invoking eche vieing element</returns>
        List<Vector3> GetElements();

        /// <summary>
        /// <param name="o_NearestElement">Nearest Element</param>
        /// <returns>false => ther are no elmnrts or colde not fund the nearest element</returns>
        bool FindTheNearestOne(Vector3 i_From, out Vector3 o_NearestElement);
    }
}