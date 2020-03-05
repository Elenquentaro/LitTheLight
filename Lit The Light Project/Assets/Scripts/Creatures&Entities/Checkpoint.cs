using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{
    public static PositionEvent onNewCheckPoint = new PositionEvent();

#if UnityEditor
    void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.green;
        style.alignment = TextAnchor.UpperCenter;
        Handles.Label(transform.position, "Checkpoint", style);
    }
#endif

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>()) onNewCheckPoint?.Invoke(transform.position);
    }
}
