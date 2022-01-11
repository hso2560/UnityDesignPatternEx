using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant Data", menuName = "Scriptable Object/Plant Data")]
public class PlantDataSO : ScriptableObject
{
    public enum eThreat : int
    {
        NONE = 0,
        LOW,
        MODERATE,
        HIGH
    }

    [SerializeField] private string plantName;
    [SerializeField] private eThreat plantThreat;
    [SerializeField] private Texture icon;

    public string Name { get { return plantName; } }
    public eThreat Threat { get { return plantThreat; } }
    public Texture Icon { get { return icon; } }


    public void SetRandomName()
    {
        plantName = Utils.GetRandomName(Random.Range(6,10));
    }

    public void SetRandomThreat()
    {
        plantThreat = Utils.GetRandomEnum<eThreat>();
    }

}
