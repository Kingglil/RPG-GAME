using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float xRotation = 0f;
    private float yRotation = 0f;

    private float turnSpeed = 2f;
    private float sensitivityMultiplier = 4f;

    private float delta;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        delta = AbilitiesManager.stopTime ? Time.unscaledDeltaTime : Time.deltaTime;
        if (InputManager.Controller && Input.GetAxis("RSB") == 0)
        {
            turnSpeed = Input.GetAxis("LT") * sensitivityMultiplier + 2;
        }

        yRotation += Input.GetAxis("RSH") * delta * turnSpeed;
        xRotation += Input.GetAxis("RSV") * delta * turnSpeed;

        Quaternion horizontalRotation = new Quaternion(0, Mathf.Sin(yRotation / 2), 0, Mathf.Cos(yRotation / 2));
        Quaternion verticalRotation = new Quaternion(Mathf.Sin(xRotation / 2), 0, 0, Mathf.Cos(xRotation / 2));

        transform.rotation =  horizontalRotation * verticalRotation;

        transform.root.rotation = horizontalRotation;
    }
}
