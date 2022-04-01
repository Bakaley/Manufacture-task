using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashZone : MonoBehaviour, ResourceReceiver
{
    public bool canReceiveResource(Type type)
    {
        return true;
    }

    public void receive(Resource resource)
    {
        Debug.Log("rec");
        resource.moveTo(transform, Vector3.zero);
        Destroy(resource.gameObject, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
