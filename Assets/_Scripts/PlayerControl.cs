using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public GameObject GameManagerGO;//reference to game manager

	public GameObject PlayerButtonGO;//player's button prefab
	public GameObject bulletPosition01;
	public GameObject bulletPosition02;
    public GameObject ExplosionGO;//explosion prefab

    //Reference to the lives ui text
    public Text LivesUIText;

    const int MaxLives = 3;//maximum player lives
    int lives;//current player lives

    public float speed;

    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();

        //Reset the player position to the center of the screen
        transform.position = new Vector2(0, 0);

        //set this game object to active
        gameObject.SetActive(true);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {

            //play the fire sound effect
            GetComponent<AudioSource>().Play();

			GameObject bullet01 = (GameObject)Instantiate(PlayerButtonGO);
			bullet01.transform.position = bulletPosition01.transform.position; //set the bullet initial position

			GameObject bullet02 = (GameObject)Instantiate(PlayerButtonGO);
			bullet02.transform.position = bulletPosition02.transform.position;
		}

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

    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "EnemyShip") || (col.tag == "EnemyBullet"))
        {
            PlayExplosion();

            lives--;
            LivesUIText.text = lives.ToString();

            if(lives == 0)
            {
                //Change game manager state to game over
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

                gameObject.SetActive(false);                
            }
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        //set the position of the explosion
        explosion.transform.position = transform.position;
    }
}
