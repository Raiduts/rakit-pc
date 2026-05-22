using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HighlightOnTutorial : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Material highlightMaterial;
    [SerializeField] private string highlightStep;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        highlightMaterial = ResourceManager.Instance.hintMaterial;

        if (TutorialManager.Instance)
        {
            TutorialManager.Instance.OnChangeStep += OnChangeStep;        
    
            OnChangeStep(TutorialManager.Instance.GetCurrentTutorialStep());    
        }

    }

    private void OnChangeStep(TutorialData data)
    {
        //print("Check Quest For Material");

        if (data == null) return;

        List<Material> mats = new List<Material>(meshRenderer.materials);

        if (data.tutorialName == highlightStep)
        {
            // ADD kalau belum ada
            //print("Add Material");
            if (!mats.Contains(highlightMaterial))
            {
                mats.Add(highlightMaterial);
            }
        }
        else
        {
            // REMOVE
            print("Remove Material");
            //if (mats.Count > 1)

            for (int i = mats.Count - 1; i > 0; i--)
            {
                mats.RemoveAt(i);                
            }
        }

        meshRenderer.materials = mats.ToArray();
    }

    public void ForceRemove()
    {
        if (TutorialManager.Instance)
        {
            TutorialManager.Instance.OnChangeStep -= OnChangeStep;
        }

        //OnChangeStep();

        List<Material> mats = new List<Material>(meshRenderer.materials);

        for (int i = mats.Count - 1; i > 0; i--)
        {
            mats.RemoveAt(i);
        }

        meshRenderer.materials = mats.ToArray();
        //enabled = false;
    }

    private void OnDestroy()
    {
        if (TutorialManager.Instance)
        {
            TutorialManager.Instance.OnChangeStep -= OnChangeStep;
        }
    }
}
