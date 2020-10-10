using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyroControl : MonoBehaviour
{
    float threshold = 15f;
    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        float delta_x = transform.eulerAngles.x - Input.gyro.rotationRateUnbiased.x * Time.deltaTime * Mathf.Rad2Deg;
        float delta_z = transform.eulerAngles.z + Input.gyro.rotationRateUnbiased.z * Time.deltaTime * Mathf.Rad2Deg;
        delta_x = delta_x <= 180 ? delta_x : delta_x - 360;
        delta_z = delta_z <= 180 ? delta_z : delta_z - 360;
        delta_x = Mathf.Min(delta_x, threshold);
        delta_x = Mathf.Max(delta_x, -threshold);
        delta_z = Mathf.Min(delta_z, threshold);
        delta_z = Mathf.Max(delta_z, -threshold);
        transform.eulerAngles = new Vector3(delta_x, 0.0f, delta_z);
    }

}
