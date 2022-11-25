using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnArea : MonoBehaviour
{
    public GameObject[] myObjects;
    public int amount;

    void Start()
    {
        Spawn(amount);
        StartCoroutine(HelpMunition());
    }

    IEnumerator HelpMunition()
	{
		yield return new WaitForSeconds(20f);
        Spawn(amount);
        StartCoroutine(HelpMunition());
    }

    void Spawn(int x){
        for (int i = 0; i < x; i++)
        {
            int randomIndex = Random.Range(0, myObjects.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-4.5f, 4.5f), 0.3f, Random.Range(-12f, 3f));

            Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
        }
    }
}
