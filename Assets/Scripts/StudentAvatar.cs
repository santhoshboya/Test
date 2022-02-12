using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentAvatar : MonoBehaviour
{

    [SerializeField]
    private float MoveForce = 10f;

    [SerializeField]
    public Animator anim;

    public enum MovementState { idel, wave, walk, jump, sit, stand };

    public MovementState state;
    public bool sit;

    private float velocityX=0;
    private float velocityZ=0;
    public float accelerationForce = 0.2f;
    public float descelerationForce = 0.2f;

    public Vector3 moveDir;


    public int animationState = Animator.StringToHash("state");
    private int velocityXHashCode = Animator.StringToHash("VelocityX");
    private int velocityZHashCode = Animator.StringToHash("VelocityZ");

    public bool isClickedOnAvatar = false;



    void Start()
    {
        state = MovementState.idel;
        sit = false;
       
    }

    void Update()
    {
        UpdateAnimationState();    
    }

    private void OnMouseDown()
    {
        isClickedOnAvatar = !isClickedOnAvatar;
    }

    private void UpdateAnimationState()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (state == MovementState.sit)
            {
                state = MovementState.stand;
                sit = false;
            }
            else
            {
                state = MovementState.sit;
                sit = true;
            }
        }
        else
        {
            if (sit)
            {
                state = MovementState.sit;
            }
            else
            {
                state = MovementState.idel;
            }
        }

        if (!sit)
        {
            AcceleratePlayerMovement();

            DesceleratePlayerMovement();

            if (Input.GetKey(KeyCode.H))
            {
                state = MovementState.wave;
            }


            if (Input.GetKey(KeyCode.Space))
            {
                state = MovementState.jump;

            }
        }

        anim.SetInteger(animationState, (int)state);
        anim.SetFloat(velocityXHashCode, velocityX);
        anim.SetFloat(velocityZHashCode, velocityZ);
    }

    public void AcceleratePlayerMovement()
    {
        if (moveDir.x > 0)
        {
            state = MovementState.walk;
            if (velocityX < 0.5f)
                velocityX += Time.deltaTime * accelerationForce;
        }
        if (moveDir.x < 0)
        {
            state = MovementState.walk;
            if (velocityX > -0.5f)
                velocityX -= Time.deltaTime * descelerationForce;
        }
        if (moveDir.z > 0)
        {
            state = MovementState.walk;
            if (velocityZ < 0.5f)
                velocityZ += Time.deltaTime * accelerationForce;
        }
        if (moveDir.z < 0)
        {
            state = MovementState.walk;
            if (velocityZ > -0.5f)
                velocityZ -= Time.deltaTime * descelerationForce;
        }
    }
    public void DesceleratePlayerMovement()
    {
        if (!(moveDir.x > 0) & velocityX > 0)
        {
            velocityX -= Time.deltaTime * descelerationForce;
        }
        if (!(moveDir.x < 0) & velocityX < 0)
        {
            velocityX += Time.deltaTime * descelerationForce;
        }
        if (!(moveDir.z > 0) & velocityZ > 0)
        {
            velocityZ -= Time.deltaTime * descelerationForce;
        }
        if (!(moveDir.z < 0) & velocityZ < 0)
        {
            velocityZ += Time.deltaTime * descelerationForce;
        }
    }
}
