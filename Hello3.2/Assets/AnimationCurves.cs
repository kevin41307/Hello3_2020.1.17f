using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurves : StateMachineBehaviour
{
    [System.Serializable]
    public struct Curves
    {
        public string param;
        public AnimationCurve curve;
    }

    public Curves[] curves;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var cur in curves)
        {
            animator.SetFloat(cur.param, 1f);
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var cur in curves)
        {
            animator.SetFloat(cur.param, 1f);
            animator.SetFloat(cur.param, cur.curve.Evaluate(stateInfo.normalizedTime));
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var cur in curves)
        {
            animator.SetFloat(cur.param, 1f);
        }
    }
}
