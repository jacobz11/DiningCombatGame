using Assets.Scripts.Player.PickUpItem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AiThrew :IThrowingGameObj
{
    [SerializeField]
    public float m_TimeToThrew;
    [SerializeField]
    private GameManager m_GameManager;
    [SerializeField]
    private GameObject m_Pleyer;
    private GameObject m_FoodToThrow;

    void Update()
    {
        if (m_FoodToThrow == null)
        {
            SetGameFoodObj(null);
            StartCoroutine(shotAtThePlayer());
        }
    }

    private Vector3 calaV3()
    {
        Vector3 v = m_Pleyer.gameObject.transform.position - this.gameObject.transform.position;
        Debug.DrawRay(this.gameObject.transform.position, v, Color.red, 5);

        return v;
    }

    private IEnumerator shotAtThePlayer()
    {
        yield return new WaitForSeconds(m_TimeToThrew);
        ThrowObj();
    }

    internal override void SetGameFoodObj(GameObject i_GameObject)
    {
        m_FoodToThrow = m_GameManager.SpawnGameFoodObj();
        m_FoodToThrow.transform.position = transform.position - transform.forward;

        m_FoodToThrow.transform.parent = this.transform;
    }

    internal override void ThrowObj()
    {
        float lineToPleyr = Vector3.Distance(transform.position, m_Pleyer.transform.position);

        m_FoodToThrow.GetComponent<GameFoodObj>().ThrowFood(1800, calaV3());
        m_FoodToThrow = null;
    }
}
