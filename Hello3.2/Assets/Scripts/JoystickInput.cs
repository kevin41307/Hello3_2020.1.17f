using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : IUserInput
{
    [Header("===== Joystick settings =====")]
    public string axisX = "axisX";
    public string axisY = "axisY";

    public string axis3 = "axis3";
    public string axis6 = "axis6";

    public string btnA = "btn0"; //sqaure
    public string btnB = "btn1"; // X
    public string btnC = "btn2"; // O
    public string btnD = "btn3"; // triangle
    public string btnLB = "btn4"; 
    public string btnLT = "btn6"; 
    public string btnRB = "btn5";
    public string btnRT = "btn7"; 

    public MyButton buttonA = new MyButton();
    public MyButton buttonB = new MyButton();
    public MyButton buttonC = new MyButton();
    public MyButton buttonD = new MyButton();
    public MyButton buttonLB = new MyButton();
    public MyButton buttonLT = new MyButton();
    public MyButton buttonRB = new MyButton();
    public MyButton buttonRT = new MyButton();

    public MyButton buttonX = new MyButton();
    /*
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
    */
    //Input.GetKey(joystick button 4)
    private void Update()
    {
        //buttonX.Tick(Input.GetKey("y"));
        //print(buttonB.OnReleased);
        //print(buttonX.IsDelaying);
        //        print(!buttonX.IsDelaying && buttonX.OnReleased || buttonX.IsExtending);
        //print( (buttonX.IsPressing && !buttonX.IsDelaying ) || buttonX.IsExtending);
        //print(buttonX.OnReleased && buttonX.IsDelaying);
        //print(buttonX.IsExtending);
        //print(buttonX.OnReleased && buttonX.IsDelaying);
        /*
        

        print(buttonX.delayTimer.state);
        print(buttonX.delayTimer.elapsedTime);        */

        buttonA.Tick(Input.GetButton(btnA));
        //test
        //buttonB.Tick(Input.GetKey("y"));
        buttonB.Tick(Input.GetButton(btnB));
        buttonC.Tick(Input.GetButton(btnC));
        buttonD.Tick(Input.GetButton(btnD));
        buttonLB.Tick(Input.GetButton(btnLB));
 //       buttonLT.Tick(Input.GetButton(btnLT));
        buttonRB.Tick(Input.GetButton(btnRB));
 //       buttonRT.Tick(Input.GetButton(btnRT));

        Jup = Input.GetAxis(axis6);
        Jright = Input.GetAxis(axis3) ;

        targetDup = Input.GetAxis(axisY);
        targetDright = Input.GetAxis(axisX) ;

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
        Dvec = Dright2 * transform.right + Dup2 * transform.forward ;

        //defense = Input.GetButton(btnLB);
        defense = buttonLB.IsPressing;

        //run = Input.GetButton(btnB);

        /*
        bool newJump = Input.GetButtonDown(btnC);
        if (newJump != lastJump && newJump == true)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        lastJump = newJump;
        */
        run = (buttonB.IsPressing && !buttonB.IsDelaying) || buttonB.IsExtending;
        jump = buttonB.OnPressed && buttonB.IsExtending;
        roll = buttonB.OnReleased && buttonB.IsDelaying;

        action = buttonC.IsPressing;
        //print(buttonB.OnReleased);
        //print(buttonB.IsDelaying);


        /*
        bool newAttack = Input.GetButton(btnA);
        if (newAttack != lastAttack && newAttack == true)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        lastAttack = newAttack;
        */
        //attack = buttonA.OnPressed;
        defense = buttonLB.IsPressing;
        rb = buttonRB.OnPressed;
        //rt = buttonRT.OnPressed;
        lb = buttonLB.OnPressed;
        //lt = buttonLT.OnPressed;

    }
    /*
    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1f - 0.5f * input.y * input.y);
        output.y = input.y * Mathf.Sqrt(1f - 0.5f * input.x * input.x);
        return output;
    }
    */
}
