using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyScript : MonoBehaviour
{
    private Rigidbody2D Body;
    private Animator energyAnimator;
    public float tMin = 0.5f, tMax = 1.5f;
    public float timer = 0;
    public float timeCharge;
    public float chargeSpeed = 6f;
    public float lifeTime = 2.5f;
    public bool Creatable;

    public State animState
    {
        get => (State)energyAnimator.GetInteger("State");
        set => energyAnimator.SetInteger("State", (int)value);
    }

    void Start()
    {
        timeCharge = Random.Range(tMin, tMax);
        Body = GetComponent<Rigidbody2D>();
        energyAnimator = GetComponent<Animator>();
        animState = State.Circle;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= timeCharge && animState == State.Circle)
        {
            animState = State.Charge;
            Body.velocity = new Vector2(-chargeSpeed * transform.parent.localScale.x, 0);
            timer = 0;
        }
        if (timer >= lifeTime && animState != State.Circle)
        {
            Boom();
            /*if (timer > lifeTime + 1.2f)
            {
                Exploded();
            }*/
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 8)
            Boom();
        if (collider.gameObject.tag == "Player")
            Boom();
    }

    private void Boom()
    {
        animState = State.Explosion;
        Body.velocity = new Vector2(0, 0);
    }

    private void Exploded()
    {
        // Creatable = transform.parent.gameObject.GetComponent<blobScript>().isCreateAviable;
        blobScript Creatable = transform.parent.gameObject.GetComponent<blobScript>();
        Creatable.setCreateAviable();
        Destroy(gameObject);
    }

    public enum State
    {
        Circle,     //0
        Charge,     //1
        Explosion   //2
    }
}
