using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinZone : MonoBehaviour, ResourceReceiver
{
    [SerializeField]
    int winRequirment = 10;
    [SerializeField]
    GameObject winText;
    [SerializeField]
    TextMeshPro blueText;
    [SerializeField]
    TextMeshPro greenText;
    [SerializeField]
    TextMeshPro redText;

    Dictionary<System.Type, int> winCon = new Dictionary<System.Type, int>();
    Dictionary<System.Type, TextMeshPro> textDict = new Dictionary<System.Type, TextMeshPro>();

    private void Start()
    {
        winCon.Add(typeof(RedResource), winRequirment);
        winCon.Add(typeof(GreenResource), winRequirment);
        winCon.Add(typeof(BlueResource), winRequirment);

        textDict.Add(typeof(RedResource), redText);
        textDict.Add(typeof(GreenResource), greenText);
        textDict.Add(typeof(BlueResource), blueText);
    }

    bool checkWin()
    {
        foreach (int key in winCon.Values)
        {
            if (key != 0) return false;
        }
        return true;
    }

    public bool canReceiveResource(System.Type type)
    {
        return winCon[type] > 0;
    }

    public void receive(Resource resource)
    {
        resource.moveTo(transform, Vector3.zero);
        Destroy(resource.gameObject, 1);
        winCon[resource.GetType()]--;
        textDict[resource.GetType()].text = winRequirment - winCon[resource.GetType()] + "/" + winRequirment;
        if (checkWin())
        {
            winText.SetActive(true);
        }
    }


}
