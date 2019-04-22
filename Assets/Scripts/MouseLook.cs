using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float lookSensitivity = 5;
    public float yRotation;
    public float xRotation;
    public float currentYRotation;
    float currentXRotation;
    float yRotationV;
    float xRotationV;
    float lookSmoothDamp = 0.1f;
    public float currentAimRacio = 1; 

    float defaultCameraAngle = 60;
    public float currentTargetCameraAngle = 60;
    float racioZoom = 1;
    float racioZoomV;
    float racioZoomSpeed = 0.2f;

    Camera camera;

    public float headBobSpeed = 1;
    public float headBobStepCounter;
    public float headBobAmountX = 1;
    public float headBobAmountY = 1;
    Vector3 parentLastPos;
    public float eyeHeightRatio = 0.9f;

    private void Start()
    {
        camera = GetComponent<Camera>();
        parentLastPos = GetComponent<Vector3>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        parentLastPos = transform.parent.position;
    }

    void Update()
    {
        headBobStepCounter += Vector3.Distance(parentLastPos, transform.parent.position) * headBobSpeed;
        float newX = Mathf.Sin(headBobStepCounter) * headBobAmountX * currentAimRacio;
        float newY = (Mathf.Cos(headBobStepCounter * 2) * headBobAmountY * -1 * currentAimRacio) + (transform.localScale.y * eyeHeightRatio) - (transform.localScale.y/2); 

        transform.localPosition = new Vector3(newX, newY, transform.localPosition.z);
        parentLastPos = transform.parent.position;

        if (currentAimRacio == 1)
            racioZoom = Mathf.SmoothDamp(racioZoom, 1, ref racioZoomV, racioZoomSpeed);
        else
            racioZoom = Mathf.SmoothDamp(racioZoom, 0, ref racioZoomV, racioZoomSpeed);

        camera.fieldOfView = Mathf.Lerp(currentTargetCameraAngle, defaultCameraAngle, racioZoom);

        yRotation += Input.GetAxis("Mouse X") * lookSensitivity * currentAimRacio;
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity * currentAimRacio;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothDamp);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothDamp);

        transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
    }
}
