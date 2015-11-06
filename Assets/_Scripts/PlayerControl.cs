using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
	}

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //bottom-left corner
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //top-right corner

        max.x = max.x - 0.225f; //substract the player sprite half width
        min.x = min.x + 0.225f; //add the player sprite half width

        max.y = max.y - 0.0285f; //substract the player sprite half height
        min.y = min.y + 0.285f; //add the player sprite half width

        //Get the player current position
        Vector2 pos = transform.position;

        //Calculate new position 
        pos += direction * speed * Time.deltaTime;

        //Make sure the new position is not outside the screen
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //Update the player's position
        transform.position = pos;
    }
}
