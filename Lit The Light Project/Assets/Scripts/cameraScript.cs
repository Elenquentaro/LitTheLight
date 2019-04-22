using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }

    void Update()
    {
        if (target)
        {
            Vector3 shiftPoint = new Vector3(target.position.x, target.position.y + 0.75f, target.position.z);
            Vector3 point = mainCamera.WorldToViewportPoint(shiftPoint);
            Vector3 destination = transform.position + shiftPoint - mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }

}
