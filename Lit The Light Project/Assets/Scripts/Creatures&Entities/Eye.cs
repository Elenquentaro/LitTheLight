using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Eye : Damager
{
    [SerializeField] private Vector3 areal;

    [SerializeField] private float arealRadius = 2.1f;

    [SerializeField] private bool isFacingRight = false;

    private Rigidbody2D EyeBody;

    private int directionModifier = -1;

    void Start()
    {
        EyeBody = GetComponent<Rigidbody2D>();
        if (isFacingRight)
        {
            Flip();
        }
    }

    void Update()
    {
        if (directionModifier * (areal.x + (directionModifier * arealRadius) - EyeBody.position.x) > 0.2)
            EyeBody.velocity = new Vector2((directionModifier * 0.7f), 0);
        else
            Flip();
    }

    private void Flip()
    {
        directionModifier *= -1;
        transform.FlipX();
    }

    public void SetArealPosition()
    {
        areal = transform.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(areal, areal + new Vector3(arealRadius, 0, 0));
        Gizmos.DrawLine(areal, areal - new Vector3(arealRadius, 0, 0));
    }
}
