using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlacement : MonoBehaviour
{

    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
