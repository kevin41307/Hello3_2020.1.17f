using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{



    private Rigidbody rigid;
    PlayerInput pi;
    private Animator anim;

    private Vector3 planarVec;

    private float walkSpeed = 3.0f;
    private float runSpeed = 2.0f;
    private float runMultiply = 1f;

    private Vector3 jumpThrust;
    private float jumpVelocity = 4.0f;
    private float rollVelocity = 3.2f;
    private float jabMultiplier = 2f;

    private bool lockPlanar = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        pi = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (pi.attack)
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1f);
            anim.SetTrigger("attack");
            
        }  


        if (pi.run)
            runMultiply = Mathf.Lerp(runMultiply, runSpeed, 0.3f);
        else
            runMultiply = 1f;

        anim.SetFloat("forward", pi.Dmag * ((pi.run) ? runMultiply : 1f));
        if(pi.jump)
        {
            anim.SetTrigger("jump");
        }
            

        if (pi.Dmag > 0.1f)
        {
            transform.forward = Vector3.Slerp(transform.forward, pi.Dvec, 0.3f);
        }

        if(!lockPlanar)
            planarVec = pi.Dmag * transform.forward * walkSpeed * ((pi.run) ? runMultiply : 1f) ;

        if(rigid.velocity.magnitude > 5f)
        {
            anim.SetTrigger("roll");
        }
    }

    private void FixedUpdate()
    {

        //rigid.position +=  pi.Dmag * walkSpeed * Time.fixedDeltaTime * transform.forward;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + jumpThrust;
        jumpThrust = Vector3.zero;

    }

    /// <summary>
    /// message process block
    /// </summary>

    public void OnJumpEnter()
    {
        //Debug.Log("On jump Enter");
        lockPlanar = true; 
        pi.inputEnable = false;
        jumpThrust = new Vector3(0, jumpVelocity, 0);
    }
    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        lockPlanar = false;
        pi.inputEnable = true;
    }
    public void OnFallEnter()
    {
        lockPlanar = false;
        pi.inputEnable = true;
    }

    public void OnRollEnter()
    {
        lockPlanar = true;
        pi.inputEnable = false;
        jumpThrust = new Vector3(0, rollVelocity, 0);
    }

    public void OnJabEnter()
    {
        lockPlanar = true;
        pi.inputEnable = false;
    }

    public void OnJabUpdate()
    {
        jumpThrust = anim.GetFloat("jabVelocity") * transform.forward * jabMultiplier;

    }

    public void OnAttackExit()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
    }


    /*
    v = mag * Mathf.SmoothDamp(v, Dup, ref smoothVelocity, 0.1f);
    h = mag * Mathf.SmoothDamp(h, Dright, ref smoothVelocity2, 0.1f);
    direction = new Vector2(h, v);
    */

    //transform.Translate(1.4f * transform.forward * Time.deltaTime);//不會抖腳
}
