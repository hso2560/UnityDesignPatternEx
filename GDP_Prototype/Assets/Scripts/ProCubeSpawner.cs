using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProCubeSpawner : MonoBehaviour
{
    
    void Update()
    {
        if (Random.Range(0, 100) < 10)
        {
            //ProcCube.CreateCube(transform.position);
            ProcCube.Clone(transform.position);
        }
    }
}
