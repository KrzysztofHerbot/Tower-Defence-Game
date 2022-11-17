using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    GameObject Gcam;
    Transform cam;

    private void Start()
    {
        Gcam = GameObject.FindGameObjectWithTag("MainCamera");
        cam = Gcam.GetComponent<Transform>();
    }
    void Update()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
