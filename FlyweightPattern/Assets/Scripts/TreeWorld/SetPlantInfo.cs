using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlantInfo : MonoBehaviour
{
    public GameObject plantInfoPanel;
    public RawImage plantIcon;
    public Text planeName;
    public Text phreatLevel;

    public LayerMask whatIsPlant;

    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    public void OpenPlantPanel()
    {
        plantInfoPanel.SetActive(true);
    }

    public void ClosePlantPanel()
    {
        plantInfoPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit,whatIsPlant))
            {
                PlantDataSO pData = hit.transform.GetComponent<Plant>().PlantInfo;
                plantIcon.texture = pData.Icon;
                planeName.text = pData.Name;
                phreatLevel.text = pData.Threat.ToString();
            }
        }
    }
}
