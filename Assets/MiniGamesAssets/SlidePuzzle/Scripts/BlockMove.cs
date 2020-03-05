using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGames
{
    public class BlockMove : MonoBehaviour
    {
        public Vector3 right_pos;
        public bool at_pos;
        string hint;

        void Start()
        {
            hint = ((MapCreate)gameObject.transform.parent.gameObject.GetComponent(typeof(MapCreate))).hint;
            if (Mathf.Abs(this.gameObject.transform.position.x - right_pos.x) + Mathf.Abs(this.gameObject.transform.position.y - right_pos.y) < 1)
            {
                at_pos = true;
            }
            else { at_pos = false; }
        }
        void OnMouseDown()
        {
            GameObject empty = GameObject.FindGameObjectWithTag("emptyBlock");
            Vector3 empty_pos = empty.transform.position;
            if (Mathf.Abs(this.gameObject.transform.position.x - empty_pos.x) + Mathf.Abs(this.gameObject.transform.position.y - empty_pos.y) < 4)
            {
                empty.transform.position = this.gameObject.transform.position;
                this.gameObject.transform.position = empty_pos;
                if (Mathf.Abs(this.gameObject.transform.position.x - right_pos.x) + Mathf.Abs(this.gameObject.transform.position.y - right_pos.y) < 1)
                {
                    at_pos = true;
                    if (checkAllBlock())
                    {
                        GameObject.FindWithTag("hint").GetComponent<UnityEngine.UI.Text>().text = hint;
                    }
                }
                else { at_pos = false; }
            }
        }
        private bool checkAllBlock()
        {
            GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");
            for (int i = 0; i < blocks.Length; i++)
            {
                if (!((BlockMove)blocks[i].GetComponent(typeof(BlockMove))).at_pos)
                {
                    return false;
                }
            }
            return true;
        }
    }
}