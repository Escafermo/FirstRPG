using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraFollow : MonoBehaviour {

    private GameObject player;
    private Vector3 offset;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void LateUpdate ()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) // First view
        {
            offset = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) // Second view
        {
            offset = new Vector3(0, -6, -6);
        } 
        if (Input.GetKeyDown(KeyCode.Keypad3)) // Third view
        {
            offset = new Vector3(0, -9, -9);
        }
        transform.position = player.transform.position + offset;
    }
}
