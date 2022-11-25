using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

	public Wave[] waves;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;

	//public GameController gameController;

	private int waveIndex = 1;

	void Update ()
	{
		if (EnemiesAlive > 0)
		{
			return;
		}

		if (waveIndex == waves.Length)
		{
			//gameController.WinLevel();
			this.enabled = false;
		}

		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
			return;
		}

		countdown -= Time.deltaTime;

		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

		//waveCountdownText.text = string.Format("{0:00.00}", countdown);
	}

	IEnumerator SpawnWave ()
	{
		WallStats.instance.ChangeRound();

		Wall.health = 20;

		Wave wave = waves[waveIndex];

		EnemiesAlive = wave.count;

		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemy[RandomSpecificInt()]);
			yield return new WaitForSeconds(1f / wave.rate);
		}

		waveIndex++;
	}

	void SpawnEnemy (GameObject enemy)
	{
		Instantiate(enemy, transform.position, transform.rotation);
	}

    public int RandomSpecificInt(){    
        int val = Random.Range(0,4);
        if(val == 1)
            {
                return 2;
            }
        else if(val == 2)
            {
                return 0;
            }
        return 1;
    }

}
