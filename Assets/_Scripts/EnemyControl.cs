using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
    public GameObject ExplosionGO;//explosion prefab

	float speed;
	// Use this for initialization
	void Start () {
		speed = 2f;
	}
	
	// Update is called once per frame
	void Update () {
		//Get the enemy current position
		Vector2 position = transform.position;

		//Compute the enemy new position
		position = new Vector2 (position.x, position.y - speed * Time.deltaTime);

		//Update the enemy position
		transform.position = position;

		//Bottom-left position of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		if (transform.position.y < min.y) {
			Destroy(gameObject);
		}

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "PlayerShip") || (col.tag == "PlayerBullet"))
        {
            PlayExplosion();

            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        //set the position of explosion
        explosion.transform.position = transform.position;
    }
}
















