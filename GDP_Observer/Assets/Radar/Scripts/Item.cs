using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public EventSO dropped;
    public EventSO pickup;

    public Image icon;

    private void Start()
    {
        dropped.Occurred(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            pickup.Occurred(this.gameObject);
        }
    }
}
