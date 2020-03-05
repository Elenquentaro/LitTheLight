using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Damager
{
    [SerializeField] private GameObject energyMissilePrefab;

    [SerializeField] private float minCirclingTime = 1f;

    [SerializeField] private float maxCirclingTime = 2.5f;

    [SerializeField] private bool isFacingRight = false;

    private Animator animBlob;

    void Start()
    {
        if (isFacingRight)
        {
            transform.FlipX();
        }
        animBlob = GetComponent<Animator>();
        Reload();
    }

    //calling from animation
    private void Shoot()
    {
        Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y + 1.5f, transform.localPosition.z);
        Instantiate(energyMissilePrefab, position, transform.rotation, transform)
            .GetComponent<EnergyCharge>().Assign(Reload, Random.Range(minCirclingTime, maxCirclingTime));
    }

    private void Reload()
    {
        animBlob.SetBool("Reloading", true);
        this.DelayedAction(animBlob.GetCurrentAnimatorClipInfo(0)[0].clip.length, Reloaded);
    }

    private void Reloaded()
    {
        animBlob.SetBool("Reloading", false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 basePos = transform.position + Vector3.up;
        float distance = energyMissilePrefab.GetComponent<EnergyCharge>().FlightDistance;
        Gizmos.DrawLine(basePos, basePos + Vector3.right * (isFacingRight ? 1 : -1) * distance);
    }
}
