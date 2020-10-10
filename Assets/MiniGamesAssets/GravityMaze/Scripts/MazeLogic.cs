using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeLogic : MonoBehaviour
{
    public GameObject prefab_ball;
    public GameObject this_ball;
    public GameObject callee;
    public GameObject text;
    private int count = 0;
    private string succeed_message = "You finish the maze and collect all the pieces!";
    private string fail_message = "You reach the finish line but didn't collect all the pieces. Please go back and collect them.";

    private void Update()
    {
        if(this_ball.transform.position.y < -5)
        {
            Destroy(this_ball);
            this_ball = Instantiate(prefab_ball, gameObject.transform);
        }
    }
    public void OnCollisionAll(string name)
    {
        if (name.Contains("Piece"))
        {
            callee.GetComponent<NewPuzzlePieces>().NewPieces(++count);
        }
        else
        {
            if(count == 4)
            {
                text.GetComponent<UnityEngine.UI.Text>().text = succeed_message;
            }
            else
            {
                text.GetComponent<UnityEngine.UI.Text>().text = fail_message;
            }
        }
    }
}
