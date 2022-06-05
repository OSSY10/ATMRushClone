using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public MoneyState currentMoneyState;
    public GameObject dollar;

    public GameObject diamond;

    
    public enum MoneyState
    {
        Dollar,
        
        Diamond
    }
    public void ChangeState(MoneyState newState)
    {
        if (newState == MoneyState.Diamond)
        {
            
            dollar.SetActive(false);
            diamond.SetActive(true);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            if (!AtmRush.instance.cubes.Contains(other.gameObject))
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.tag = "NewCube";
                other.gameObject.AddComponent<Collision>();
             
                other.gameObject.AddComponent<Rigidbody>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                AtmRush.instance.StackCube(other.gameObject, AtmRush.instance.cubes.Count - 1);
            }
        }
        if (other.gameObject.CompareTag("Gate") && this.gameObject.CompareTag("NewCube"))
        {
            ChangeObject();
        }

        void ChangeObject()
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(AtmRush.instance.MakeObjectsBigger(1.15f));
        }
    }
}
