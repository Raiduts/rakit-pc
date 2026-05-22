using UnityEngine;

public class ViewerAutoCenter : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Camera viewerCamera;
    [SerializeField] private float distance = 5f;

    private void Start()
    {
        Renderer[] renderers = target.GetComponentsInChildren<Renderer>();

        Bounds bounds = renderers[0].bounds;

        foreach (Renderer r in renderers)
        {
            bounds.Encapsulate(r.bounds);
        }

        // Center object
        target.position = -bounds.center;

        // Move camera
        viewerCamera.transform.position = new Vector3(0, 0, -distance);
        viewerCamera.transform.LookAt(Vector3.zero);
    }
}