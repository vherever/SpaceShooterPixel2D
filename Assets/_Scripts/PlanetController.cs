using UnityEngine;
using System.Collections;
using System.Collections.Generic;//For Queue

public class PlanetController : MonoBehaviour {

    public GameObject[] Planets; //array of PlanetGO prefabs

    //Queue to hold the planets
    Queue<GameObject> availablePlanets = new Queue<GameObject>();


	// Use this for initialization
	void Start () {
        //add the planets to the Queue
        availablePlanets.Enqueue(Planets[0]);
        availablePlanets.Enqueue(Planets[1]);
        availablePlanets.Enqueue(Planets[2]);

        //call the MovePlanetDown function every 20 seconds
        InvokeRepeating("MovePlanetDown", 0, 20f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //Dequeue the planet, and set its Moving flag to true so then the planet starts scrolling down the screen
    void MovePlanetDown()
    {
        EnqueuePlanets();

        if(availablePlanets.Count == 0)
        {
            return;
        }

        //Get the planet from the queue
        GameObject aPlanet = availablePlanets.Dequeue();

        //Set the planet isMoving flag to true
        aPlanet.GetComponent<Planet>().isMoving = true;
    }

    //Function to Enqueue planets that are below the screen and are not moving
    void EnqueuePlanets()
    {
        foreach(GameObject aPlanet in Planets)
        {
            //if the planet is below the screen and is not moving
            if((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet>().isMoving))
            {
                //reset the planet position
                aPlanet.GetComponent<Planet>().ResetPosition();

                //Enqueue the planet
                availablePlanets.Enqueue(aPlanet);
            }
        }
    }
}
