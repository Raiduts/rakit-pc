using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsManager : MonoBehaviour
{
    [SerializeField] private ComputerPart[] computerParts;
    private ComputerPart currentPart;
    private int partIndex;

    private void Start()
    {
        SelectorManager.Instance.ChangeObject += ChangePart;

        SummonPart(computerParts[0]);
    }

    private void ChangePart(int value)
    {
        Destroy(currentPart.gameObject);

        partIndex += value;

        if (partIndex >= computerParts.Length)
        {
            partIndex = 0;
        }
        else if(partIndex < 0 )
        {
            partIndex = computerParts.Length - 1;
        }

        SummonPart(computerParts[partIndex]);
    }

    private void SummonPart(ComputerPart partPref)
    {
        currentPart = Instantiate(partPref);
    }
}
