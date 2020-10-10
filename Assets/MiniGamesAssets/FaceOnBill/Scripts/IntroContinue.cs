using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
namespace MiniGames
{
    public class IntroContinue : MonoBehaviour
    {
        public Sprite GWintro2;
        private int step = 0;
        private GameObject ARSO;
        private GameObject canvas_2;
        void Start()
        {
            ARSO = GameObject.FindGameObjectWithTag("ARSessionOrigin");
            canvas_2 = transform.parent.GetChild(1).gameObject;
            canvas_2.SetActive(false);
            transform.GetChild(1).GetComponent<Button>().onClick.AddListener(ContinueOnClick);
        }

        private void ContinueOnClick()
        {
            if (step == 0)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = GWintro2;
                step = 1;
            }
            else
            {
                ARSO.GetComponent<ARTrackedImageManager>().enabled = false;
                ARSO.GetComponent<ImageDetected>().enabled = false;
                canvas_2.SetActive(true);
                ARSO.GetComponent<ARFaceManager>().enabled = true;
                ARSO.GetComponent<ExtractFaceTexture>().canvas = canvas_2;
                ARSO.GetComponent<ExtractFaceTexture>().enabled = true;
                gameObject.SetActive(false);
            }
        }
    }
}
