using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningPopper : MonoBehaviour
{
    public static WarningPopper Instance;

    [SerializeField] private WarningPopup warningPopup;

    public bool isShowing;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowWarning(string message)
    {
        if (isShowing) return;

        isShowing = true;

        Instantiate(warningPopup, transform).SetText(message);
    }
}
