﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiniGames
{
    public class GameInstant : MonoBehaviour
    {
        public bool isFour = true;
        public GameObject block;
        void Start()
        {
            int picN;
            GameObject newBlock;
            BlockGenerate script;
            if (isFour)
            {
                int[] possi = new int[] { 0, 1, 2, 3, 0, 1, 2, 3 };
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i != 0 || j != 0)
                        {
                            newBlock = Instantiate(block, new Vector3(2.2f * i, 2.2f * j, 6.9f), Quaternion.identity, this.gameObject.transform);
                            script = (BlockGenerate)newBlock.GetComponent(typeof(BlockGenerate));
                            do
                            {
                                picN = Random.Range(0, 8);
                            } while (possi[picN] == -1);
                            script.picNum = possi[picN];
                            possi[picN] = -1;
                        }
                    }
                }
            }
            else
            {
                ((LogicControl)this.gameObject.GetComponent(typeof(LogicControl))).targetPairs = 8;
                transform.localScale = new Vector3(1.5f, 1.5f, 1);
                int[] possi = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7 };
                for (int i = -3; i <= 3; i += 2)
                {
                    for (int j = -3; j <= 3; j += 2)
                    {
                        newBlock = (GameObject)Instantiate(block, new Vector3(0.825f * i, 0.825f * j, 6.9f), Quaternion.identity, this.gameObject.transform);
                        script = (BlockGenerate)newBlock.GetComponent(typeof(BlockGenerate));
                        do
                        {
                            picN = Random.Range(0, 16);
                        } while (possi[picN] == -1);
                        script.picNum = possi[picN];
                        possi[picN] = -1;
                    }
                }
            }
        }
    }
}
