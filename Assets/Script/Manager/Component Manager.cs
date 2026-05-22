using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    [SerializeField] private PartTab partTabPref;
    [SerializeField] private Transform container;
    [SerializeField] private ShowcaseData[] showcaseDatas;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateComponentsTab();
    }

    private void InstantiateComponentsTab()
    {
        foreach (ShowcaseData item in showcaseDatas)
        {
            Instantiate(partTabPref, container).SetPartTab(item);
        }
    }

    public void CloseComponentTab()
    {
        Destroy(gameObject);
    }
}

[System.Serializable]
public class ComponentTabData
{
    public ComputerPart computerPartPref;
}
