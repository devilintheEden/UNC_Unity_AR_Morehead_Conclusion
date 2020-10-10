using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MiniGames
{
    public class ExitMiniGame : MonoBehaviour
    {
        void Start()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        }

        void TaskOnClick()
        {
            ((ImageDetected)GameObject.FindWithTag("ARSessionOrigin").GetComponent(typeof(ImageDetected))).detectActive = true;
            Destroy(gameObject.transform.parent.parent.gameObject);
        }
    }
}
