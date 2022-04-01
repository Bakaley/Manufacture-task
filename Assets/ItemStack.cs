using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ItemStack : MonoBehaviour
{
    //������� �����, ������� ����� ��������� � ����������
    //��������������, ��� ������� ����� �� ��������� � ������� ����
    [SerializeField]
    protected BoxCollider sizeCellSampler;

    //����� � ����� ����
    [SerializeField]
    protected int rowSize = 5;
    //����� � ������ ����
    [SerializeField]
    protected int rowsCount = 2;
    //�������� ���� (� ������)
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

    //��������� ����� � ��������� � ������� �� ����� 
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
