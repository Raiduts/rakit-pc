using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketLock : MonoBehaviour
{
    //[SerializeField] private Animator animator;

    //[SerializeField] private string triggerName; 
    [SerializeField] private int layerAnimation;
    [SerializeField] private string reqCompleted;

    private bool canInteract = true;

    //public Action OpenLock;

    private void OnMouseDown()
    {
        if (!canInteract || Motherboard.Instance.HasComponent("Motherboard To Case")) return;

        AudioManager.Instance.PlaySFX(SFXType.Lock);

        canInteract = false;

        MotherboardEvent.OpenLock?.Invoke(name, layerAnimation);

        //animator.SetTrigger("Open_CPU");

        //OpenLock?.Invoke();
    }
}
