using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainCharacterController : MonoBehaviour
{
    public Joystick Joystick;
    public FixedJoystickButton JoystickButton;
    public float GravityForce;
    public float JumpForce;
    public float JumpDistance;
    public float JoystickSensitivity;
    public float DeathDistance;
    public Camera Camera;
    public bool StopMoving;

    private Animator _animator;
    private bool _isGrounded;
    private CharacterController _controller;
    private bool _isOnPlatform;
    private Vector3 _moveDirection;
    private Transform _platform;

    private void Awake()
    {
        if (Joystick == null || JoystickButton == null)
            Debug.LogError("select joystick and joystick buttons!");

        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    public void SetLastPosition()
    {
        transform.position = CheckpointManager.instance.GetLastCheckpointPosition();
    }
    
    void SetAnim()
    {
        _animator.SetBool("IsGrounded", CheckGrounded());
        _animator.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical") + Joystick.Vertical) + Mathf.Abs(Input.GetAxis("Horizontal") + Joystick.Horizontal)));
    }

    /*void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.LogError(hit.collider.name);
    }*/

    void Move()
    {
        //use camera direction to calculate right position of axes to the player
        Vector3 camForward = Camera.transform.forward;
        Vector3 camRight = Camera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;

        if (CheckGrounded())
        {
            Vector3 horDirection = (Joystick.Horizontal + Input.GetAxis("Horizontal")) * camRight;
            Vector3 vertDirection = (Joystick.Vertical + Input.GetAxis("Vertical")) * camForward;
            //player movement according to camera rotation
            _moveDirection = horDirection * 1f + vertDirection * 1f;

            //this is needed to stop player of rotation to default state when idle
            if (horDirection.x != 0f || vertDirection.z != 0f)
            {
                Vector3 lookPos = _moveDirection;
                lookPos.y = 0;

                // rotate player model according to new movement vector
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, JoystickSensitivity);
            }

            if ((JoystickButton.IsPressed() || Input.GetButton("Jump")) && CheckGrounded() )
            {
                _moveDirection = Vector3.zero;
                _moveDirection.y = JumpForce;
                float velocity = Mathf.Abs(Input.GetAxis("Vertical") + Joystick.Vertical) + Mathf.Abs(Input.GetAxis("Horizontal") + Joystick.Horizontal);
                if (velocity > 0)
                    _moveDirection.z = JumpDistance;
                _moveDirection = transform.TransformDirection(_moveDirection);
                _moveDirection.y = Math.Abs(_moveDirection.y);
            }

        }

        _moveDirection.y -= GravityForce * Time.deltaTime;

        if (StopMoving)
        {
            //_moveDirection = Vector3.zero;
            return;
        }
            
        _controller.Move(_moveDirection * Time.deltaTime * JoystickSensitivity);
    }

    public void YouAreOnPlatform(bool yesorno)
    {
        _isOnPlatform = yesorno;
    }

    private bool CheckGrounded()
    {
        // make it very small for better performance
        float floorDistanceFromFoot = _controller.stepOffset * 0.05f;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, floorDistanceFromFoot) || _controller.isGrounded)
        {
            //Debug.DrawRay(transform.position, Vector3.down * floorDistanceFromFoot, Color.yellow);
            return true;
        }
        return false;
    }

    void Update()
    {
        Move();

        SetAnim();

        CheckDeath();
    }

    void CheckDeath()
    {
        if (_isOnPlatform)
            return;
        if (transform.localPosition.y < DeathDistance && !CheckGrounded())
        {
            transform.position = CheckpointManager.instance.GetLastCheckpointPosition();
        }
    }
}
