using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ResourceReceiver
{
    public bool canReceiveResource(System.Type type);

    public void receive(Resource resource);
}
