using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryStack : ItemStack
{
    protected override void Awake()
    {
        collection = new List<Resource>();
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void add(Resource resource)
    {
    }

    protected override void remove(Resource resource)
    {
    }


    //возвращает пустое место, куда надо поставить бочку
    //
    int getEmptyCell()
    {
        return 0;
    }

    //вообще это используется кастом эдитором, но при желании можно отшлифовать и использовать в игре
    public void spriteResize()
    {
        //бочки стоят вертикально, поэтому нужно брать X и Z
        GetComponent<SpriteRenderer>().size = new Vector2(rowSize * sizeCellSampler.GetComponent<BoxCollider>().size.x, rowsCount * sizeCellSampler.GetComponent<BoxCollider>().size.z);
    }




  


}
