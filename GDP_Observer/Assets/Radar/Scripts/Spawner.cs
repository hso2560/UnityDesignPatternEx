using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public GameObject eggPrefab;
    public GameObject medkitPrefab;

    public Terrain terrain;

    TerrainData terrainData;

    private void Start()
    {
        terrainData = terrain.terrainData;

        InvokeRepeating("CreateEgg", 1, 1);
        InvokeRepeating("CreateMedKit", 1, 1);
    }

    private void CreateEgg()
    {
        int x = (int)Random.Range(0, terrainData.size.x);
        int z = (int)Random.Range(0, terrainData.size.z);

        Vector3 pos = new Vector3(x, 0, z);
        pos.y = terrain.SampleHeight(pos) + 10;

        GameObject eggObj = Instantiate(eggPrefab, pos, Quaternion.identity);
        eggObj.transform.SetParent(this.transform);
    }

    private void CreateMedKit()
    {
        int x = (int)Random.Range(0, terrainData.size.x);
        int z = (int)Random.Range(0, terrainData.size.z);

        Vector3 pos = new Vector3(x, 0, z);
        pos.y = terrain.SampleHeight(pos) + 10;

        GameObject medkitObj = Instantiate(eggPrefab, pos, Quaternion.identity);
        medkitObj.transform.SetParent(this.transform);
    }
}
