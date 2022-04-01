using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ItemStack : MonoBehaviour
{
    //размеры бочек, которые будут храниться в контейнере
    //предполагается, что размеры бочки не изменятся в течении игры
    [SerializeField]
    protected BoxCollider sizeCellSampler;

    //бочек в одном ряду
    [SerializeField]
    protected int rowSize = 5;
    //рядов в одному слое
    [SerializeField]
    protected int rowsCount = 2;
    //максимум слоёв (в высоту)
    [SerializeField]
    protected int layers = 1;

    protected Vector3 cellSize;

    protected int maxSize
    {
        get; private set;
    }

    protected CustomStackCollection<Resource> collection;


    protected virtual void Awake()
    {
        maxSize = rowSize * rowsCount * layers;
        cellSize = sizeCellSampler.GetComponent<BoxCollider>().size;
        
    }

    protected virtual void Start()
    {

    }

    public bool Full
    {
        get
        {
            return collection.Count == maxSize;
        }
    }

    //добавляет бочку в коллекцию и находит ей место 
    protected abstract void add(Resource resource);
    
    protected abstract class CustomStackCollection<T> where T : Resource
    {
        protected List<Resource> list = new List<Resource>();

        public abstract int Count { get; }

        public abstract void Remove(Resource res);

        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public int IndexOf(Resource res)
        {
            return list.IndexOf(res);
        }

        public void Insert(int index, Resource res)
        {
            list.Insert(index, res);
        }


        
        public Resource this [int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void RemoveAll(Predicate<Resource> predicate)
        {
            list.RemoveAll(predicate);
        }

    }

}
