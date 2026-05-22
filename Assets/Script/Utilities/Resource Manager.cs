using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public Canvas mainCanvas;
    public Material hintMaterial;
    public AudioClip bgm;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AudioManager.Instance.PlayBGM(bgm, 0.5f);
    }

    private void Update()
    {
        if (hintMaterial)
        {
            ChangeMaterialOpacity();
        }
    }

    private void ChangeMaterialOpacity()
    {
        Color color = hintMaterial.color;

        color.a = Mathf.PingPong(Time.time * 1, 0.75f);

        hintMaterial.color = color;
    }
}
