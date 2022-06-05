using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    public List<GameObject> pooledObjects = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NewCube"))
        {
            if (AtmRush.instance.cubes.Count - 1 == AtmRush.instance.cubes.IndexOf(other.gameObject)) //listenin son elemaný ise
            {
                AtmRush.instance.cubes.Remove(other.gameObject);
                Destroy(other.gameObject);
            }

            else
            {
                int crashObjIndex = AtmRush.instance.cubes.IndexOf(other.gameObject);
                int lastIndex = AtmRush.instance.cubes.Count - 1;

                for (int i = crashObjIndex; i <= lastIndex; i++)
                {
                    RemoveList(AtmRush.instance.cubes[crashObjIndex]);
                }
            }
        }

        else if (other.CompareTag("MainCube"))
        {
            StartCoroutine(Crash());
            other.transform.DOMove(other.transform.position - new Vector3(0, 0, 7), 1).SetEase(Ease.OutBounce);
        }
    }

    IEnumerator Crash()
    {
        Movement.instance.moveSpeed = 0;
        yield return new WaitForSeconds(1.5f);
        Movement.instance.moveSpeed = 6;
    }
    GameObject GetPooledObject()
    {
        for(int i = 0; i< pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public void RemoveList(GameObject crashObj)
    {
        crashObj.SetActive(false);
        pooledObjects.Add(crashObj);
        AtmRush.instance.cubes.Remove(crashObj);       
        GameObject money = GetPooledObject();

        if(money != null)
        {
            money.tag = "Cube";
            money.GetComponent<BoxCollider>().isTrigger = true;
            money.GetComponent<Collision>().enabled = false;
            money.transform.position = RandomPos(transform);
            money.transform.rotation = Quaternion.identity;
            
            GameObject bounceMoney = Instantiate(money, RandomPos(transform), Quaternion.identity);
            Destroy(bounceMoney.GetComponent<Rigidbody>());
            bounceMoney.SetActive(true);
            bounceMoney.transform.DOMove(bounceMoney.transform.position - new Vector3(0, 2, 0), 1).SetEase(Ease.OutBounce);
        }
        
        
    }

    public Vector3 RandomPos(Transform obstacle)
    {
        float x = Random.Range(-3, 3.1f);
        float z = Random.Range(6, 12);
        Vector3 posisiton = new Vector3(x, 3, obstacle.position.z + z);
        return posisiton;
    }
}