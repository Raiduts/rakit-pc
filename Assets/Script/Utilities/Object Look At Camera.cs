using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLookAtCamera : MonoBehaviour
{
    [Header("Optional")]
    [SerializeField] private Transform lookAtTarget;
    [SerializeField] private bool inverted;

    private void Start()
    {
        if (lookAtTarget == null)
        {
            lookAtTarget = CinemachineController.Instance.transform;
        }
    }

    private void FixedUpdate()
    {
        transform.LookAt(lookAtTarget.position);
    }
}
