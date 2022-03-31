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


    //���������� ������ �����, ���� ���� ��������� �����
    //
    int getEmptyCell()
    {
        return 0;
    }

    //������ ��� ������������ ������ ��������, �� ��� ������� ����� ����������� � ������������ � ����
    public void spriteResize()
    {
        //����� ����� �����������, ������� ����� ����� X � Z
        GetComponent<SpriteRenderer>().size = new Vector2(rowSize * sizeCellSampler.GetComponent<BoxCollider>().size.x, rowsCount * sizeCellSampler.GetComponent<BoxCollider>().size.z);
    }




  


}
