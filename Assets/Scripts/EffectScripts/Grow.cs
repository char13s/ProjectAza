using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : Reminant
{
    [SerializeField] private Vector3 rate;
    private Coroutine coroutine;
    public override void Start() {
        base.Start();
        coroutine=StartCoroutine(WaitToDie());
        StartCoroutine(StopGrow());
    }
    private IEnumerator WaitToDie() {
        while (isActiveAndEnabled) {
            
            yield return null;
            transform.localScale += rate;
        }
    }
    private IEnumerator StopGrow() {
        YieldInstruction wait = new WaitForSeconds(0.3f);
        yield return wait;
        StopCoroutine(coroutine);
        Debug.Log("ugh");
    }
}
