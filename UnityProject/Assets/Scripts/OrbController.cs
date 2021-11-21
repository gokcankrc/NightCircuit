



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{

    // [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private CharacterController controller;
    [SerializeField] public Transform cameraTransform;
    [Range(0, 20f)]
    [SerializeField] private float moveSpeed = 8f;
    [Range(45, 450f)]
    [SerializeField] private float sensitivity = 1.3f;
    
    [SerializeField] private float initialDelay;

    public Vector3 angleV3;
    
    private Quaternion _initialLook;




    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // to disable the first frame snap
        _initialLook = cameraTransform.rotation;
    }

    void Update()
    {
        
        if (initialDelay > 0)
        {
            initialDelay -= Time.deltaTime;
            return;
        }
        
        angleV3 += new Vector3(-Input.GetAxis("Mouse Y") * sensitivity, Input.GetAxis("Mouse X") * sensitivity, 0) * Time.deltaTime;
        angleV3.x = Mathf.Clamp(angleV3.x, -90, 90);

        OrbSight();
        Move();
        Fall();


    }

    void Move()
    {
        var moveVector = cameraTransform.forward * Input.GetAxisRaw("Vertical") + cameraTransform.right * Input.GetAxisRaw("Horizontal");
        if (moveVector.magnitude < 0.05) return;
        moveVector.y = 0;
        controller.Move(moveVector.normalized * moveSpeed * Time.deltaTime);
        
    }

    void Fall()
    {
        controller.Move(Vector3.down * 0.03f);
    }

    void OrbSight()
    {
        cameraTransform.rotation = _initialLook;
        cameraTransform.Rotate(angleV3);
    }
}

