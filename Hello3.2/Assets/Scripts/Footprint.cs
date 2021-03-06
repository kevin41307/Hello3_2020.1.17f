using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    private bool footUp = false;
    private bool footDown = false;
    private Animator animator;
    private BoxCollider boxCollider;
    private AnimatorStateInfo stateInfo;
    private float cdTimer = cdTime;
    private const float cdTime = 0.15f;
    private void Awake()
    {
        boxCollider = this.gameObject.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(0.24f, 0.24f, 0.24f);
        boxCollider.isTrigger = true;
    }
    public virtual void Start()
    {
        animator = Game.playerAttrSingle.animator;
    }

    private void Update()
    {
        if(cdTimer > 0f)
            cdTimer -= Time.deltaTime;     
    }


    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log();
        if (footUp && other.gameObject.layer ==LayerMask.NameToLayer("Player"))
        {
            footUp = false;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if(animator.GetBool("isGrounded") && cdTimer <= 0)
            {
                Game.playerAttrSingle.transform.SendMessage("PlayFootsteps", SendMessageOptions.DontRequireReceiver);
                cdTimer = cdTime;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            footUp = true;
        }
    }
}
