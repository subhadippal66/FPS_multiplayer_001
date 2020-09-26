using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController chCont;
    [SerializeField] float speed = 10f;

    [SerializeField] float gravity = -9.81f;
    Vector3 fallVelocity;

    [SerializeField] Transform groundCheck;  //an empty which is in the bottom of player yelling the position to check
    [SerializeField] float sphereRad = .4f;  //an argument of physics.checksphere
    [SerializeField] LayerMask groundLayer;  //to specify which object to collide and that to ignore 
    bool isGrounded;
    [SerializeField] float jumpVelocity = 20f;
 
    // Start is called before the first frame update
    void Start()
    {
        chCont = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
        HandleGravity();
        Jump();
    }

    public void Jump()    //handles jump
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            fallVelocity.y = jumpVelocity * Time.deltaTime;
        }
    }

    public void HandleGravity()   //this will handle gravity in the scene
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRad, groundLayer);  //needs 3 argument
        if (isGrounded && fallVelocity.y < 0)    //check if the player is in the grolund or not
        {
            fallVelocity.y = 0f;
        }
        fallVelocity.y += gravity * Time.deltaTime * Time.deltaTime;  //physics formula
        chCont.Move(fallVelocity);
    }

    public void HandleMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveVect = transform.forward * moveZ + transform.right * moveX;
        chCont.Move(moveVect * speed * Time.deltaTime);
    }
}
