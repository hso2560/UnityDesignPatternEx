using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cube;

    private int score;
    private int killCnt;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Enemy e = Instantiate(cube, new Vector3(Random.value, Random.value, Random.value), Quaternion.identity).GetComponent<Enemy>();
            e.deathEvent += () =>
            {
                score += 2;
                killCnt++;
                Debug.Log($"Å³: {killCnt}, Á¡¼ö: {score}");
                Destroy(e.gameObject);
            };
        }
    }
}
