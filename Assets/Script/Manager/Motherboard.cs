using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motherboard : QuestComponent
{
    public static Motherboard Instance;

    public bool isMotherboadLocked;

    private Animator animator;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        MotherboardEvent.OpenLock += OnOpenLock;
        MotherboardEvent.CloseLock += OnCloseLock;
        MotherboardEvent.ComponentPlaced += OnComponentPlaced;

        componentsPlaced.Add("Motherboard");
        UpdateQuest?.Invoke();
    }

    private void OnComponentPlaced(string componentName, int count)
    {
        componentsPlaced.Add(componentName);
        UpdateQuest?.Invoke();
    }

    public void OnOpenLock(string trigger, int layer)
    {
        animator.SetTrigger($"{trigger}_OPEN");
    }


    private void OnCloseLock(string trigger, int layer)
    {
        animator.SetTrigger($"{trigger}_PLACED");
    }

    public void OnEndOpenLock(string lockName)
    {
        MotherboardEvent.OpenedLock?.Invoke(lockName, 0);
    }

    private void OnDestroy()
    {
        MotherboardEvent.OpenLock -= OnOpenLock;
        MotherboardEvent.CloseLock -= OnCloseLock;
        MotherboardEvent.ComponentPlaced -= OnComponentPlaced;
    }
}

public static class MotherboardEvent
{
    public static Action<string, int> OpenLock, CloseLock, OpenedLock, ComponentPlaced;
}
