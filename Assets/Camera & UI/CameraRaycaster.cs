using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    //( TODO : consider de-registering OnLayerChange on leaving all game scenes

    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit raycastHit;
    public RaycastHit hit
    {
        get { return raycastHit; }
    }

    Layer layerHit;
    public Layer currentLayerHitMethod
    {
        get { return layerHit; }
    }

    public delegate void OnLayerChange(Layer newLayer); // Declare new delegate type 
    public event OnLayerChange onLayerChange; /*Instantiate an observer SET or POOL or LIST and declares it as an EVENT type 
                                                    (from the outside of this script its only possible to register or de-register from it, not change it completely )*/
    void Start() 
    {
        viewCamera = Camera.main;

    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer currentLayer in layerPriorities)
        {
            var hit = RaycastForLayer(currentLayer);
            if (hit.HasValue)
            {
                raycastHit = hit.Value;
                if (layerHit != currentLayer) //If layer has changed
                {
                    layerHit = currentLayer; 
                    onLayerChange(currentLayer); // Call the delegates, since layerChangeObservers is a POINTER to functions
                }
                layerHit = currentLayer; // If layer has changed
                return;
            }
        }

        // Otherwise return background hit
        raycastHit.distance = distanceToBackground;
        layerHit = Layer.RaycastEndStop;
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
