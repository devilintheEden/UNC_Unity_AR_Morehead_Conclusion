using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicControl : MonoBehaviour
{
    public int targetPairs = 4;
    public bool locked = false;
    int completePairs = 0;
    int numActive = 0;
    GameObject picActive = null;

    public void Inform(int picN, GameObject go)
    {
        if (numActive == 0)
        {
            numActive = picN;
            picActive = go;
            return;
        }
        else
        {
            if (numActive != picN)
            {
                ((BlockGenerate)go.GetComponent(typeof(BlockGenerate))).flipToBlank();
                ((BlockGenerate)picActive.GetComponent(typeof(BlockGenerate))).flipToBlank();
            }
            else
            {
                completePairs++;
                if (completePairs == targetPairs)
                {
                    Destroy(((CountdownTimer)this.gameObject.GetComponent(typeof(CountdownTimer))));
                    GameObject.FindWithTag("hint").GetComponent<UnityEngine.UI.Text>().text = "Congrats! You win!";
                }
            }
            numActive = 0;
            picActive = null;
        }
    }
}
