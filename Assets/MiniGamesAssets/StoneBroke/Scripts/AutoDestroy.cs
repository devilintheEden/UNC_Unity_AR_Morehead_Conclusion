using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGames
{
    public class AutoDestroy : MonoBehaviour
    {
        void Update()
        {
            if (gameObject.transform.position.y < -30f)
            {
                Destroy(gameObject);
            }
        }
    }
}
