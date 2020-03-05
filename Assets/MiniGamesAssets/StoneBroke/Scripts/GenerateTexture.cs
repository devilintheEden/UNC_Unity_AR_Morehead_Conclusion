using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGames
{
    public class GenerateTexture : MonoBehaviour
    {

        public GameObject gold;
        public GameObject effect;
        private int status = 0;
        Texture2D initial;
        Texture2D complement;
        Color32[] initial_pixels;
        Color32[] comple_pixels;
        void Start()
        {
            //hint = ((ObjectPlaced)gameObject.transform.parent.gameObject.GetComponent(typeof(ObjectPlaced))).hint;
            initial = Instantiate(Resources.Load("StoneBroke_Texture_Arts/stone1") as Texture2D) as Texture2D;
            complement = Resources.Load("StoneBroke_Texture_Arts/gold") as Texture2D;
            initial_pixels = initial.GetPixels32();
            comple_pixels = complement.GetPixels32();
            Vector2 center = new Vector2(Random.value * 156 + 50, Random.value * 156 + 50);
            initial_pixels = GenerateCrack(initial_pixels, comple_pixels, center, Random.value * (2 * Mathf.PI / 3), 20 + (int)(Random.value * 30));
            initial_pixels = GenerateCrack(initial_pixels, comple_pixels, center, 2 * Mathf.PI / 3 + Random.value * (2 * Mathf.PI / 3), 20 + (int)(Random.value * 30));
            initial_pixels = GenerateCrack(initial_pixels, comple_pixels, center, 4 * Mathf.PI / 3 + Random.value * (2 * Mathf.PI / 3), 20 + (int)(Random.value * 30));
            initial.SetPixels32(initial_pixels);
            initial.Apply();
            GetComponent<Renderer>().material.SetTexture("_MainTex", initial);
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
            if ((point_color.r + point_color.g) / 2 - point_color.b > 0.2)
            {
                if (status < 2)
                {
                    status++;
                    if (status == 1)
                    {
                        initial_pixels = GenerateCrack(initial_pixels, comple_pixels, pixelUV, Random.value * (2 * Mathf.PI / 3) + (2 * Mathf.PI / 9), 15 + (int)(Random.value * 30));
                        initial_pixels = GenerateCrack(initial_pixels, comple_pixels, pixelUV, 2 * Mathf.PI / 3 + Random.value * (2 * Mathf.PI / 3) + (2 * Mathf.PI / 9), 15 + (int)(Random.value * 30));
                        initial_pixels = GenerateCrack(initial_pixels, comple_pixels, pixelUV, 4 * Mathf.PI / 3 + Random.value * (15 * Mathf.PI / 3) + (2 * Mathf.PI / 9), 15 + (int)(Random.value * 30));
                    }
                    else
                    {
                        initial_pixels = GenerateCrack(initial_pixels, comple_pixels, pixelUV, Random.value * (2 * Mathf.PI / 3) + (4 * Mathf.PI / 9), 15 + (int)(Random.value * 30));
                        initial_pixels = GenerateCrack(initial_pixels, comple_pixels, pixelUV, 2 * Mathf.PI / 3 + Random.value * (2 * Mathf.PI / 3) + (4 * Mathf.PI / 9), 15 + (int)(Random.value * 30));
                        initial_pixels = GenerateCrack(initial_pixels, comple_pixels, pixelUV, 4 * Mathf.PI / 3 + Random.value * (15 * Mathf.PI / 3) + (4 * Mathf.PI / 9), 15 + (int)(Random.value * 30));
                    }
                    initial.SetPixels32(initial_pixels);
                    initial.Apply();
                    GetComponent<Renderer>().material.SetTexture("_MainTex", initial);
                    GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
                }
                else
                {
                    Instantiate(gold, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                    Instantiate(effect, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
                    string hint = ((ObjectPlaced)gameObject.transform.parent.gameObject.GetComponent(typeof(ObjectPlaced))).hint;
                    GameObject.FindWithTag("hint").GetComponent<UnityEngine.UI.Text>().text = hint;
                    Destroy(gameObject);
                }
            }
        }

        Color32[] GenerateCrack(Color32[] target, Color32[] comple, Vector2 startpoint, double angle, int distance)
        {
            startpoint.x = (int)startpoint.x;
            startpoint.y = (int)startpoint.y;
            if (angle > Mathf.PI * 2)
            {
                angle -= Mathf.PI * 2;
            }
            Vector2 endpoint = new Vector2((int)(startpoint.x + distance * Mathf.Cos((float)angle)), (int)(startpoint.y + distance * Mathf.Sin((float)angle)));
            if (startpoint.x == endpoint.x)
            {
                endpoint.x++;
            }
            if (startpoint.y == endpoint.y)
            {
                endpoint.y++;
            }
            int directionX = (int)((endpoint.x - startpoint.x) / Mathf.Abs(startpoint.x - endpoint.x));
            int directionY = (int)((endpoint.y - startpoint.y) / Mathf.Abs(startpoint.y - endpoint.y));
            Vector2 now = startpoint;
            target[256 * (int)now.y + (int)now.x] = comple[256 * (int)now.y + (int)now.x];
            if (Mathf.Abs((endpoint.x - startpoint.x) / (endpoint.y - startpoint.y)) >= 1)
            {
                while (now != endpoint)
                {
                    if (Random.value <= Mathf.Abs((endpoint.y - now.y) / (endpoint.x - now.x)))
                    {
                        now.x += directionX;
                        now.y += directionY;
                    }
                    else
                    {
                        now.x += directionX;
                    }
                    target[256 * (int)now.y + (int)now.x] = comple[256 * (int)now.y + (int)now.x];
                    Color point_color = comple[256 * (int)now.y + (int)now.x];
                    int x = Random.Range(1, 3);
                    for (int i = 0; i < x; i++)
                    {
                        target[256 * (int)now.y + (int)now.x - i] = comple[256 * (int)now.y + (int)now.x - i];
                        target[256 * (int)now.y + (int)now.x + i] = comple[256 * (int)now.y + (int)now.x + i];
                    }
                }
            }
            else
            {
                while (now != endpoint)
                {
                    if (Random.value <= Mathf.Abs((endpoint.x - now.x) / (endpoint.y - now.y)))
                    {
                        now.x += directionX;
                        now.y += directionY;
                    }
                    else
                    {
                        now.y += directionY;
                    }

                    target[256 * (int)now.y + (int)now.x] = comple[256 * (int)now.y + (int)now.x];
                    Color point_color = comple[256 * (int)now.y + (int)now.x];
                    int x = Random.Range(1, 3);
                    for (int i = 0; i < x; i++)
                    {
                        target[256 * (int)now.y + (int)now.x - i] = comple[256 * (int)now.y + (int)now.x - i];
                        target[256 * (int)now.y + (int)now.x + i] = comple[256 * (int)now.y + (int)now.x + i];
                    }
                }
            }
            return target;
        }

    }
}