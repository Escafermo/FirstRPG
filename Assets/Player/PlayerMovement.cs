using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter thirdPersonPlayer;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

    [SerializeField] float walkMoveStopRadius = 0.2f;

    private bool isInDirectMode = false; // Direct mode = keyboard or gamepad | Indirect mode = mouse

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonPlayer = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.G)) // Press G to change between mouse and gamepad. TODO: Allow player to remap this key
        {
            currentClickTarget = transform.position; // Clear the last set ClickTarget
            isInDirectMode = !isInDirectMode; // Toggle
        }

        if (!isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovement();
        }
    }

    private void ProcessMouseMovement() //Mouse movement
    {
        if (Input.GetMouseButton(0))
        {
            //print("Cursor raycast hit" + cameraRaycaster.hit.collider.gameObject.name.ToString());
            //print("Cursor layer  hit" + cameraRaycaster.layerHit);

            switch (cameraRaycaster.currentLayerHitMethod)
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;  // So not set in default case
                    break;
                case Layer.Enemy:
                    //print("Not moving to enemy");
                    break;
                default:
                    //print("Error on layer - can't walk there!");
                    return;
            }
        }
        var playerToClickPoint = currentClickTarget - transform.position;
        if (playerToClickPoint.magnitude >= walkMoveStopRadius)
        {
            thirdPersonPlayer.Move(playerToClickPoint, false, false);
        }
        else
        {
            thirdPersonPlayer.Move(Vector3.zero, false, false);
        }
    }

    private void ProcessDirectMovement ()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal"); // Could be CROSSPLATFORM
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveVector = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonPlayer.Move(moveVector, false, false);
    }
}

