using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraFollow : MonoBehaviour {

    private GameObject player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void LateUpdate ()
    {
        transform.position = player.transform.position;
	}
}
