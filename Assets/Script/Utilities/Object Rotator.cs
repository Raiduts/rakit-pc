using UnityEngine;
using UnityEngine.UI;

public class ObjectRotator : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private RectTransform rotateArea;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private bool verticalRotation = true;
    [SerializeField] private bool horizontalRotation = true;
    [SerializeField] private bool invertX = false;
    [SerializeField] private bool invertY = false;

    private Vector2 lastPointerPosition;
    private bool isDragging;
    private bool canRotate;

    private void Start()
    {
        if (AssemblyManager.Instance)
        {
            AssemblyManager.Instance.Assembling += OnAssembly;
            OnAssembly(AssemblyManager.Instance.isAssembling);
        }
        else
        {
            canRotate = true;
        }
    }

    public void OnAssembly(bool isAssembling)
    {
        if (isAssembling)
        {
            transform.rotation = Quaternion.identity;
        }

        canRotate = !isAssembling;
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        HandleMouseInput();
#endif

#if UNITY_ANDROID || UNITY_IOS
        HandleTouchInput();
#endif
    }

    private bool IsPointerInsideUI(Vector2 screenPos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            rotateArea,
            screenPos
        );
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!RotationViewerArea.IsHovering || CameraZoom.IsZooming)
                return;

            isDragging = true;
            lastPointerPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 currentPosition = Input.mousePosition;
            Vector2 delta = currentPosition - lastPointerPosition;

            RotateObject(delta);

            lastPointerPosition = currentPosition;
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount != 1) return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:

                if (!RotationViewerArea.IsHovering)
                    return;

                isDragging = true;
                lastPointerPosition = touch.position;
                break;

            case TouchPhase.Moved:

                if (!isDragging) return;

                Vector2 delta = touch.position - lastPointerPosition;

                RotateObject(delta);

                lastPointerPosition = touch.position;
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                isDragging = false;
                break;
        }
    }

    private void RotateObject(Vector2 delta)
    {
        if (!canRotate) return;

        float rotX = delta.y * rotationSpeed * (invertY ? 1 : -1) * (verticalRotation ? 1 : 0);
        float rotY = delta.x * rotationSpeed * (invertX ? -1 : 1) * (horizontalRotation ? 1 : 0);

        transform.Rotate(Vector3.up, -rotY * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, -rotX * Time.deltaTime, Space.World);
    }

    private void OnDestroy()
    {
        if (AssemblyManager.Instance)
        {
            AssemblyManager.Instance.Assembling -= OnAssembly;
        }
    }
}