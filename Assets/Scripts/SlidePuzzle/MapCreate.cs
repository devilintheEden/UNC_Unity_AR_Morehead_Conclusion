using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    public int n = 3;
    public Texture2D full_image;
    public GameObject block;
    public GameObject Empty;
    private GameObject spawned;
    void Start()
    {
        bool[] choose_from = new bool[n * n - 1];
        int[] result = new int[n * n - 1];
        for (int i = 0; i < n * n - 1; i++)
        {
            int rand;
            do
            {
                rand = Random.Range(0, n * n - 1);
            } while (choose_from[rand]);
            result[i] = rand;
            choose_from[rand] = true;
        }
        if (!checkValid(result))
        {
            int temp = result[0];
            result[0] = result[1];
            result[1] = temp;
        }

        int texture_len = full_image.width / n;
        Color32[] pixels = full_image.GetPixels32();
        for (int i = 0; i < n * n - 1; i++)
        {
            Vector3 position = new Vector3(-n * 1.1f + 1.1f + (i % n) * 2.2f, n * 1.1f - 1.1f - i / n * 2.2f, 0);
            Texture2D texture = new Texture2D(texture_len, texture_len);
            Color32[] text_pixels = new Color32[texture_len * texture_len];
            int row = result[i] / n;
            int col = result[i] % n;
            for (int j = 0; j < texture_len; j++)
            {
                for (int k = 0; k < texture_len; k++)
                {
                    text_pixels[texture_len * j + k] = pixels[full_image.width * (j + full_image.width - texture_len * (row + 1)) + k + texture_len * col];
                }
            }
            texture.SetPixels32(text_pixels);
            texture.Apply();
            spawned = Instantiate(block, position, Quaternion.identity);
            spawned.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
            ((BlockMove)spawned.GetComponent(typeof(BlockMove))).right_pos = new Vector3(-n * 1.1f + 1.1f + (result[i] % n) * 2.2f, n * 1.1f - 1.1f - result[i] / n * 2.2f, 0);
        }
        Instantiate(Empty, new Vector3(n * 1.1f - 1.1f, -n * 1.1f + 1.1f, 0), Quaternion.identity);
    }

    private bool checkValid(int[] result)
    {
        int sum = 0;
        for (int i = 0; i < result.Length; i++)
        {
            int count = 0;
            for (int j = i + 1; j < result.Length; j++)
            {
                if (result[i] > result[j])
                {
                    count++;
                }
            }
            sum += count;
        }
        sum += result.Length + 1;
        if (sum / 2 * 2 == sum)
        {
            return true;
        }
        return false;
    }
}
