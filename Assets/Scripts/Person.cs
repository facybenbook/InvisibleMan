﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{

    public float speed = 5;
    public GameObject footprintTemplate;
    //public bool hasPrints;

    [HideInInspector]
    public bool leavesPrints;
    [HideInInspector]
    public bool isPainted;

    //variables that has to do with spawning footprints
    //flourdistance: how long a person has to walk before they stop leaving footprints
    //distanceUntilSpawnFlour: distance until the footprints stop spawning, internal-use only
    //flourinterval: how long a person has to walk until a set of prints is spawned
    //distanceUntilSpawnFlour: distance until the next set of prints is spawned, internal-use only
    public float flourDistance = 5;
    private float distanceUntilStopSpawningFlour;
    public float flourInterval = 1;
    private float distanceUntilSpawnFlour;

    public float afflictionDuration = 10;
    [HideInInspector]
    public float afflictionTimer;

    // Start is called before the first frame update
    void Start()
    {
        distanceUntilSpawnFlour = 0;
        distanceUntilStopSpawningFlour = 0;
        leavesPrints = false;
        isPainted = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator StartLeavingPrints() {

        //start leaving prints
        leavesPrints = true;
        distanceUntilStopSpawningFlour = flourDistance;

        //while this character should leave prints...
        while (leavesPrints) {

            //set the distance until spawning prints to be the distance interval of spawning prints
            distanceUntilSpawnFlour = flourInterval;
            
            //set here to be the start position
            Vector2 startPos = transform.position;

            //while the distance to next flour is greater than 0...
            while (distanceUntilSpawnFlour > 0) {
                
                //set here to be current position
                Vector2 currentPos = transform.position;

                //distance calculated from start to here
                float distanceTravelled = Vector2.Distance(startPos, currentPos);

                //subtract from spawn interval and total flour distance
                distanceUntilSpawnFlour -= distanceTravelled;
                distanceUntilStopSpawningFlour -= distanceTravelled;

                //set here to be the new start point
                startPos = transform.position;

                //wait for a bit before re-calculating location
                yield return new WaitForSeconds(0.1f);
            }

            //when distance is zero or less, spawn some prints
            GameObject newPrints = Instantiate(footprintTemplate, transform.position, transform.rotation);

            //if player has walked far enough, no longer leave any prints
            if(distanceUntilStopSpawningFlour <= 0) {
                leavesPrints = false;
            }

        }
    }
}
