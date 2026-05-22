using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockButtonByReq : MonoBehaviour
{
    public string reqString;

    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();

        bool isUnlocked = PlayerPrefs.GetInt(reqString) == 1;

        btn.interactable = isUnlocked;
    }
}
