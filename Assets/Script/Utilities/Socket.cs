using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] private string socketName;

    public int[] layerAnimation;

    private HighlightOnTutorial highlighter;

    public string reqTag;

    public List<string> reqlock;

    public bool isSocketReady, keepRealPart, lockedOnMoboPlaced;

    public GameObject placeholderObject, disableObjectOnEnd;

    public Action SocketAssembled;

    private void Start()
    {
        MotherboardEvent.OpenedLock += CheckLock;

        highlighter = GetComponent<HighlightOnTutorial>();
    }

    private void CheckLock(string lockName, int layerAnimation)
    {
        for (int i = reqlock.Count - 1; i >= 0; i--)
        {
            string item = reqlock[i];

            if (item == lockName)
            {
                //print($"deleting {item}");
                reqlock.RemoveAt(i); // Hapus berdasarkan indeks
            }
        }

        //print("accessing");

        //foreach (string item in reqlock)
        //{
        //    print(item);

        //    if (item == lockName)
        //    {
        //        print($"deleting {item}");
        //        reqlock.Remove(item);
        //        continue;
        //    }
        //}

        //print("Kelar");

        isSocketReady = reqlock.Count == 0;

        if (isSocketReady)
        {
            //print($"{socketName} Terbuka");
            TutorialManager.Instance.TryFulfillStep($"{socketName} Terbuka");

            if (highlighter)
            {
                highlighter.enabled = true;
            }

            MotherboardEvent.OpenedLock -= CheckLock;

            //foreach (HighlightOnTutorial item in highlightsToEnable)
            //{
            //    item.enabled = true;
            //}
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform other = collision.transform;

        if (lockedOnMoboPlaced && Motherboard.Instance.HasComponent("Motherboard To Case"))
        {
            WarningPopper.Instance.ShowWarning("Motherboard sudah terpasang di Casing!");
        }

        else if (other.CompareTag(reqTag))
        {
            if (!isSocketReady)
            {
                WarningPopper.Instance.ShowWarning($"Socket Masih Terkunci!");
                return;
            }

            //if (Motherboard.Instance)
            //{
            //    if (Motherboard.Instance.isMotherboadLocked)
            //    {
            //        return;                
            //    }
            //}

            Destroy(GetComponent<Collider>());

            isSocketReady = false;

            if (keepRealPart)
            {
                other.parent = transform;

                other.localPosition = Vector3.zero;

                Destroy(other.GetComponent<ObjectRotator>()); 

                Destroy(other.GetComponent<ObjectDragger>());

                Destroy(other.GetComponent<Rigidbody>());

                other.DOLocalMoveZ(-2, 1).From().SetEase(Ease.OutQuad);
            }
            else
            {
                Destroy(collision.gameObject);            
            }

            if (placeholderObject)
            {
                placeholderObject.SetActive(true);
            }

            SocketAssembled?.Invoke();

            foreach (int layer in layerAnimation)
            {
                MotherboardEvent.CloseLock?.Invoke(name, layer);   
            }

            MotherboardEvent.ComponentPlaced?.Invoke(socketName, 0);

            //if (socketName == "Motherboard")
            //{
            //    Motherboard.Instance.isMotherboadLocked = true;
            //}

            TutorialManager.Instance.TryFulfillStep($"{socketName} Terpasang");

            if (disableObjectOnEnd)
            {
                disableObjectOnEnd.SetActive(false);
            }
            
            AudioManager.Instance.PlaySFX(SFXType.Place_Part);

            ScoreManager.Instance.AddScore(150);
        }
        else if (other.GetComponent<ComputerPart>() && isSocketReady)
        {
            WarningPopper.Instance.ShowWarning("Socket Tidak Tepat!");
        }
    }
}
