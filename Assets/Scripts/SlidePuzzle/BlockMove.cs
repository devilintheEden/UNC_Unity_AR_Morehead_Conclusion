using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public Vector3 right_pos;
    public bool at_pos;

    void Start()
    {
        if (Mathf.Abs(this.gameObject.transform.position.x - right_pos.x) + Mathf.Abs(this.gameObject.transform.position.y - right_pos.y) < 1)
        {
            at_pos = true;
            if (checkAllBlock())
            {
                Debug.Log("Game win");
            }
        }
        else { at_pos = false; }
    }
    void OnMouseDown()
    {
        GameObject empty = GameObject.FindGameObjectWithTag("empty");
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
                    GameObject.FindWithTag("hint").GetComponent<UnityEngine.UI.Text>().text = "Congrats! You win!";
                }
            }
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
