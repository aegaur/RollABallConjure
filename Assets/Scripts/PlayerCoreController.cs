using UnityEngine;
using System.Collections;

public class PlayerCoreController : MonoBehaviour {

    public GameObject player;
    
	void Update () {
        Debug.Log(transform.position);
        Debug.Log(player.transform.position);
        transform.position = player.transform.position;
	}
}
