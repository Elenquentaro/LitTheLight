using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void FlipX(this Transform transform)
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public static Vector2 posVector2(this Transform transform)
    {
        return transform.position;
    }
}
