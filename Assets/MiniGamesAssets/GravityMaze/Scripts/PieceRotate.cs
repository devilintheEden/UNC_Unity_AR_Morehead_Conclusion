﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceRotate : MonoBehaviour
{
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 60 * Time.deltaTime);

    }
}
