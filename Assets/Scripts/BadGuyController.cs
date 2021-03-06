﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BadGuyController : MonoBehaviour {

    public float speed;
    public float magicPickupDropTime;
    public GameObject magicPickUp;

    private List<Vector3> positionList;
    private Rigidbody rb;
    private float positionStartTime;
    private float magicPickupStartTime;
    private float journeyLength;
    private int i;
    private int j;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        positionList = new List<Vector3>(new Vector3[]
        {
            new Vector3(0f, 1f, 7.3f), 
            new Vector3(-8.5f, 1f, 7f), 
            new Vector3(-8.5f, 1f, 4f), 
            new Vector3(-3f, 1f, 4f), 
            new Vector3(-3f, 1f, -4f), 
            new Vector3(-8.5f, 1f, -4f), 
            new Vector3(-8.5f, 1f, -7f), 
            new Vector3(8.5f, 1f, -7f), 
            new Vector3(8.5f, 1f, -4f), 
            new Vector3(3f, 1f, -4f), 
            new Vector3(3f, 1f, 4f), 
            new Vector3(8.5f, 1f, 4f), 
            new Vector3(8.5f, 1f, 7f), 
        });
        
        i = 0;
        j = 1;
        positionStartTime = Time.time;
        magicPickupStartTime = Time.time;
        journeyLength = Vector3.Distance(positionList[i], positionList[j]);
    }
	
	// Update is called once per frame
	void Update () {
        float distCovered = (Time.time - positionStartTime) * speed;
        float fracJourney = distCovered / journeyLength;
        Vector3 newPosition = Vector3.Lerp(positionList[i], positionList[j], fracJourney);

        rb.MovePosition(newPosition);

	    if (newPosition.Equals(positionList[j]))
	    {
	        i = (i + 1) == positionList.Count ? 0 : i + 1;
	        j = (j + 1) == positionList.Count ? 0 : j + 1;
            positionStartTime = Time.time;
            journeyLength = Vector3.Distance(positionList[i], positionList[j]);
        }

        if ((Time.time - magicPickupStartTime) >= magicPickupDropTime)
	    {
	        GameObject magicPickUpInstance = (GameObject) Instantiate(magicPickUp, new Vector3(newPosition.x, 0.5f, newPosition.z), Quaternion.identity, transform.parent);

            magicPickUpInstance.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV(0f,1f,0f,1f,0f,1f,1f,1f);
	        magicPickupStartTime = Time.time;
	    }
    }
}
