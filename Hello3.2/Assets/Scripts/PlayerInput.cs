using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("===== Keyboard settings =====")]
    string keyUp = "w";
    string keyDown = "s";
    string keyLeft = "a";
    string keyRight = "d";

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;

    public string keyJUp;
    public string keyJDown;
    public string keyJRight;
    public string keyJLeft;

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
    public bool attack = false;
    private bool lastAttack = false;
    public bool jump = false;
    private bool lastJump = false;
    

    [Header("===== Others =====")]
    [HideInInspector]
    public bool inputEnable = true;

    private float Dup;
    private float Dright;
    private float DupVelocity;
    private float DrightVelocity;


    private void Update()
    {

        Jup = (Input.GetKey(keyJUp) ? 1f : 0) - (Input.GetKey(keyJDown) ? 1f : 0);
        Jright = (Input.GetKey(keyJRight) ? 1f : 0) - (Input.GetKey(keyJLeft) ? 1f : 0);


        targetDup = ((Input.GetKey(keyUp)) ? 1f : 0) - (Input.GetKey(keyDown) ? 1f : 0) ;
        targetDright = (Input.GetKey(keyRight) ? 1f : 0) - (Input.GetKey(keyLeft) ? 1f : 0);

        if (!inputEnable)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref DupVelocity, 0.08f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref DrightVelocity, 0.08f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        Dvec = Dright2 * Vector3.right +  Dup2 * Vector3.forward;

        run = Input.GetKey(keyA);

        bool newJump = Input.GetKey(keyB);
        if (newJump != lastJump && newJump == true)
        {
            jump = true;

            //Debug.Log("jump trigger");
        }
        else 
        {
            jump = false;
        }
        lastJump = newJump;

        bool newAttack = Input.GetKey(keyC);
        if (newAttack != lastAttack && newAttack == true)
        {
            attack = true;

            //Debug.Log("jump trigger");
        }
        else
        {
            attack = false;
        }
        lastAttack = newAttack;



    }



    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1f - 0.5f * input.y * input.y);
        output.y = input.y * Mathf.Sqrt(1f - 0.5f * input.x * input.x);
        return output;
    }
}
