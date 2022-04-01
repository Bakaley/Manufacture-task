using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FactoryStack : ItemStack, ResourceReceiver
{
    //может ли игрок подцепить из этого стака ресурс
    [SerializeField]
    bool absorbableByPlayer;


    [SerializeField]
    GameObject alertIcon;

    [SerializeField]
    TextMeshPro counter;
    protected override void Awake()
    {
        collection = new NullList<Resource>();
        base.Awake();
    }

    protected override void Start()
    {
        counter.text = collection.Count + "/" + maxSize;
    }

    protected  override void add(Resource resource)
    {
        int index = getEmptyCellIndex();
        collection[index] = resource;
        Vector3 newPos = getLocalPositionByIndex(index);
        resource.moveTo(transform, newPos);
        counter.text = collection.Count + "/" + maxSize;
        if (Full && absorbableByPlayer) alertIcon.SetActive(true);
    }

    Vector3 getLocalPositionByIndex(int index)
    {
        //z - номер ряда
        float z = (index / rowSize) * cellSize.z + cellSize.z / 2;
        //x - номер в ряду
        float x = (index % rowSize) * cellSize.x + cellSize.x / 2;
        return new Vector3(x, cellSize.y / 2, z);
    }

    int getEmptyCellIndex()
    {
        int index = 0;
        foreach(Resource res in collection)
        {
            if (res == null) return index;
            index++;
        }
        collection.Insert(index, null);
        return index;
    }

    public Resource pop()
    {
        Resource res = null;
        for(int i = ((NullList<Resource>)collection).CountWithNulls - 1; i >= 0; i--)
        {
            if (collection[i] != null) res = collection[i];
        }
        collection.Remove(res);

        counter.text = collection.Count + "/" + maxSize;
        if (!Full && absorbableByPlayer) alertIcon.SetActive(false);

        return res;
    }
    
    //вызывается, когда игрок цепляет ресурс
    public void remove (Resource resource)
    {
        collection.Remove(resource);
        counter.text = collection.Count +  "/" + maxSize;
        if (!Full && absorbableByPlayer) alertIcon.SetActive(false);

    }

    public bool isEmpty { get => collection.Count == 0; }
    //возвращает пустое место, куда надо поставить бочку
    //


    //вообще это используется кастом эдитором, но при желании можно отшлифовать и использовать в игре
    public void spriteResize()
    {
        //бочки стоят вертикально, поэтому нужно брать X и Z
        SpriteRenderer sp = GetComponentInChildren<SpriteRenderer>();
        sp.transform.localScale = new Vector3(rowSize * sizeCellSampler.GetComponent<BoxCollider>().size.x/10, rowsCount * sizeCellSampler.GetComponent<BoxCollider>().size.z / 10, 1);
        sp.transform.localRotation = Quaternion.Euler(90, 0, 0);
        sp.transform.localPosition = new Vector3(2.5f, 2.25f, .5f);
    }

    public bool canReceiveResource(Type type)
    {
        return !Full;
    }

    public void receive(Resource resource)
    {
        add(resource);
    }


    private class NullList<T> : CustomStackCollection<T> where T : Resource
    {
        public override void Remove(Resource res)
        {
            int index = -1;
            foreach(Resource resource in list)
            {
                index ++;
                if (res == resource) break;
            }
            list[index] = null;
        }

        public override int Count
        {
            get
            {
                int c = 0;
                foreach (Resource resource in list)
                {
                    if (resource != null) c++;
                }
                return c;
            }
        }

        public int CountWithNulls
        {
            get
            {
                return list.Count;
            }
        }

    }
}
