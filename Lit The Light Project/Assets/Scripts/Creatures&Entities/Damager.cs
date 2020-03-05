using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.GetComponent<PlayerController>()?.DamageSelf();
    }
}
