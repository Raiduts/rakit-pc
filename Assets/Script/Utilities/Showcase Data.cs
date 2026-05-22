using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShowcaseData
{
    [Header("Data")]
    public string partName, partSeries;
    [TextArea] public string overview;
    public List<string> facts, functions;
    public Sprite partSprite;

    [Header("Optional")]
    public ComputerPart part3D;
}
