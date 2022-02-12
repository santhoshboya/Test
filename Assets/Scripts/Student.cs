using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{
    [Serializable]
    public struct StudentElements
    {
        public StudentDetailsAndOptions studentDetailsAndOptions;
        public StudentAvatar studentAvatar;
        public Transform ViewPoint;
    }

    public StudentElements studentElements;

    public Vector3 offeset= new Vector3(-1.5f, 3.2f,0);


    public float mouseSensitivity = 1f;
    private float veriticalRotStore;
    private Vector2 mouseInput;
    public bool invertLook = false;
    private Vector3 moveDir;
    private Vector3 movement;
    public float moveSpeed = 2.5f;
    public CharacterController charCon;
    public float runSpeed = 10f;
    public float activeSpeed;
    private Camera cam;
    private float gravityMode = 2.5f;
    public float jumpForce = 7.5f;
    public Transform groundCheckPoint;
    public LayerMask graoundLayers;
    private bool isGrounded;

    public enum MovementState { idel, wave, walk, jump, sit, stand };

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        studentElements.studentDetailsAndOptions.transform.localPosition = studentElements.studentAvatar.transform.localPosition+offeset;
        ToggleStudentOptions();

        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        veriticalRotStore += mouseInput.y;
        veriticalRotStore = Mathf.Clamp(veriticalRotStore, -60f, 60f);

        if (invertLook)
        {
            studentElements.ViewPoint.rotation = Quaternion.Euler(veriticalRotStore, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        else
        {
            studentElements.ViewPoint.rotation = Quaternion.Euler(-veriticalRotStore, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }


        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        studentElements.studentAvatar.moveDir = moveDir;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeSpeed = runSpeed;
        }
        else
        {
            activeSpeed = moveSpeed;
        }

        float yVelocity = movement.y;

        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeSpeed;
        movement.y = yVelocity;

        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.25f, graoundLayers);
        StudentAvatar avatar = studentElements.studentAvatar;
       

        if (Input.GetButtonDown("Jump") & isGrounded)
        {
            movement.y = jumpForce;
        }

        movement.y += Physics.gravity.y * Time.deltaTime * gravityMode;
        if(!avatar.sit & (int)avatar.state != (int)MovementState.wave)
        {
            charCon.Move(movement * Time.deltaTime);
        }
    }

    public void ToggleStudentOptions()
    {
        studentElements.studentDetailsAndOptions.studentDetailsElementsRef.StudentOptions.SetActive(studentElements.studentAvatar.isClickedOnAvatar);
    }

    private void LateUpdate()
    {
        cam.transform.position = studentElements.ViewPoint.transform.position;
        cam.transform.rotation = studentElements.ViewPoint.transform.rotation;
    }

}
