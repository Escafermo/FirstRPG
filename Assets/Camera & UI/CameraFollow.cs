using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraFollow : MonoBehaviour {

    private GameObject player;
    private Vector3 offset;
    private float currentViewValue = 2f;
    //private Vector3 currentOffset;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //currentOffset = new Vector3(0, 0, 0);
        offset = new Vector3(0, -6, -6);
    }

    void LateUpdate ()
    {
        AdjustCameraPosition();

        #region TESTING ScrollWheel as camera zoom
        //Attempt to make SCROLL WHEEL work as camera zoom ( TODO , has bug when walking)
        //var thisOffset = Input.GetAxis("Mouse ScrollWheel");

        //if (thisOffset > 0f && currentOffset.magnitude < 12)
        //{
        //    offset = new Vector3(0, +1, +1);
        //    currentOffset += offset;
        //}
        //else if (thisOffset > 0f && currentOffset.magnitude > 12)
        //{
        //    currentOffset += new Vector3(0, -1, -1);
        //}
        //else if (thisOffset < 0f && currentOffset.magnitude < 12)
        //{
        //    offset = new Vector3(0, -1, -1);
        //    currentOffset += offset;
        //}
        //else if (thisOffset < 0f && currentOffset.magnitude > 12)
        //{
        //    currentOffset += new Vector3(0, +1, +1);
        //}

        //transform.position = player.transform.position + currentOffset;
        //print(currentOffset.magnitude);
        #endregion
    }

    public void AdjustCameraPosition()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (currentViewValue % 2f == 0f)
            {
                offset = new Vector3(0, -9, -9);
                currentViewValue += 1f;
            }
            else if (currentViewValue % 3f == 0f)
            {
                offset = Vector3.zero;
                currentViewValue -= 2f;
            }
            else
            {
                offset = new Vector3(0, -6, -6);
                currentViewValue += 1f;
            }
        }
        transform.position = player.transform.position + offset;
    }

    #region TESTING camera rotation with Q and E
    //public void AdjustCameraRotation()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        transform.Rotate(new Vector3(0, 45f, 0));
    //    }
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        transform.Rotate(new Vector3(0, -45f, 0));
    //    }
    //}
    #endregion
}
