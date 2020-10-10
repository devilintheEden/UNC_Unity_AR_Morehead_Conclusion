using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageDetected : MonoBehaviour
{
    public bool detectActive = true;
    public GameObject SlidePuzzlePrefab;
    public GameObject MatchPuzzlePrefab;
    public GameObject StoneBrokePrefab;
    public GameObject GravityMazePrefab;
    public GameObject FaceOnCashPrefab;

    void Awake()
    {
        gameObject.GetComponent<ARTrackedImageManager>().trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        string name;
        if (detectActive)
        {
            for (int i = 0; i < eventArgs.added.Count; i++)
            {
                name = eventArgs.added[i].referenceImage.name;
                switch (name)
                {
                    case "painting1":
                        Instantiate(SlidePuzzlePrefab);
                        break;
                    case "painting2":
                        Instantiate(MatchPuzzlePrefab);
                        break;
                    case "PaulusVanBeresteyn":
                        Instantiate(StoneBrokePrefab);
                        break;
                    case "painting4":
                        Instantiate(GravityMazePrefab);
                        break;
                    case "GeorgeWashington":
                        Instantiate(FaceOnCashPrefab);
                        break;
                }
                detectActive = false;
            }
            for (int i = 0; i < eventArgs.updated.Count; i++)
            {
                name = eventArgs.updated[i].referenceImage.name;
                if (eventArgs.updated[i].trackingState == TrackingState.Tracking)
                {
                    switch (name)
                    {
                        case "painting1":
                            Instantiate(SlidePuzzlePrefab);
                            break;
                        case "painting2":
                            Instantiate(MatchPuzzlePrefab);
                            break;
                        case "PaulusVanBeresteyn":
                            Instantiate(StoneBrokePrefab);
                            break;
                        case "painting4":
                            Instantiate(GravityMazePrefab);
                            break;
                        case "GeorgeWashington":
                            Instantiate(FaceOnCashPrefab);
                            break;
                    }
                    detectActive = false;
                }                
            }
        }
    }
}
