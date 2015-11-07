using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    float speed;

	// Use this for initialization
	void Start () {
        speed = 8f;
	}
	
	// Update is called once per frame
	void Update () {
        //Get the bullet current position
        Vector2 position = transform.position;

        //compute the bullet's new position
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        //Update the bullet's position
        transform.position = position;

		//top-right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		if (transform.position.y > max.y) {
			Destroy(gameObject);
		}
	}
}
