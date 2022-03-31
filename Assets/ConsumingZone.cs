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
        //игрок у нас один, поэтому достаточно его иницализировать единожды
        if (!player)
        {
            player = other.GetComponent<Player>();
            backpackStack = player.GetComponentInChildren<BackpackStack>();
        }
    }

    //по-хорошему надо хранить ссылку на корутину, но у корутины нельзя узнать работает ли она (ну или я не нашёл как)
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

    //на всякий случай
    private void OnCollisionExit(Collision collision)
    {
        StopAllCoroutines();
        CR_running = false;
    }


    //подогнать размер триггерной зоны под размеры области (только для эдитора)
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
