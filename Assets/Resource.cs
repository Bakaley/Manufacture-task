using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{

    new Rigidbody rigidbody;
    Vector3 desiredVelocity;
    float speed;


    bool moving = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Transform target;
    Vector3 offset;
    public void moveTo (Transform targetParent, Vector3 localOffset)
    {
        target = targetParent;
        this.offset = localOffset;
        speed = 12;
        moving = true;

    }

    private void FixedUpdate()
    {

        if (moving)
        {
            Vector3 distance = target.position + target.TransformVector(offset) - transform.position;
            desiredVelocity = rigidbody.position + (target.position + target.TransformVector(offset) - transform.position) * Time.fixedDeltaTime * speed;
            rigidbody.MovePosition(desiredVelocity);
            if (distance.magnitude <= 1)
            {
                stop();
            }

        }
    }

    void stop()
    {
        moving = false;
        //rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transform.parent = target;
        transform.localPosition = offset;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
