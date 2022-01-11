using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameEnvironment
{
    private static GameEnvironment _instance;

    public List<GameObject> CheckpointList = new List<GameObject>();

    public static GameEnvironment Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameEnvironment();
                _instance.CheckpointList.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));

                _instance.CheckpointList =_instance.CheckpointList.OrderBy(_ => _.name).ToList();
            }

            return _instance;
        }
    }
}
