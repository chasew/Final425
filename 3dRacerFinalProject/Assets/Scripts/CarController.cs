using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] float acceleration = 8;
    [SerializeField] float turnSpeed = 5;
    Quaternion targetRotation;
    Rigidbody _rigidBody;
    Vector3 lastPoisition;
    float _sideSlipAmount = 0;

    public float sideSlipAmount
    {
        get { 
            return _sideSlipAmount;
        }
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        SetRotationPoint();
        SetSideSlip();
    }

    private void SetSideSlip()
    {
        Vector3 direction = transform.position - lastPoisition;
        Vector3 movement = transform.InverseTransformDirection(direction);
        lastPoisition = transform.position;

        _sideSlipAmount = movement.x;
    }

    private void SetRotationPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        }
    }

    private void FixedUpdate()
    {
        float accelerationInput = acceleration * (Input.GetKey(KeyCode.UpArrow) ? 1 : Input.GetKey(KeyCode.DownArrow) ? -1 : 0) * Time.fixedDeltaTime;
        _rigidBody.AddRelativeForce(Vector3.forward * accelerationInput);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
    }
}


