using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private PlantDataSO plantInfo;

    public PlantDataSO PlantInfo { get { return plantInfo; } }

    private void Start()
    {
        plantInfo.SetRandomName();
        plantInfo.SetRandomThreat();
    }
}
