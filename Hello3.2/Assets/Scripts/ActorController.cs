using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    private Rigidbody rigid;
    [HideInInspector]
    public IUserInput pi;
    private Animator anim;
    private CapsuleCollider col;

    [Space(10)]
    [Header("===== Friction settings ======")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    private Vector3 planarVec;
    private float walkSpeed = 3.0f;
    private float runSpeed = 2.0f;
    private float runMultiply = 1f;
    private Vector3 jumpThrust;
    private float jumpVelocity = 4.0f;
    private float rollVelocity = 3.2f;
    private float attackVelocity = 1f;
    private float jabMultiplier = 2f;
    private float lerpTarget;
    private Vector3 deltaPosition;

    private bool lockPlanar = false;
    [SerializeField]
    private bool canAttack = true;

    private float turnSmoothVelocity;
    private Camera mainCamera;
    private float targetRotation;

    public bool leftIsShield = true;

    public delegate void OnActionDelegate();
    public event OnActionDelegate OnAction;

    public bool isDummy = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        IUserInput[] inputs = GetComponents<IUserInput>();
        foreach (var input in inputs)
        {
            if (input.enabled)
            {
                pi = input;
                break;
            }           
        }
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        mainCamera = Camera.main;

        //print( transform.DeepFind("Bip01 R Toe0"));
    }
    private void Update()
    {
        if (isDummy)
            return;

        anim.SetBool("defense", pi.defense);
        if ( (pi.rb || pi.lb )  && canAttack && (CheckState("Idle - Walk") || CheckStateTag("attackL") || CheckStateTag("attackR")) )
        {
            if(pi.rb )
            {
                anim.SetBool("R0L1", false);
                anim.SetTrigger("attack");
            }
            else if (pi.lb && !leftIsShield)
            {
                anim.SetBool("R0L1", true);
                anim.SetTrigger("attack");          
            }

        }

        if(leftIsShield)
        {
            if(CheckState("Idle - Walk"))
            {
                anim.SetLayerWeight(anim.GetLayerIndex("Defense Layer"), 1f);
                anim.SetBool("defense", pi.defense);
            }
            else
            {
                anim.SetBool("defense", false);
                anim.SetLayerWeight(anim.GetLayerIndex("Defense Layer"), 0);
            }

        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Defense Layer"), 0);
        }

        if (pi.run)
            runMultiply = Mathf.Lerp(runMultiply, runSpeed, 0.3f);
        else
            runMultiply = 1f;

        anim.SetFloat("forward", pi.Dmag * ((pi.run) ? runMultiply : 1f));

        if(pi.roll || rigid.velocity.magnitude > 7f) // roll or jab
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

        if(pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        if (pi.targetDright != 0f || pi.targetDup != 0f) //pi.Dmag > 0.1f ||(pi.targetDright != 0f || pi.targetDup != 0f)
        {
            //transform.forward = Vector3.Slerp(transform.forward, pi.Dvec , 0.01f);
            //pi.targetDright, pi.targetDup
            targetRotation = Mathf.Atan2(pi.targetDright, pi.targetDup) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, 0.1f);

            //Quaternion rt = Quaternion.LookRotation(pi.Dvec);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rt, rotateSpeed * Time.deltaTime);

        }

        if(pi.action)
        {
            OnAction.Invoke();
        }

        if (lockPlanar == false)
            planarVec = pi.Dmag * transform.forward * walkSpeed * ((pi.run) ? runMultiply : 1f) ;

        /*
        if(rigid.velocity.magnitude > 5f)
        {
            anim.SetTrigger("roll");
        }
        */
    }

    private void FixedUpdate()
    {
        rigid.position += deltaPosition;
        //rigid.position +=  pi.Dmag * walkSpeed * Time.fixedDeltaTime * transform.forward;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + jumpThrust;
        jumpThrust = Vector3.zero;
        deltaPosition = Vector3.zero;
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
        canAttack = true;
        col.material = frictionOne;

    }

    public void OnGroundExit()
    {
        col.material = frictionZero;
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

        jumpThrust = new Vector3(0, rollVelocity, 0) + anim.GetFloat("rollVelocity") * transform.forward * rollVelocity * rollVelocity ;
    }

    public void OnJabEnter()
    {
        lockPlanar = true;
        pi.inputEnable = false;
        canAttack = false;
    }

    public void OnJabUpdate()
    {
        jumpThrust = anim.GetFloat("jabVelocity") * transform.forward * jabMultiplier;

    }

    public void OnAttackExit()
    {
        //print("attackexit");
        SendMessage("WeaponDisable");
    }

    public void OnAttack1h1Enter()
    {
        //lockPlanar = false;
        pi.inputEnable = false;
        lerpTarget = 1.0f;
    }

    public void OnAttack1h1Stay()
    {
        jumpThrust = anim.GetFloat("attackVelocity") * transform.forward ;
        //anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack Layer")), lerpTarget, 0.3f));
    }

    public void OnAttackIdleEnter()
    {
        lerpTarget = 0f;
    }

    public void OnAttackIdleUpdate()
    {
        pi.inputEnable = true;
        //anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack Layer")), lerpTarget, 0.3f));

    }

    public void OnUpdateRM(object _deltaPos)
    {
        if(CheckState("attack1hC"))
            deltaPosition += (Vector3)_deltaPos;
    }

    public  void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void SetBool(string name, bool value)
    {
        anim.SetBool(name, value);
    }

    public bool CheckState(string stateName, string layerName = "Base Layer" )
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }

    public bool CheckStateTag(string tagName, string layerName = "Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsTag(tagName);
    }

    /*
    v = mag * Mathf.SmoothDamp(v, Dup, ref smoothVelocity, 0.1f);
    h = mag * Mathf.SmoothDamp(h, Dright, ref smoothVelocity2, 0.1f);
    direction = new Vector2(h, v);
    */

    //transform.Translate(1.4f * transform.forward * Time.deltaTime);//不會抖腳
}
