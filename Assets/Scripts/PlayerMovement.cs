using UnityEngine;
using System;
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float fallThreshold = -20f;
    public float speed = 12f;
    public float gravity = 9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool ismoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //reseting the defoult velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //getting the inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Creating the moving Vector
        Vector3 move = transform.right * x + transform.forward * z;//right - red axsis, forward blue axis

        //Actually moving the player
        controller.Move(move * speed * Time.deltaTime);

        //check if the player can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Going up
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            print("Jump"); //!
        }

        //falling down
        velocity.y += gravity * Time.deltaTime;

        //Exectuting the jump
        controller.Move(velocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position && isGrounded == true)
        {
            ismoving = true;
            //For later use
        }
        else
        {
            ismoving = false;
        }
        lastPosition = gameObject.transform.position;
        CheckFallDeath();


    }

    void CheckFallDeath()
    {
        if (transform.position.y < fallThreshold)
        {
            GameManager.Instance.AdjustTime(-999f); // Süreyi bitirir
        }
    }



}
