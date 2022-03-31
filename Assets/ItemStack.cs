using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemStack : MonoBehaviour
{
    //размеры бочек, которые будут хранитьс€ в контейнере
    //предполагаетс€, что размеры бочки не измен€тс€ в течении игры
    [SerializeField]
    protected Rigidbody sizeCellSampler;

    //бочек в одном р€ду
    [SerializeField]
    protected int rowSize = 5;
    //р€дов в одному слое
    [SerializeField]
    protected int rowsCount = 2;
    //максимум слоЄв (в высоту)
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

    //добавл€ет бочку в коллекцию и находит ей место 
    public abstract void add(Resource resource);
    

    //убирает еЄ из коллекции
    protected abstract void remove(Resource resource);


}
