using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCharge : Damager
{
    [SerializeField] private float chargeSpeed = 6f;

    [SerializeField] private float chargeTime = 2.5f;

    public float FlightDistance => chargeSpeed * chargeTime;

    private Animator energyAnimator;

    private Rigidbody2D body;

    private EmptyAction explosionAction;

    public State animState
    {
        get => (State)energyAnimator.GetInteger("State");
        set => energyAnimator.SetInteger("State", (int)value);
    }

    public void Assign(EmptyAction onExplosion, float preparingTime)
    {
        explosionAction = onExplosion;

        body = GetComponent<Rigidbody2D>();
        energyAnimator = GetComponent<Animator>();

        animState = State.Circle;
        this.DelayedAction(preparingTime, () =>
        {
            animState = State.Charge;
            this.PhysicsProcess(chargeTime, () =>
            {
                body.velocity = new Vector2(-chargeSpeed * transform.parent.localScale.x, 0);
            }, Boom);
        });
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>() || collider.gameObject.layer == 8)
        {
            Boom();
        }
    }

    private void Boom()
    {
        animState = State.Explosion;
        StopAllCoroutines();
        body.velocity = new Vector2(0, 0);
    }

    //called from animation
    private void Exploded()
    {
        explosionAction?.Invoke();
        Destroy(gameObject);
    }

    public enum State
    {
        Circle,     //0
        Charge,     //1
        Explosion   //2
    }
}
