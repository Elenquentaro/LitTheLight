using System;
using System.Collections.Generic;
using UnityEngine;

public class Controls
{
    private static KeyCode[] jump = { KeyCode.W, KeyCode.Space, KeyCode.UpArrow };

    public static bool IsJumpKeyDown => CheckKeysDownFromArray(jump);

    public static bool IsJumpKeyUp => CheckKeysUpFromArray(jump);

    public static bool CheckKeysDownFromArray(KeyCode[] keys)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i])) return true;
        }
        return false;
    }

    public static bool CheckKeysUpFromArray(KeyCode[] keys)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyUp(keys[i])) return true;
        }
        return false;
    }
}
