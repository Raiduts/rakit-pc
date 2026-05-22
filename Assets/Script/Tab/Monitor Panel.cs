using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonitorPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMonitor;
    private int errorCounter;

    public void CekPartsInstalled()
    {
        if (Motherboard.Instance == null) return;
        
        textMonitor.text = "Problem : \n";
        
        //foreach (string item in Motherboard.Instance.componentsPlaced)
        //{
        //    textMonitor.text += $"{item}, ";
        //}

        if (!Motherboard.Instance.componentsPlaced.Contains("SSD"))
        {
            errorCounter++;
            textMonitor.text += $"SSD Belum Terpasang, ";
        }
        if (!Motherboard.Instance.componentsPlaced.Contains("RAM"))
        {
            errorCounter++;
            textMonitor.text += $"RAM Belum Terpasang, ";
        }
        if (!Motherboard.Instance.componentsPlaced.Contains("SSD Screw"))
        {
            errorCounter++;
            textMonitor.text += $"Pengunci SSD Belum Terpasang, ";
        }
        if (!Motherboard.Instance.componentsPlaced.Contains("Thermal Paste"))
        {       
            errorCounter++;
            textMonitor.text += $"Thermal Paste Belum Terpasang, ";
        }

        if (errorCounter == 4)
        {
            textMonitor.text = $"HAHA GOBLOK GABISA RAKIT PC";
        }
    }
}
