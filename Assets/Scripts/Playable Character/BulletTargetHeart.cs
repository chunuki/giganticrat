using UnityEngine;
using System;
using System.Collections;

public class BulletTargetHeart : MonoBehaviour
{
    public Akio akio;
    public static event Action OnManInLove; // growtopia superbroadcast type shi

    public void GetShot()
    {
        if (akio.currentState != AkioState.Dead && akio.currentState != AkioState.InLove)
        {
            OnManInLove?.Invoke();
            StartCoroutine(ShotAnimation());
            Debug.Log("He is in Love!");
        }
    }

    private IEnumerator ShotAnimation()
    {
        akio.agent.isStopped = true;
        akio.animator.SetTrigger("IsInLove");
        yield return null;
        yield return new WaitForSeconds(2.0f);
        akio.UpdateState(AkioState.InLove);

    }
}