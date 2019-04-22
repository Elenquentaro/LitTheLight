using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blobScript : MonoBehaviour
{
    private Vector3 blobPos;
    public GameObject energyCharge;
    private Animator animBlob;

    public bool isCreateAviable = true;
    public bool isFacingRight = false;

    void Start()
    {
        if (isFacingRight)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        animBlob = GetComponent<Animator>();
    }

    void Update()
    {
        if (isCreateAviable)
        {
            Reload();
        }
    }

    public void setCreateAviable()
    {
        isCreateAviable = true;
    }

    private void Create()
    {
        Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y + 1.5f, transform.localPosition.z);
        Instantiate(energyCharge, position, transform.rotation, transform);
        isCreateAviable = false;
    }

    private void Reload()
    {
        animBlob.SetBool("Reloading", true);
    }

    private void Reloaded()
    {
        animBlob.SetBool("Reloading", false);
    }
}
