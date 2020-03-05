using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lantern : MonoBehaviour
{
    public static GenericEvent<Lantern> onWithPlayerCollided = new GenericEvent<Lantern>();

    // public static EmptyEvent onLit = new EmptyEvent();

    [SerializeField, Range(0, 3)] private int number = 0;
    public int Number => number;

    public void Assign(int num)
    {
        number = num;
        Debug.Log("Lanter number " + number + " assigned");
    }

    // public void Lit()
    // {
    //     onLit?.Invoke();
    // }

    public void PlayLited() => GetComponent<Animator>().SetBool("Lit", true);

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>())
        {
            onWithPlayerCollided?.Invoke(this);
        }
    }
}
