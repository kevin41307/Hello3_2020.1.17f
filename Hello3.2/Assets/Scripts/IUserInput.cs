using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{
    [Header("===== output signal =====")]
    public float targetDup;
    public float targetDright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;

    //1.press signal
    public bool run = false;
    //2.trigger once signal
    public bool defense = false;
    public bool roll = false;
    public bool lockon = false;
    public bool action = false;
    public bool rb;
    public bool rt;
    public bool lb;
    public bool lt;
    //public bool attack = false;
    protected bool lastAttack = false;
    public bool jump = false;
    protected bool lastJump = false;


    [Header("===== Others =====")]
    [HideInInspector]
    public bool inputEnable = true;

    protected float Dup;
    protected float Dright;
    protected float DupVelocity;
    protected float DrightVelocity;


    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1f - 0.5f * input.y * input.y);
        output.y = input.y * Mathf.Sqrt(1f - 0.5f * input.x * input.x);
        return output;
    }

    protected void UpdateDmagDvec(float Dup2, float Dright2)
    {
        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;
    }
    /*
    protected Quaternion TargetRotation(Vector3 _forward)
    {
        _forward.y = 0;
        Quaternion rt = Quaternion.LookRotation(_forward);
        return 

    }
    */
}
