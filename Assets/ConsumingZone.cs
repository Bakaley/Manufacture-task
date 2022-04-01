using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumingZone : MonoBehaviour
{
    [SerializeField]
    Resource[] resourceReceivingSamplers;

    System.Type[] typesToConsume;

    FactoryStack factoryStack;

    [SerializeField]
    GameObject receiverObj;

    ResourceReceiver receiver = null;

    public System.Type ResourceType
    {
        get; private set;
    }


    Player player;
    BackpackStack backpackStack;

    private void Start()
    {
        factoryStack = GetComponent<FactoryStack>();
        if(receiverObj) receiver = receiverObj.GetComponent<ResourceReceiver>();
        typesToConsume = new System.Type[resourceReceivingSamplers.Length];
        for (int i = 0; i < resourceReceivingSamplers.Length; i++)
        {
            typesToConsume[i] = resourceReceivingSamplers[i].GetType();
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
                StartCoroutine(startConsuming(backpackStack, typesToConsume).GetEnumerator());
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
        GetComponent<BoxCollider>().size = new Vector3(GetComponentInChildren<SpriteRenderer>().size.x + 1, GetComponentInChildren<SpriteRenderer>().size.y + 1, GetComponent<BoxCollider>().size.z);
    }

    Resource res;

    IEnumerable startConsuming(BackpackStack stack, System.Type[] resourceType)
    {
        while (true)
        {
            foreach (System.Type type in resourceType)
            {
                if (receiver != null && receiver.canReceiveResource(type))
                {
                    res = stack.Pop(type);
                    if (res) receiver.receive(res);
                    yield return new WaitForSeconds(.1f);
                }
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
