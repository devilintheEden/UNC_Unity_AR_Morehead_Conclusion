using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGames
{
    public class LogicControl : MonoBehaviour
    {
        [TextArea]
        public string hint;
        public int targetPairs = 4;
        public bool locked = false;
        int completePairs = 0;
        int numActive = -1;
        GameObject picActive = null;

        public void Inform(int picN, GameObject go)
        {
            if (numActive == -1)
            {
                numActive = picN;
                picActive = go;
                return;
            }
            else
            {
                if (numActive != picN)
                {
                    ((BlockGenerate)go.GetComponent(typeof(BlockGenerate))).FlipToBlank();
                    ((BlockGenerate)picActive.GetComponent(typeof(BlockGenerate))).FlipToBlank();
                }
                else
                {
                    completePairs++;
                    if (completePairs == targetPairs)
                    {
                        Destroy(((CountdownTimer)this.gameObject.GetComponent(typeof(CountdownTimer))));
                        GameObject.FindWithTag("hint").GetComponent<UnityEngine.UI.Text>().text = hint;
                    }
                }
                numActive = -1;
                picActive = null;
            }
        }
    }
}