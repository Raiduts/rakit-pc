using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    public static CameraZoom Instance;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZ = -2f;
    [SerializeField] private float maxZ = -10f;

    public static bool IsZooming;

    private CinemachineVirtualCamera vcam;

    private void Awake()
    {
        Instance = this;

        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    //private void Update()
    //{
    //    HandleMouseZoom();
    //    HandleTouchZoom();
    //}

    //private void HandleMouseZoom()
    //{
    //    float scroll = Input.mouseScrollDelta.y;

    //    if (scroll == 0) return;

    //    Zoom(scroll * zoomSpeed * Time.deltaTime * 100f);
    //}

    //private void HandleTouchZoom()
    //{
    //    if (Input.touchCount != 2) return;

    //    Touch touch0 = Input.GetTouch(0);
    //    Touch touch1 = Input.GetTouch(1);

    //    Vector2 touch0Prev = touch0.position - touch0.deltaPosition;
    //    Vector2 touch1Prev = touch1.position - touch1.deltaPosition;

    //    float prevMagnitude = (touch0Prev - touch1Prev).magnitude;

    //    float currentMagnitude = (touch0.position - touch1.position).magnitude;

    //    float difference = currentMagnitude - prevMagnitude;

    //    Zoom(-difference * zoomSpeed * Time.deltaTime);
    //}

    private void Zoom(float amount)
    {
        if (vcam)
        {
            CinemachineVirtualCamera virtualCamera =
                vcam.GetComponent<CinemachineVirtualCamera>();

            CinemachineTransposer transposer =
                virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

            Vector3 adder = transposer.m_FollowOffset;
            
            adder.z = amount;
            adder.z = Mathf.Clamp(adder.z, maxZ, minZ);

            transposer.m_FollowOffset = adder;
        }
        else if (RotationViewerArea.IsHovering)
        {
            Vector3 adder = transform.position;

            adder.z += amount;
            adder.z = Mathf.Clamp(adder.z, maxZ, minZ);

            transform.position = adder;
        }

        //IsZooming = false;
    }

    private Coroutine zoomCoroutine;

    private IEnumerator ZoomDelay()
    {
        yield return new WaitForSeconds(0.5f);
        IsZooming = false;
    }

    public void ZoomBySlider(Slider slider)
    {
        IsZooming = true;

        if (zoomCoroutine != null)
            StopCoroutine(zoomCoroutine);

        zoomCoroutine = StartCoroutine(ZoomDelay());

        float zoomValue = maxZ + ((Mathf.Abs(maxZ) - Mathf.Abs(minZ)) * slider.value);
        Zoom(zoomValue);
    }

    public void SetZoom(float amount)
    {
        CinemachineVirtualCamera virtualCamera =
            vcam.GetComponent<CinemachineVirtualCamera>();

        CinemachineTransposer transposer =
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        transposer.m_FollowOffset = new(0, 0, amount);
    }
}