using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public int duration = 10;
    int timeRemaining;
    LogicControl logctrl;

    void Start()
    {
        logctrl = (LogicControl)this.gameObject.GetComponent(typeof(LogicControl));
        timeRemaining = duration;
        GameObject.FindWithTag("timer").GetComponent<UnityEngine.UI.Text>().text = timeRemaining + "";
        Invoke("_tick", 1f);
    }

    private void _tick()
    {
        timeRemaining--;
        GameObject.FindWithTag("timer").GetComponent<UnityEngine.UI.Text>().text = timeRemaining + "";
        if (timeRemaining > 0)
        {
            Invoke("_tick", 1f);
        }
        else
        {
            logctrl.locked = true;
            GameObject.FindWithTag("hint").GetComponent<UnityEngine.UI.Text>().text = "Time out! You lose :(";
        }
    }
}
