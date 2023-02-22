using Assets.Scripts.PickUpItem;
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
    private void Start()
    {
    }
    void Update()
    {

        if (m_FoodToThrow == null)
        {
            SetGameFoodObj(m_GameManager.SpawnGameFoodObj());
            StartCoroutine(ShotAtThePlayer());
        }
       
    }

    private Vector3 calaV3()
    {
        Vector3 v = m_Pleyer.gameObject.transform.position - this.gameObject.transform.position;
        Debug.DrawRay(this.gameObject.transform.position, v, Color.red, 5);

        return v;
    }

    public IEnumerator ShotAtThePlayer()
    {
        yield return new WaitForSeconds(2);
        ThrowObj();
    }

    internal override void SetGameFoodObj(GameObject i_GameObject)
    {
        m_FoodToThrow = i_GameObject;
        m_FoodToThrow.transform.position = transform.position - transform.forward;

        GameFoodObj obj = i_GameObject.GetComponent<GameFoodObj>();

        if (obj != null)
        {
            //m_GameFoodObj = i_GameObject;
            obj.SetPickUpItem(this);
            //StatePlayerHand++;
        }
        //m_FoodToThrow.transform.parent = this.transform;
    }

    internal override void ThrowObj()
    {
        float lineToPleyr = Vector3.Distance(transform.position, m_Pleyer.transform.position);

        m_FoodToThrow.GetComponent<GameFoodObj>().ThrowFood(1800, calaV3());
        m_FoodToThrow = null;
    }
}
