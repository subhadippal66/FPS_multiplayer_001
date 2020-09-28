using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSensivity;
    [SerializeField] Transform playerBody;
    float yRot;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MouseControl();
    }

    void MouseControl()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y");

        yRot = yRot - mouseY;
        yRot = Mathf.Clamp(yRot, -90, 90);
        transform.localRotation = Quaternion.Euler(yRot, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
