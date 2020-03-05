using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGames
{
    public class BlockGenerate : MonoBehaviour
    {
        public int picNum = 0;
        Texture blank;
        Texture pattern;
        Material mat;
        LogicControl logctrl;

        void Start()
        {
            mat = GetComponent<Renderer>().material;
            blank = mat.GetTexture("_MainTex");
            pattern = ((DataSavedScript)GameObject.FindWithTag("data").GetComponent(typeof(DataSavedScript))).matchPuzzle_blocksTexture[picNum]; ;
            logctrl = (LogicControl)GameObject.FindWithTag("rootPrefab").GetComponent(typeof(LogicControl));
        }

        void OnMouseDown()
        {
            if ((!logctrl.locked) && mat.mainTexture == blank)
            {
                mat.SetTexture("_MainTex", pattern);
                logctrl.Inform(picNum, this.gameObject);
            }
        }

        public void FlipToBlank()
        {
            StartCoroutine(Coroutine());
        }

        IEnumerator Coroutine()
        {
            logctrl.locked = true;
            yield return new WaitForSeconds(0.5f);
            mat.SetTexture("_MainTex", blank);
            if(GameObject.FindWithTag("hint").GetComponent<UnityEngine.UI.Text>().text != "Time out! You lose :(")
            {
                logctrl.locked = false;
            }
        }
    }
}
