using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : MonoBehaviour
{
    [SerializeField] private string partName;

    private ScrewDriver driver;

    [SerializeField] private Transform screwObject;

    [SerializeField] private ScrewDriver driverPref;

    [SerializeField] private float zTarget = -8;

    private float rotateSpeed = 1;

    private bool isPlaced, driverReady;

    public Action ScrewPlaced;

    [Header("Optional")]
    [SerializeField] private HighlightOnTutorial highlight;

    public bool IsPlaced()
    {
        return isPlaced;
    }

    private void OnMouseDown()
    {
        if (isPlaced)
        {
            StopUsingDriver();
            return;
        }

        SummonDriver();
        //transform.rotation +=
    }

    private void OnMouseDrag()
    {
        if (!driverReady || isPlaced) return;

        transform.Rotate(new Vector3(0,0,360) * rotateSpeed * Time.deltaTime);

        screwObject.localPosition = screwObject.localPosition + new Vector3( 0, 0, -5 * Time.deltaTime);

        if (screwObject.localPosition.z <= zTarget)
        {
            isPlaced = true;

            Motherboard.Instance.AddComponent(partName);

            ScoreManager.Instance.AddScore(50);

            if (highlight != null)
            {
                highlight.ForceRemove();            
            }

            ScrewPlaced?.Invoke();

            StopUsingDriver();
        }
    }

    private void OnMouseUp()
    {
        if (driver == null) return;

        StopUsingDriver();
    }

    private void SummonDriver()
    {
        if (isPlaced) return;

        if (driver == null)
        {
            ScrewDriver driverTemp = Instantiate(driverPref, screwObject);

            driver = driverTemp;

            driverTemp.transform.localPosition = Vector3.zero;

            driverTemp.DriverOnPlace += OnDriverOnPlace;
        }
    }

    private void StopUsingDriver()
    {
        if (driver == null)
        {
            return;
        }

        driverReady = false;

        Destroy(driver.gameObject);

        driver = null;
    }

    private void OnDriverOnPlace(ScrewDriver driver)
    {
        //if (!isInteracting || this.driver != null)
        //{
        //    Destroy(driver.gameObject);
        //    return;
        //}

        driverReady = true;
    }
}
