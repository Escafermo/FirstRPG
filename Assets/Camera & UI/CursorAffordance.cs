using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D enemyCursor = null;
    [SerializeField] Texture2D questionCursor = null;
    [SerializeField] Vector2 cursorHotSpot = new Vector2(0, 0); // Size of assets given by course

    CameraRaycaster cameraRayCaster;

    void Start ()
    {
        cameraRayCaster = GetComponent<CameraRaycaster>();
        cameraRayCaster.onLayerChange += OnLayerChangeCursor; // Registering this function as an observer of CameraRayCaster delegate OnLayerChange()
    }

    void OnLayerChangeCursor(Layer newLayerCursor)
    {
        switch (newLayerCursor)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto); // Can also use CursorMode.ForceSoftware in case of problems here
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
