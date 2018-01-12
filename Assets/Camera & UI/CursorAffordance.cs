using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D enemyCursor = null;
    [SerializeField] Texture2D questionCursor = null;
    [SerializeField] Vector2 cursorHotSpot = new Vector2(96, 96); // Size of assets given by course

    CameraRaycaster cameraRayCaster;

    void Start ()
    {
        cameraRayCaster = GetComponent<CameraRaycaster>();
    }

    void LateUpdate()
    {
        switch (cameraRayCaster.layerHit)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(enemyCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case Layer.RaycastEndStop:
                Cursor.SetCursor(questionCursor, cursorHotSpot, CursorMode.Auto);
                break;
            default:
                Debug.LogError("Can't find cursor to show");
                return;
        }
    }
}
