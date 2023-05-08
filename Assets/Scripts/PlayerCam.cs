using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sens = 2;

    public Transform orientation;

    private float _xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //get mouse input
        var mouseX = Input.GetAxis("Mouse X") * sens;
        var mouseY = Input.GetAxis("Mouse Y") * sens;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        //rotate camera and player
        transform.localEulerAngles = Vector3.right * _xRotation;
        orientation.Rotate(Vector3.up * mouseX);
    }
}
