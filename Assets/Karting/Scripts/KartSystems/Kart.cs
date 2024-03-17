using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{
    [SerializeField] private Rigidbody sphereRb;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private LayerMask groundLayerMask;
    private float _forwardAmount;
    private float _currentSpeed;
    private float _turnAmount;


    private void Start()
    {
        sphereRb.transform.parent = null;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = sphereRb.transform.position;

        _forwardAmount = Input.GetAxis("Vertical");
        _turnAmount = Input.GetAxis("Horizontal");

        if(_forwardAmount != 0)
            Drive();
        else
            DriveNowhere();

        TurnHandler();
        GroundNormalHandler();
    }

    private void GroundNormalHandler()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit, 1, groundLayerMask);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation, 0.1f);
    }

    private void TurnHandler()
    {
        float newRotation = _turnAmount * turnSpeed * Time.deltaTime;
        transform.Rotate(0, newRotation, 0, Space.World);
    }

    private void DriveNowhere()
    {
        _currentSpeed = 0;
    }

    private void DriveReverse()
    {
        _currentSpeed = _forwardAmount *= forwardSpeed;
    }

    private void FixedUpdate()
    {
        sphereRb.AddForce(transform.forward * _currentSpeed, ForceMode.Acceleration);
    }

    private void Drive()
    {
        _currentSpeed = _forwardAmount *= forwardSpeed;
    }
}
