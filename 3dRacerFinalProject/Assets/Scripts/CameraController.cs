using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform observeable;
    [SerializeField] float aheadSpeed;
    [SerializeField] float followDamping;
    [SerializeField] float cameraHeight;

    Rigidbody _observableRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _observableRigidBody = observeable.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_observableRigidBody == null)
        {
            return;
        }

        Vector3 targetPosition = observeable.position + Vector3.up * cameraHeight + _observableRigidBody.velocity * aheadSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDamping * Time.deltaTime);
    }
}
