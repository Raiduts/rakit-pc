using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestComponent : MonoBehaviour 
{
    public Action UpdateQuest;

    public List<string> componentsPlaced;

    public void AddComponent(string name)
    {
        componentsPlaced.Add(name);
        UpdateQuest?.Invoke();
    }

    public void RemoveComponent(string name)
    {
        componentsPlaced.Remove(name);
        UpdateQuest?.Invoke();
    }

    public bool HasComponent(string name)
    {
        return componentsPlaced.Contains(name);
    }

    public int ComponentCount(string name)
    {
        int count = 0;

        foreach (string component in componentsPlaced)
        {
            if (component.Equals(name))
            {
                count++;
            }
        }

        TutorialManager.Instance.TryFulfillStep($"{count} {name} Terpasang");

        return count;
    }
}
