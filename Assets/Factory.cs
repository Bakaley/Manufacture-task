using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField]
    Resource resourceSample;

    [SerializeField]
    FactoryStack[] inputZones;

    //System.Type[] typesToConsume;

    [SerializeField]
    FactoryStack outputZone;

    private void Start()
    {


        StartCoroutine(manufacturing().GetEnumerator());
    }

    public bool tryConsume ()
    {
        foreach (FactoryStack zone in inputZones)
        {
            if (zone.isEmpty)
            {
                return false;
            }
        }
        foreach (FactoryStack zone in inputZones)
        {
            {
                Resource res = zone.pop();
                res.moveTo(transform, Vector3.zero);
                Destroy(res.gameObject, 1);
            }
        }
        return true;
    }

  

    //фабрика пытается съесть по одному ресурсу из каждой зоны, которая к фабрике привязана
    IEnumerable manufacturing()
    {
        while (true)
        {
            if (outputZone.Full)
            {
                yield return new WaitForSeconds(1);
            }
            if (!outputZone.Full && tryConsume())
            {
                GameObject res = Instantiate(resourceSample.gameObject, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
                outputZone.receive(res.GetComponent<Resource>());
            }
            yield return new WaitForSeconds(1);
        }
      
    }
}
