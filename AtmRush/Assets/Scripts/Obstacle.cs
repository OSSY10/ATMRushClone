using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
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
            other.transform.DOMove(other.transform.position - new Vector3(0, 0, 10), 1).SetEase(Ease.OutBounce);
        }
    }

    IEnumerator Crash()
    {
        Movement.instance.moveSpeed = 0;
        yield return new WaitForSeconds(1.5f);
        Movement.instance.moveSpeed = 6;
    }

    public void RemoveList(GameObject crashObj)
    {
        AtmRush.instance.cubes.Remove(crashObj);
        crashObj.tag = "Cube";
        crashObj.GetComponent<BoxCollider>().isTrigger = true;
        crashObj.GetComponent<Collision>().enabled = false;

        GameObject bounceMoney = Instantiate(crashObj, RandomPos(transform), Quaternion.identity);
        Destroy(bounceMoney.GetComponent<Rigidbody>());
        bounceMoney.transform.DOMove(bounceMoney.transform.position - new Vector3(0, 2, 0), 1).SetEase(Ease.OutBounce);
        Destroy(crashObj);
    }

    public Vector3 RandomPos(Transform obstacle)
    {
        float x = Random.Range(0, 3.95f);
        float z = Random.Range(10, 15);
        Vector3 posisiton = new Vector3(x, 3, obstacle.position.z + z);
        return posisiton;
    }
}