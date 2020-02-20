using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class StoneSpawn : MonoBehaviour
{
    public GameObject bigRock;
    private ARPlaneManager arPlaneManager;
    private GameObject spawnedObject;
    private bool stone_placed = false;
    void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.planesChanged += PlaneChanged;
    }

    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if (!stone_placed)
        {
            ARPlane arPlane = args.added[0];
            Vector3 position = new Vector3(arPlane.transform.position.x, arPlane.transform.position.y + 0.5f, arPlane.transform.position.z);
            spawnedObject = Instantiate(bigRock, position, Quaternion.identity);
            stone_placed = true;
        }
    }
}
