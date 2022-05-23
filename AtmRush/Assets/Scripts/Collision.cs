using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] GameObject moneyPrefab;
    int gateNumber;
    int targetCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            if(!AtmRush.instance.cubes.Contains(other.gameObject))
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.tag = "Untagged";
                other.gameObject.AddComponent<Collision>();
                other.gameObject.AddComponent<Rigidbody>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;  

                AtmRush.instance.StackCube(other.gameObject, AtmRush.instance.cubes.Count - 1);
            }
        }
        if (other.gameObject.CompareTag("Gate"))
        {
            
            gateNumber = other.gameObject.GetComponent<GateController>().GetGateNumber();
            targetCount = AtmRush.instance.cubes.Count + gateNumber;
            if(gateNumber > 0)
            {
                IncreaseMoneyCount();
            }
            else if(gateNumber < 0 && this.gameObject.CompareTag("MainCube"))
            {
                DecreaseMoneyCount();     
            }
        }
    }

    void IncreaseMoneyCount()
    {
        for(int i = 0; i < gateNumber; i++)
        {
            GameObject newMoney = Instantiate(moneyPrefab);
            AtmRush.instance.StackCube(newMoney.gameObject, AtmRush.instance.cubes.Count - 1);
        }
    }
    void DecreaseMoneyCount()
    {
        for(int i = AtmRush.instance.cubes.Count - 1; i >= targetCount; i--)
        {
            AtmRush.instance.cubes[i].SetActive(false);
            AtmRush.instance.cubes.RemoveAt(i);
        }
    }
}
