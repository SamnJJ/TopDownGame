﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _currentMovementSpeed = Vector3.zero;
    private float _currentGravitationalForce;
    private CharacterController _controller = null;
    private Vector3 _orientationF = Vector3.zero;
    private Vector3 _orientationR = Vector3.zero;
   // private PlayerClick _stats = null;

    [SerializeField]
    public Vector3 CamHeight = Vector3.zero;
    [SerializeField]
    public Camera MainCam = null;
    [SerializeField]
    private int _moveSpeed;
    [SerializeField]
    private Animator _anim = null;

    // Use this for initialization
    void Start()
    {
        _orientationF = transform.forward;
        _orientationR = transform.right;
        _controller = GetComponent<CharacterController>();
        //_stats = GetComponent<PlayerClick>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SetRotation();
    }

    private void SetRotation()
    {
        //Vector3 upAxis = new Vector3(0, 1, 0);
        //Vector3 mouseScreenPosition = Input.mousePosition;
        //mouseScreenPosition.z = transform.position.z;
        //Vector3 mouseWorldSpace = MainCam.ScreenToWorldPoint(mouseScreenPosition);


        RaycastHit hit;
        Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 0;
        layerMask = ~layerMask;

        if (Physics.Raycast(ray, out hit, 10000.0f, layerMask))
        {
            var tag = hit.transform.gameObject.tag;
            if (tag == "RayFloor")
            {
                Vector3 upAxis = new Vector3(0, 1, 0);
                Vector3 pos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                transform.LookAt(pos, upAxis);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
        }
    }


    private void Movement()
    {
        float inputHor = Input.GetAxis("Horizontal");
        float inputVer = Input.GetAxis("Vertical");
        if (inputHor != 0 || inputVer != 0)
        {
            _anim.SetBool("IsMoving", true);
        }
        else
        {
            _anim.SetBool("IsMoving", false);
        }

        Vector3 inputMove = (inputHor * _orientationR + inputVer * _orientationF).normalized;
        _currentMovementSpeed = inputMove;

        _controller.Move(_currentMovementSpeed * Time.deltaTime * _moveSpeed);
        MainCam.transform.position = transform.position + CamHeight;        
    }


}