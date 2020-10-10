using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPuzzlePieces : MonoBehaviour
{
    public Texture2D initial;
    public GameObject vessel;

    private Color32[] initial_pixels;
    private Sprite newSprite;
    private int tex_len;

    void Start()
    {
        initial_pixels = initial.GetPixels32();
        tex_len = initial.width / 2;
        vessel.transform.localScale = new Vector3(100 / (float)tex_len, 100 / (float)tex_len, 1);
    }

    public void NewPieces(int location)
    {
        switch(location){
            case 1:
                CopyTexture(initial.width - tex_len, initial.width - (int)(tex_len * 1.3), tex_len, (int)(tex_len * 1.3));
                Instantiate(vessel, new Vector3(100.7f, 0.5f, 0), Quaternion.identity, this.transform);
                break;
            case 2:
                CopyTexture(0, initial.width - tex_len, (int)(tex_len * 1.3), tex_len);
                Instantiate(vessel, new Vector3(100 - 0.55f, 0.65f, 0), Quaternion.identity, this.transform);
                break;
            case 3:
                CopyTexture(0, 0, tex_len, (int)(tex_len * 1.3));
                Instantiate(vessel, new Vector3(99.3f, -0.6f, 0), Quaternion.identity, this.transform);
                break;
            case 4:
                CopyTexture(initial.width - (int)(tex_len * 1.3), 0, (int)(tex_len * 1.3), tex_len);
                Instantiate(vessel, new Vector3(100.55f, -0.75f, 0), Quaternion.identity, this.transform);
                break;
        }
    }
    void CopyTexture(int x, int y, int w, int h)
    {
        Texture2D tex = new Texture2D(w, h);
        Color32[] tex_pixels = new Color32[w * h];
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                tex_pixels[w * i + j] = initial_pixels[(y + i) * (initial.width) + (x + j)];
            }
        }
        tex.SetPixels32(tex_pixels);
        tex.Apply();
        newSprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        vessel.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
