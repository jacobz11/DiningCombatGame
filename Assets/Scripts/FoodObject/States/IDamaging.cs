using UnityEngine;
// TODO : Move it to the interfaces folder
namespace DiningCombat.Util
{
    public interface IDamaging
    {
        /// <summary>
        /// <see cref="Assets.Util.Vector2AsRang"/>
        /// x => max 
        /// y => min 
        /// </summary>
        Vector2 RangeDamage { get; }

        Vector3 ActionDirection { get; }

        bool IsActionHappen { get; }

        /// <summary>
        /// Activation by triger Collision
        /// </summary>
        /// <param name="collision"></param>
        void Activation(Collision collision);

        /// <summary>
        /// Activation by triger
        /// </summary>
        /// <param name="i_Collider"></param>
        void Activation(Collider i_Collider);

        void Activate();
        float CalculatorDamag();
    }
}