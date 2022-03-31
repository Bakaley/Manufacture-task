using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class BackpackStack : ItemStack
{

    protected override void Awake()
    {
        collection = new StackList<Resource>();
        base.Awake();
    }


    protected override void Start()
    {
        base.Start();
        ((StackList<Resource>)collection).OnResourcePopped += clamp;
    }


    public override void add(Resource resource)
    {
        ((StackList<Resource>)collection).Push(resource);
        resource.gameObject.layer = 0;
        Vector3 newPos = getLocalPositionByIndex(collection.Count - 1);
        resource.moveTo(transform, newPos);

    }

    Vector3 getLocalPositionByIndex(int index)
    {
        //z - номер ряда
        float z = (index / rowSize) * cellSize.z + cellSize.z / 2;
        //x - номер в ряду
        float x = (index % rowSize) * cellSize.x + cellSize.x / 2;
        return new Vector3(x, cellSize.y / 2, z);
    }

    //убираем пробелы после вытаскивания
    void clamp(object sender, EventArgs args)
    {
        foreach (Resource resource in collection)
        {
            resource.transform.localPosition = getLocalPositionByIndex(collection.IndexOf(resource));
        }
    }

    //вытаскиваем из стека первый встреченный элемент нужного типа
    public Resource Pop(System.Type res)
    {
        return ((StackList<Resource>)collection).Pop(res);
    }

    protected override void remove(Resource resource)
    {
        ((StackList<Resource>)collection).Remove(resource);
    }

    private class StackList<T> : List<T> where T : Resource
    {
        public void Push(T item)
        {
            Insert(Count, item);
        }
        public Resource Pop(System.Type resourceType)
        {
            foreach (Resource item in this)
            {
                if(item.GetType() == resourceType)
                {
                    Remove(item);
                    return item;
                }
            }
            return null;
        }
        public event EventHandler OnResourcePopped;
        public void Remove (Resource item)
        {
            base.Remove((T)item);
            RemoveAll(item => item == null);
            OnResourcePopped?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
