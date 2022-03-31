using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemStack : MonoBehaviour
{
    //������� �����, ������� ����� ��������� � ����������
    //��������������, ��� ������� ����� �� ��������� � ������� ����
    [SerializeField]
    protected Rigidbody sizeCellSampler;

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

    int maxSize;

    protected List<Resource> collection;


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
    public abstract void add(Resource resource);
    

    //������� � �� ���������
    protected abstract void remove(Resource resource);


}
