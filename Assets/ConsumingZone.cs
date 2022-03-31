using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumingZone : MonoBehaviour
{
    FactoryStack factoryStack;
    BoxCollider zoneCollider;
    [SerializeField]
    Resource[] resourceConsumingSamplers;

    System.Type[] resourceTypes;

    Player player;
    BackpackStack backpackStack;

    private void Start()
    {
        resourceTypes = new System.Type[resourceConsumingSamplers.Length];
        for (int i = 0; i < resourceTypes.Length; i++)
        {
            resourceTypes[i] = resourceConsumingSamplers[i].GetType();
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        //����� � ��� ����, ������� ���������� ��� ��������������� ��������
        if (!player)
        {
            player = other.GetComponent<Player>();
            backpackStack = player.GetComponentInChildren<BackpackStack>();
        }
    }

    //��-�������� ���� ������� ������ �� ��������, �� � �������� ������ ������ �������� �� ��� (�� ��� � �� ����� ���)
    bool CR_running;
    private void OnTriggerStay(Collider other)
    {
        if (player.Staying)
        {
            if (!CR_running)
            {
                StartCoroutine(startConsuming(backpackStack, resourceTypes).GetEnumerator());
                CR_running = true;
            }
        }
        else
        {
            StopAllCoroutines();
            CR_running = false;
        }
    }

    //�� ������ ������
    private void OnCollisionExit(Collision collision)
    {
        StopAllCoroutines();
        CR_running = false;
    }


    //��������� ������ ���������� ���� ��� ������� ������� (������ ��� �������)
    public void resizeZoneOfSprite()
    {
        GetComponent<BoxCollider>().size = new Vector3(GetComponent<SpriteRenderer>().size.x + 1, GetComponent<SpriteRenderer>().size.y + 1, GetComponent<BoxCollider>().size.z);
    }

    Resource res;

    IEnumerable startConsuming(BackpackStack stack, System.Type[] resourceTypes)
    {
        while (true)
        {
            foreach (System.Type type in resourceTypes)
            {
                //Debug.Log(stack.Pop(type));
                res = stack.Pop(type);
                if(res) res.moveTo(transform, Vector3.zero);
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
