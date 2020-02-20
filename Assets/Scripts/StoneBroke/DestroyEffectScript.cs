using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffectScript : MonoBehaviour
{
    public GameObject small1;
    public GameObject small2;
    void Start()
    {
        GameObject particle;
        for (int i = 0; i < 6; i++)
        {
            GameObject prefab;
            if (Random.value > 0.5)
            {
                prefab = small1;
            }
            else { prefab = small2; }
            Vector3 position = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-1, 1), transform.position.z + Random.Range(-0.5f, 0.5f));
            particle = Instantiate(prefab, position, Quaternion.identity);
            particle.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load("Arts/stone" + (int)Random.Range(1, 3)) as Texture2D);
            particle.GetComponent<Rigidbody>().velocity = new Vector3(3 * (Random.value - 0.5f), 2 * Random.value, 3 * (Random.value - 0.5f));
        }
    }
}
