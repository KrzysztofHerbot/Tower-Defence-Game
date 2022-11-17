using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildTimer = 0.5f;
    //[SerializeField] Bank bank;
    //TowerElement towerElement;
    private void Start()
    {
        
        StartCoroutine(Build());
    }
    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null) { return false; }
        if (bank.GetCurrentBalance() >= cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }

        return false;
    }

    IEnumerator Build()
    {
       // Debug.Log("BUILD");
        foreach(Transform child in transform)
        {
            //Debug.Log("FOREACH1");
            //towerElement = GetComponentInChildren<TowerElement>();
            //if (towerElement != null)
            //{
                //Debug.Log("FOREACH1-IF1");
                child.gameObject.SetActive(false);
            //}
            
            foreach(Transform grandchild in child)
            {
                //towerElement = GetComponentInChildren<TowerElement>();
                //if (towerElement != null)
                //{
                    //Debug.Log("FOREACH1-IF2");
                    grandchild.gameObject.SetActive(false);
                //}
            }
        }

        foreach (Transform child in transform)
        {
            //Debug.Log("FOREACH2");
            //towerElement = GetComponentInChildren<TowerElement>();
            //if (towerElement != null)
            //{
               // Debug.Log("FOREACH2-IF2");
                child.gameObject.SetActive(true);
                yield return new WaitForSeconds(buildTimer);
            //}

            foreach (Transform grandchild in child)
            {
                //towerElement = GetComponentInChildren<TowerElement>();
                //if (towerElement != null)
                //{
                   // Debug.Log("FOREACH2-IF2");
                    grandchild.gameObject.SetActive(true);
                    yield return new WaitForSeconds(buildTimer);
                //}
            }
        }
    }

}
