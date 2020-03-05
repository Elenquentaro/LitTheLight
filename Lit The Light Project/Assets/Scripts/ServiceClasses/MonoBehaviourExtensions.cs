using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static void DelayedAction(this MonoBehaviour monoBehaviour, float time, EmptyAction action)
    {
        monoBehaviour.StartCoroutine(DelayedActionRoutine(time, action));
    }

    private static IEnumerator DelayedActionRoutine(float time, EmptyAction action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }

    public static void PhysicsProcess(this MonoBehaviour monoBehaviour,
                                    float duration,
                                    EmptyAction repeatingAction,
                                    EmptyAction postAction = null)
    {
        monoBehaviour.StartCoroutine(PhysicsProcessRoutine(duration, repeatingAction, postAction));
    }

    private static IEnumerator PhysicsProcessRoutine(float duration,
                                                    EmptyAction repeatingAction,
                                                    EmptyAction postAction)
    {
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.fixedDeltaTime;
            repeatingAction?.Invoke();
            yield return new WaitForFixedUpdate();
        }
        postAction?.Invoke();
    }
}
