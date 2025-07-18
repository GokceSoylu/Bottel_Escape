using UnityEngine;

public class MouseMovememnt : MonoBehaviour
{
    public float mouseSensitivity = 500f;
    float xRotation = 0f;
    float yRotation = 0f;
    public float topClamp = -90f;
    public float bottomClamp = 90f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Getting the mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotatation aroun x axis ( look up  and down )
        xRotation -= mouseY;

        //Clamp the rotatation
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        //Rotatation aroun y axis ( look right and left)
        yRotation += mouseX;

        //Apply rotation to our transform
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);


    }
}
