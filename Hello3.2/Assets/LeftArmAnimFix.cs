using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour
{
    private Animator anim;
    private ActorController ac;
    private Vector3 fixRotate;
    Transform leftArm;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ac = GetComponent<ActorController>();
        fixRotate = new Vector3(-60, 30, -60);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(ac.leftIsShield)
        {
            if (anim.GetBool("defense") == false)
            {
                leftArm = anim.GetBoneTransform(HumanBodyBones.LeftHand);
                leftArm.localEulerAngles += fixRotate;
                anim.SetBoneLocalRotation(HumanBodyBones.LeftHand, Quaternion.Euler(leftArm.localEulerAngles));

            }

        }



    }

}
