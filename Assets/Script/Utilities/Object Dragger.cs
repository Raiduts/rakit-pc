using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDragger : MonoBehaviour
{
    private Collider partCollider;

    [SerializeField] private LayerMask dragLayer;
    [SerializeField] private bool isDraggable = false;

    [Header("Offset")]
    [SerializeField] private Vector3 dragOffset;

    private Vector3 mouseOffset;
    private Rigidbody rb;


    private void Start()
    {
        partCollider = GetComponent<Collider>();
        //rb = GetComponent<Rigidbody>();
        
        //rb.detectCollisions = false;

        if (AssemblyManager.Instance)
        {
            AssemblyManager.Instance.Assembling += OnAssembly;
            OnAssembly(AssemblyManager.Instance.isAssembling);
        }
    }

    public void OnAssembly(bool isAssembling)
    {
        isDraggable = isAssembling;
    }

    private void OnMouseDown()
    {
        if (!isDraggable) return;

        if (!rb)
        {
            rb = gameObject.AddComponent<Rigidbody>();        
        }

        //rb.mass = 2f;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        //rb.isKinematic = false;
        //rb.detectCollisions = true;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, dragLayer))
        {
            mouseOffset = transform.position - hit.point;
        }
    }

    private void OnMouseDrag()
    {
        if (!isDraggable) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }

        if (Physics.Raycast(ray, out hit, 100f, dragLayer))
        {
            Vector3 targetPos =
                hit.point +
                mouseOffset +
                dragOffset;

            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                15f * Time.deltaTime
            );
        }   
    }

    private void OnMouseUp()
    {
        if (!isDraggable) return;

        //Collider[] cols = GetComponentsInChildren<Collider>();

        //for (int i = 0; i < cols.Length; i++)
        //{
        //    cols[i].enabled = true;
        //    if (i == cols.Length - 1)
        //    {
        //        Destroy(rb);
        //    }
        //}

        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            col.enabled = true;
        }

        Invoke(nameof(DestroyRigid), 0.1f);
    }

    private void DestroyRigid()
    {
        if (rb)
        {
            Destroy(rb);
        }
    }

    private void OnDestroy()
    {
        if (AssemblyManager.Instance)
        {
            AssemblyManager.Instance.Assembling -= OnAssembly;
        }
    }
}