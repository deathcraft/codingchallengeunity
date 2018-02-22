using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class LorenzAttractor : MonoBehaviour
{
    [SerializeField]
    private float x;

    [SerializeField]
    private float y;

    [SerializeField]
    private float z;

    [SerializeField]
    private float sigma;

    [SerializeField]
    private float rou;

    [SerializeField]
    private float beta;

    [SerializeField]
    private GameObject trailRenderer;

    void Update()
    {
        float dx = sigma * (y - x);
        float dy = x * (rou - z) - y;
        float dz = x * y - beta * z;

        x = x + dx * 0.01f;
        y = y + dy * 0.01f;
        z = z + dz * 0.01f;

        trailRenderer.transform.position = new Vector3(x, y, z);
    }
}