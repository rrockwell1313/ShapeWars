using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObstacle : MonoBehaviour
{
    public float rotationSpeed = 45f; // Rotation speed in degrees per second

    void Update()
    {
        // Rotate the obstacle around its Z-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
