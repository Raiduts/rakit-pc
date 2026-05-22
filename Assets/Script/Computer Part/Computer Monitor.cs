using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ComputerMonitor : MonoBehaviour
{
    [SerializeField] private Transform cameraView;
    private Collider monitorCollider;
    [SerializeField] private GraphicRaycaster canvasRaycaster;
    [SerializeField] private Button changeViewButton;
    [SerializeField] private BootingScreen bootingScreen;

    private CinemachineController controller;

    private void Start()
    {
        monitorCollider = GetComponent<Collider>();
        controller = CinemachineController.Instance;

        //LookAtMonitor();
    }

    public void LookAtMonitor()
    {
        Invoke(nameof(OnComputerInteract), 0.5f);

        controller.ChangeTarget(canvasRaycaster.transform);
        
        controller.GetComponent<CameraZoom>()?.SetZoom(-3.5f);

        TutorialManager.Instance?.TryFulfillStep("Look At Monitor");

        //Sequence seq = DOTween.Sequence(cameraView);

        //seq.Append(cameraView.DOLocalRotate(new(0, 90, 0), 0.5f).SetEase(Ease.OutQuad));
        //seq.Append(cameraView.DOLocalMove(new(12.5f, 1, -10), 0.5f));
    }

    public void ReturnCamera()
    {
        monitorCollider.enabled = true;
        canvasRaycaster.enabled = false;

        ObjectSummoner.Instance.CallCamera();
    }

    private void OnComputerInteract()
    {
        monitorCollider.enabled = false;
        canvasRaycaster.enabled = true;
    }

    private void OnMouseDown()
    {
        LookAtMonitor();
    }


}
