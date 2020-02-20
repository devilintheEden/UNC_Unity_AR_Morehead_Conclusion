using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
public class DetectImage : MonoBehaviour
{
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        for (int i = 0; i < eventArgs.added.Count; i++)
        {
            string name = eventArgs.added[i].referenceImage.name;
            switch (name)
            {
                case "MatchPuzzle":
                    SceneManager.LoadScene("MatchPuzzle");
                    break;
                case "SlidePuzzle":
                    SceneManager.LoadScene("SlidePuzzle");
                    break;
                case "StoneBroke":
                    SceneManager.LoadScene("ARStoneBroke");
                    break;
            }
        }
    }

}