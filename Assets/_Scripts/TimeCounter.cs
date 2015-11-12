using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCounter : MonoBehaviour {

	Text timeUI;//reference to the time counter UI 

	float startTime;//time when the player clicks on play
	float ellapsedTime;//the ellapsed time after the user clicks on play
	bool startCounter;//flag to start the counter

	int minutes;
	int seconds;

	// Use this for initialization
	void Start () {
		startCounter = false;

		//get the Text UI component from this gameObject
		timeUI = GetComponent<Text> ();
	}

	//Function to start the time counter
	public void StartTimeCounter() {
		startTime = Time.time;
		startCounter = true;
	}

	//Stop the time counter
	public void StopTimeCounter() {
		startCounter = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (startCounter) {
			//computer the ellapsed time
			ellapsedTime = Time.time - startTime;

			minutes = (int)ellapsedTime / 60;
			seconds = (int)ellapsedTime % 60;

			//update the time counter UI Text
			timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
		}
	}
}
