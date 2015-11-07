using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {
	public GameObject EnemyBulletGO; //Enemy bullet prefab
	// Use this for initialization
	void Start () {
		Invoke ("FireEnemyBullet", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FireEnemyBullet() {
		//get a reference to the player's ship
		GameObject playerShip = GameObject.Find("PlayerGO");

		if (playerShip != null) {
			GameObject bullet = (GameObject) Instantiate(EnemyBulletGO);

			//set the bullet's initial position
			bullet.transform.position = transform.position;

			//compute bullet's direction towards playership
			Vector2 direction = playerShip.transform.position - bullet.transform.position;

			bullet.GetComponent<EnemyBullet>().SetDirection(direction);
		}
	}
}
