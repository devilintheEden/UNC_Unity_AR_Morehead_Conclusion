using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerate : MonoBehaviour
{
    public int picNum = 0;
    Texture blank;
    Texture pattern;
    Material mat;
    LogicControl logctrl;

    void Start()
    {
        blank = Resources.Load("Arts_mat/0") as Texture;
        pattern = Resources.Load("Arts_mat/" + picNum) as Texture;
        mat = GetComponent<Renderer>().material;
        mat.SetTexture("_MainTex", blank);
        logctrl = (LogicControl)GameObject.FindWithTag("scaler").GetComponent(typeof(LogicControl));
    }

    void OnMouseDown()
    {
        if ((!logctrl.locked) && mat.mainTexture == blank)
        {
            mat.SetTexture("_MainTex", pattern);
            logctrl.Inform(picNum, this.gameObject);
        }
    }

    public void flipToBlank()
    {
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        logctrl.locked = true;
        yield return new WaitForSeconds(0.5f);
        mat.SetTexture("_MainTex", blank);
        logctrl.locked = false;
    }
}
