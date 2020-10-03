using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{

    private Animator animator;

    [SerializeField] float directionDampTime = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();      
    }

    // Update is called once per frame
    void Update()
    {

        // deal with Jumping
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // only allow jumping if we are running.
        if (stateInfo.IsName("Base Layer.Run"))
        {
            // When using trigger parameter
            if (Input.GetButtonDown("Fire2"))
            {
                animator.SetTrigger("Jump");
            }
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (v < 0)                                  //since we are not allowing our player to run backward
        {
            v = 0;
        }
        animator.SetFloat("Speed", h * h + v * v);

        animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);     //make uses of D & A for turning

        
    }
}
