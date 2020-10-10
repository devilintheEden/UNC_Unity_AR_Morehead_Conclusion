using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameObject callee;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("enter");
        callee.GetComponent<MazeLogic>().OnCollisionAll(gameObject.name);
        if(gameObject.name != "FinishLine")
        {
            Destroy(gameObject);
        }
    }
}
