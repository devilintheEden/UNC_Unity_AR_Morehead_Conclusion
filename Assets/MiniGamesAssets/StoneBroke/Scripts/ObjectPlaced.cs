using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
namespace MiniGames
{
    public class ObjectPlaced : MonoBehaviour
    {
        [TextArea]
        public string hint;
        public GameObject exit;
        public GameObject bigRock;
        private ARPlaneManager arPlaneManager;
        private bool Rockplaced = false;

        void Start()
        {
            GameObject.FindWithTag("hint").GetComponent<UnityEngine.UI.Text>().text = "Now find a plane to place the object.";
        }

        void Awake()
        {
            arPlaneManager = GameObject.FindWithTag("ARSessionOrigin").GetComponent<ARPlaneManager>();
            arPlaneManager.planesChanged += OnPlanesChanged;
        }

        private void OnPlanesChanged(ARPlanesChangedEventArgs args)
        {
            if (!Rockplaced)
            {
                ARPlane arPlane = args.added[0];
                Vector3 position = new Vector3(arPlane.transform.position.x, arPlane.transform.position.y + 0.5f, arPlane.transform.position.z);
                Instantiate(bigRock, position, Quaternion.identity, gameObject.transform);
                Rockplaced = true;
                GameObject.FindWithTag("hint").GetComponent<UnityEngine.UI.Text>().text = "The Object is placed! Find the golden crack and click on it.";
                exit.SetActive(true);
            }
        }
    }
}