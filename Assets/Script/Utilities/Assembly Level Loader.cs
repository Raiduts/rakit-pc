using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssemblyLevelLoader : MonoBehaviour
{
    private Button button;

    [SerializeField] LevelData levelData;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void SelectLevel()
    {
        Instantiate(levelData);
    }
}
