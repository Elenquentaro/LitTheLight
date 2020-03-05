using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Crystal : MonoBehaviour
{
    public static EmptyEvent onPickUp = new EmptyEvent();

    private Animator animCrystal;

    void Awake()
    {
        GameManager.onGameStarted.AddListener((progress) =>
        {
            if (progress.HasKey) Destroy(gameObject);
        });

        animCrystal = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>())
        {
            animCrystal.SetBool("isActive", false);
        }
    }

    //called from animation
    void PickedUp()
    {
        onPickUp?.Invoke();
        Destroy(gameObject);
    }
}
