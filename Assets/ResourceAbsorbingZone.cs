using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceAbsorbingZone : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 7)
        {
            OnResourceCollision?.Invoke(this, other.GetComponent<Resource>());
        }
    }

    public event EventHandler<Resource> OnResourceCollision;

}
