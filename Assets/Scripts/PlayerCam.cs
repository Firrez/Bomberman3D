using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sens = 3;
    public Transform orientation;

    private float _xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        var mouseX = Input.GetAxis("Mouse X") * sens;
        var mouseY = Input.GetAxis("Mouse Y") * sens;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        transform.localEulerAngles = Vector3.right * _xRotation;
        orientation.Rotate(Vector3.up * mouseX);
    }
}
