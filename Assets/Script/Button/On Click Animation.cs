using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickAnimation : MonoBehaviour
{
    [SerializeField] private OnClickAnimationType animationType;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        switch (animationType)
        {
            case OnClickAnimationType.Bounce:
                // Bounce
                break;
        }
    }
}

public enum OnClickAnimationType
{
    Bounce
}
