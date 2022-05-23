using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    AtmRush atmRush;
    [SerializeField] GameObject moneyPrefab;
    private void Start()
    {
        atmRush = FindObjectOfType<AtmRush>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            if(!AtmRush.instance.cubes.Contains(other.gameObject))
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
        if (other.gameObject.CompareTag("Obstacle") && this.gameObject.CompareTag("NewCube"))
        {
            Destroy(this.gameObject);
            AtmRush.instance.cubes.Remove(this.gameObject);
        }
    }

    void ChangeObject()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine(AtmRush.instance.MakeObjectsBigger(1.2f));
    }
}
