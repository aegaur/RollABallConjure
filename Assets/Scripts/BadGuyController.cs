using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BadGuyController : MonoBehaviour {

    public float speed;

    private List<Vector3> positionList;
    private Rigidbody rb;
    private float startTime;
    private float journeyLength;
    private int i;
    private int j;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        positionList = new List<Vector3>(new Vector3[]
        {
            new Vector3(0f, 0.5f, 7.3f), 
            new Vector3(-8.5f, 0.5f, 7f), 
            new Vector3(-8.5f, 0.5f, 4f), 
            new Vector3(-3f, 0.5f, 4f), 
            new Vector3(-3f, 0.5f, -4f), 
            new Vector3(-8.5f, 0.5f, -4f), 
            new Vector3(-8.5f, 0.5f, -7f), 
            new Vector3(8.5f, 0.5f, -7f), 
            new Vector3(8.5f, 0.5f, -4f), 
            new Vector3(3f, 0.5f, -4f), 
            new Vector3(3f, 0.5f, 4f), 
            new Vector3(8.5f, 0.5f, 4f), 
            new Vector3(8.5f, 0.5f, 7f), 
        });
        
        i = 0;
        j = 1;
        startTime = Time.time;
        journeyLength = Vector3.Distance(positionList[i], positionList[j]);
    }
	
	// Update is called once per frame
	void Update () {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        Vector3 newPosition = Vector3.Lerp(positionList[i], positionList[j], fracJourney);

        rb.MovePosition(newPosition);

	    if (newPosition.Equals(positionList[j]))
	    {
	        i = (i + 1) == positionList.Count ? 0 : i + 1;
	        j = (j + 1) == positionList.Count ? 0 : j + 1;
            startTime = Time.time;
            journeyLength = Vector3.Distance(positionList[i], positionList[j]);
        }
    }
}
