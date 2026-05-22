using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    public static CinemachineController Instance;

    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform cameraCenter, cameraFollow;

    [SerializeField] private Vector3 ogPos;

    private Sequence seq;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //ogPos = cameraTarget.position;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ChangeTarget(Transform target, Action action = null)
    {
        seq?.Kill();

        seq = DOTween.Sequence();

        seq.Append(cameraFollow.DOMove(target.position, 1f));

        seq.Join(cameraFollow.DORotateQuaternion(target.rotation, 1));

        seq.AppendCallback(() =>
        {
            action?.Invoke();
        });
    }
}
