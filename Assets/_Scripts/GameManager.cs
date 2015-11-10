using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //Reference to our game objects
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;//reference to enemy spawner
    public GameObject GameOverGO;//reference to game over bg

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;

	// Use this for initialization
	void Start () {
        GMState = GameManagerState.Opening;
    }
	
	//Function to update the game manager state
    void UpdateGameManagerState()
    {
        switch(GMState)
        {
            case GameManagerState.Opening:

                //Hide game over
                GameOverGO.SetActive(false);

                //Set play button visible
                playButton.SetActive(true);

                break;
            case GameManagerState.Gameplay:

                //hide play button on game play state
                playButton.SetActive(false);

                //set the player visible and init the player
                playerShip.GetComponent<PlayerControl>().Init();

                //Start enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

                break;
            case GameManagerState.GameOver:

                //Stop enemy spawning
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawning();

                //Display game over
                GameOverGO.SetActive(true);

                //Change game manager state to Opening state
                Invoke("ChangeToOpeningState", 8f);

                break;

        }
    }

    //Function to set the game manager state
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    //Change game manager state to opening
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
