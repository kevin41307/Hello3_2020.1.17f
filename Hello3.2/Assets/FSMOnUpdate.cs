using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMOnUpdate : StateMachineBehaviour
{
    public string[] onUpdateMessages;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var msg in onUpdateMessages)
        {
            animator.SendMessage(msg);
        }
    }
}
