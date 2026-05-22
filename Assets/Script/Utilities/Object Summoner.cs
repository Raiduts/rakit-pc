using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSummoner : MonoBehaviour
{
    public static ObjectSummoner Instance;

    public ComputerPart placeholderPart;

    private ComputerPart currentPart;

    [SerializeField] private Transform summonPos;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Testing();
    }

    private void Testing()
    {
        if (placeholderPart)
        {
            SummonObject(placeholderPart, false, false);
        }
    }

    public void CallCamera()
    {
        CinemachineController.Instance.ChangeTarget(transform.GetChild(0));
    }

    public void SummonObject(ComputerPart part, bool keepPrev = false, bool multiply = true)
    {
        float multiplyer = multiply ? part.multiplyer : 1;        

        if (!keepPrev && currentPart)
        {
            Destroy(currentPart.gameObject);
        }
        
        currentPart = Instantiate(part, summonPos);

        currentPart.transform.localScale = new(multiplyer, multiplyer, multiplyer);

        currentPart.PopAnimate();
    }

    public ComputerPart SummonObject(ComputerPart part, Transform location)
    {
        currentPart = Instantiate(part, location ? location : summonPos);

        currentPart.transform.position += part.spawnOffset;

        currentPart.PopAnimate();

        return currentPart;
    }
}
