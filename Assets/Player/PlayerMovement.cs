using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter thirdPersonPlayer;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickDestination, clickPoint;

    [SerializeField] float walkMoveStopRadius = 0.5f;
    [SerializeField] float attackMoveStopRadius = 3f;

    private bool isInDirectMode = false; // Direct mode = keyboard or gamepad | Indirect mode = mouse

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonPlayer = GetComponent<ThirdPersonCharacter>();
        currentClickDestination = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.G)) // Press G to change between mouse and gamepad. TODO: Allow player to remap this key
        {
            currentClickDestination = transform.position; // Clear the last set ClickTarget
            isInDirectMode = !isInDirectMode; // Toggle
        }

        if (isInDirectMode)
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
            clickPoint = cameraRaycaster.hit.point;
            switch (cameraRaycaster.currentLayerHitMethod)
            {
                case Layer.Walkable:
                    currentClickDestination = ShortenDistance(clickPoint, walkMoveStopRadius); //Shortens the Vector to avoid spinning and bugs
                    break;
                case Layer.Enemy:
                    currentClickDestination = ShortenDistance(clickPoint, attackMoveStopRadius);
                    break;
                default:
                    //print("Error on layer - can't walk there!");
                    return;
            }
        }
        WalkToDestinationRange();
    }

    private void WalkToDestinationRange()
    {
        var playerToClickPoint = currentClickDestination - transform.position;
        if (playerToClickPoint.magnitude >= 0)
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

    Vector3 ShortenDistance (Vector3 destination, float shorteningRange) // Shortens the destination click point by the shortening range
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shorteningRange;
        return destination - reductionVector;
    }

    private void OnDrawGizmos()
    {
        //Draw move gizmos
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, currentClickDestination); // Get a line from current position to the click target
        Gizmos.DrawSphere(currentClickDestination, 0.1f);
        Gizmos.DrawSphere(clickPoint, 0.15f);

        //Draw attack gizmos
        Gizmos.color = new Color(0f, 0f, 255f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
    }
}

