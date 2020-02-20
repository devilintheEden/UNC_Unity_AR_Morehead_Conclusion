using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTexture : MonoBehaviour
{
    [TextArea]
    public string hint;
    public GameObject gold;
    public GameObject effect;
    Texture2D text;
    private int status = 0;

    void Start()
    {
        text = Resources.Load("Arts/phase-" + status) as Texture2D;
        GetComponent<Renderer>().material.SetTexture("_MainTex", text);
    }
    void OnMouseDown()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        Texture2D tex = (Texture2D)hit.transform.GetComponent<Renderer>().material.GetTexture("_MainTex");
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
        Color point_color = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
        Debug.Log((point_color.r + point_color.g) / 2 - point_color.b);
        if ((point_color.r + point_color.g) / 2 - point_color.b > 0.2)
        {
            if (status < 2)
            {
                status++;
                text = Resources.Load("Arts/phase-" + status) as Texture2D;
                GetComponent<Renderer>().material.SetTexture("_MainTex", text);
            }
            else
            {
                Instantiate(gold, gameObject.transform.position, Quaternion.identity);
                Instantiate(effect, gameObject.transform.position, Quaternion.identity);
                GameObject.FindWithTag("hint").GetComponent<UnityEngine.UI.Text>().text = hint;
                Destroy(gameObject);
            }
        }
    }
}
