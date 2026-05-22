using System.IO;
using UnityEngine;

public class RTToTexture2D : MonoBehaviour
{
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private string fileName;

    //private void Start()
    //{
    //    SavePNG();
    //}

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SavePNG();
        }
    }

    [ContextMenu("Save PNG")]
    public void SavePNG()
    {
        print("Converting...");

        // Backup current active RT
        RenderTexture currentRT = RenderTexture.active;

        // Set target RT active
        RenderTexture.active = renderTexture;

        // Create Texture2D
        Texture2D texture = new Texture2D(
            renderTexture.width,
            renderTexture.height,
            TextureFormat.RGBA32,
            false   
        );

        // Copy pixels
        texture.ReadPixels(
            new Rect(0, 0, renderTexture.width, renderTexture.height),
            0,
            0
        );

        texture.Apply();

        // Restore previous RT
        RenderTexture.active = currentRT;

        // Encode to PNG
        byte[] pngData = texture.EncodeToPNG();

        // Save path
        string path = Path.Combine($"{Application.dataPath}/Sprite/Tumbnails", $"Tumbnails_{fileName}.png");

        // Write file
        File.WriteAllBytes(path, pngData);

        Debug.Log("PNG Saved To: " + path);

        // Cleanup
        DestroyImmediate(texture);
    }
}