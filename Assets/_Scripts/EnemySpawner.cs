using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject EnemyGO; //enemy prefab

	float maxSpawnRateInSeconds = 5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnEnemy() {
		//bottom-left point ofthe screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

		//right-top point ofthe screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		GameObject anEnemy = (GameObject)Instantiate (EnemyGO);
		anEnemy.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);

		ScheduleNextEnemySpawn ();
	}

	void ScheduleNextEnemySpawn() {
		float spawnInNSeconds;

		if (maxSpawnRateInSeconds > 1f) {
			spawnInNSeconds = Random.Range (1f, maxSpawnRateInSeconds);
		} else {
			spawnInNSeconds = 1f;
		}

		Invoke ("SpawnEnemy", spawnInNSeconds);
	}

	void IncreaseSpawnRate() {		
		if(maxSpawnRateInSeconds > 1f) {
			maxSpawnRateInSeconds --;
		}
		if(maxSpawnRateInSeconds == 1f) {
			CancelInvoke("IncreaseSpawnRate");
		}
		
	}

    //Start enemy spawning
    public void ScheduleEnemySpawner()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        //Increase spawn rate every 30 seconds
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    } 

    //Stop enemy spawning
    public void UnscheduleEnemySpawning()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}







































