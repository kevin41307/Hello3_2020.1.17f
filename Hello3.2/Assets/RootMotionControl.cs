using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionControl : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        anim.SendMessage("OnUpdateRM", (object)anim.deltaPosition);
        //print(anim.deltaPosition.x);
    }
}
