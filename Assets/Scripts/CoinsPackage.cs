using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class CoinsPackage : IPackage
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerCoins> (out PlayerCoins o_PlayerCoins))
        {
            o_PlayerCoins.AddCoins(this);
            ReturnToPool();
        }
    }
}
