using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssemblyPartButton : MonoBehaviour
{
    [HideInInspector]
    public ComputerPart computerPartPref;

    [SerializeField] private TextMeshProUGUI buttonText;

    public Action<ComputerPart> SpawnObject;

    public void PrepareButton()
    {
        buttonText.text = computerPartPref.partName;
    }

    public void LockButton()
    {
        GetComponent<Button>().interactable = false;
    }

    public void UnlockButton()
    {
        GetComponent<Button>().interactable = true;
    }

    public void OnClickButton()
    {
        SpawnObject?.Invoke(computerPartPref);

        // Tutorial Related
        TutorialManager.Instance.TryFulfillStep($"Click {computerPartPref.partName} Button");
    }
}
