using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSavedScript : MonoBehaviour
{
    public Texture[] matchPuzzle_blocksTexture = new Texture[8];
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            matchPuzzle_blocksTexture[i] = Resources.Load("Paintings_Arts/painting" + (i + 1)) as Texture;
        }
    }
}
