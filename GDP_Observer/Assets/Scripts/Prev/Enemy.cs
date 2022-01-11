using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event Action deathEvent;

    private void Start()
    {
        Invoke("Death", 3);
    }

    void Death() => deathEvent();
}
