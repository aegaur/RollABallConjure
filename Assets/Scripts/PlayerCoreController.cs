using UnityEngine;
using System.Collections;

public class PlayerCoreController : MonoBehaviour {

    public GameObject player;
    
	void Update () {
        transform.position = player.transform.position;
	}
}
