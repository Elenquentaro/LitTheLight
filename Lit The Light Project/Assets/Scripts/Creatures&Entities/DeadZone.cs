using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(BoxCollider2D))]
public class DeadZone : Damager
{
#if UnityEditor
    void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.alignment = TextAnchor.UpperCenter;
        Handles.Label(transform.position, "DeadZone", style);
    }
#endif
}
