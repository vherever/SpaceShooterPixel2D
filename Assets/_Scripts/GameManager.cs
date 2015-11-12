using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //Reference to our game objects
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;//reference to enemy spawner
    public GameObject GameOverGO;//reference to game over bg
    public GameObject scoreUITextGO;//reference to the score text UI game object
	public GameObject TimeCounterGO;//reference to the time counter game object
	public GameObject GameTitleGO;//reference to the GameTitleGO

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

				//Display the game title
				GameTitleGO.SetActive(true);

                //Set play button visible
                playButton.SetActive(true);

                break;
            case GameManagerState.Gameplay:

                //Reset the score
                scoreUITextGO.GetComponent<GameScore>().Score = 0;

                //hide play button on game play state
                playButton.SetActive(false);

				//Hide the game title
				GameTitleGO.SetActive(false);

                //set the player visible and init the player
                playerShip.GetComponent<PlayerControl>().Init();

                //Start enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

				//start the time counter
				TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

                break;
            case GameManagerState.GameOver:
				
				//stop the time counter
				TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

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
