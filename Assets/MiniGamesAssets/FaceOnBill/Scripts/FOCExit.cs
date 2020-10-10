using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
namespace MiniGames
{
    public class FOCExit : MonoBehaviour
    {
        private GameObject ARSO;
        private GameObject root;
        private GameObject ARface;
        void Start()
        {
            root = GameObject.FindGameObjectWithTag("rootPrefab");
            ARSO = GameObject.FindGameObjectWithTag("ARSessionOrigin");
            transform.GetChild(4).GetComponent<Button>().onClick.AddListener(ExitOnClick);
        }

        private void ExitOnClick()
        {
            ARSO.GetComponent<ARFaceManager>().enabled = false;
            ARSO.GetComponent<ExtractFaceTexture>().enabled = false;
            ARSO.GetComponent<ARTrackedImageManager>().enabled = true;
            ARSO.GetComponent<ImageDetected>().enabled = true;
            ARSO.GetComponent<ImageDetected>().detectActive = true;
            ARface = GameObject.FindGameObjectWithTag("ARFace");
            if (ARface != null)
            {
                Destroy(ARface);
            }
            Destroy(root);
        }
    }
}
