using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class theEyeScript : MonoBehaviour
{
    private Rigidbody2D EyeBody;
    public Transform areal;

    public bool isFacingRight = false;
    private int directionModifier = -1;
    public float distance = 2.1f;

    void Start()
    {
        EyeBody = GetComponent<Rigidbody2D>();
        int Level = PlayerPrefs.HasKey("Level") ? PlayerPrefs.GetInt("Level") : 0;
        if (Level == SceneManager.sceneCountInBuildSettings)
        {
            // Vector3 locPos = GetComponent<GameObject>().transform.localPosition;
            // locPos = new Vector3(PlayerPrefs.GetFloat(gameObject.name), locPos.y,locPos.z);
        }
        else if (isFacingRight)
        {
            Flip();
        }
    }

    void Update()
    {
        if (directionModifier * (areal.position.x + (directionModifier * distance) - EyeBody.position.x) > 0.2)
            EyeBody.velocity = new Vector2((directionModifier * 0.7f), 0);
        else
            Flip();
        PlayerPrefs.SetFloat(gameObject.name, EyeBody.position.x);
    }

    private void Flip()
    {
        directionModifier *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
