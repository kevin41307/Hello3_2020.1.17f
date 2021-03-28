using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput
{
    [Header("===== Keyboard settings =====")]
    string keyUp = "w";
    string keyDown = "s";
    string keyLeft = "a";
    string keyRight = "d";

    /*
    public string keyA; //sqaure
    public string keyB; // X
    public string keyC; // O
    public string keyD; // triangle
    */
    string keyJump = "space";
    string keyRun = "left shift";

    public string JoykeyUp; // upArrow
    public string JoykeyDown; // downArrow
    public string JoykeyRight; // rightArrow
    public string JoykeyLeft; // leftArrow



    MyButton buttonUp = new MyButton();
    MyButton buttonDown = new MyButton();
    MyButton buttonRight = new MyButton();
    MyButton buttonLeft = new MyButton();
    MyButton buttonM0 = new MyButton();
    MyButton buttonM1 = new MyButton();
    MyButton buttonJump = new MyButton();
    MyButton buttonRun = new MyButton();

    public MyButton buttonLB = new MyButton();
    //public MyButton buttonLT = new MyButton();
    public MyButton buttonRB = new MyButton();
    //public MyButton buttonRT = new MyButton();





    private void Update()
    {
        buttonUp.Tick(Input.GetKey(keyUp));
        buttonDown.Tick(Input.GetKey(keyDown));
        buttonRight.Tick(Input.GetKey(keyRight));
        buttonLeft.Tick(Input.GetKey(keyLeft));
     
        buttonM0.Tick(Input.GetMouseButton(0));
        buttonM1.Tick(Input.GetMouseButton(1));
        buttonJump.Tick(Input.GetKey(keyJump));
        buttonRun.Tick(Input.GetKey(keyRun));


        /*
        Jup = (Input.GetKey(JoykeyUp) ? 1f : 0) - (Input.GetKey(JoykeyDown) ? 1f : 0);
        Jright = (Input.GetKey(JoykeyRight) ? 1f : 0) - (Input.GetKey(JoykeyLeft) ? 1f : 0);
        */

        targetDup = (buttonUp.IsPressing ? 1f : 0) - (buttonDown.IsPressing ? 1f : 0) ;
        targetDright = (buttonRight.IsPressing ? 1f : 0) - (buttonLeft.IsPressing ? 1f : 0);

        

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

        run = (buttonRun.IsPressing && !buttonRun.IsDelaying) || buttonRun.IsExtending;
        jump = buttonJump.OnPressed && buttonRun.IsExtending; // jump ©µ«á¦A­×
        roll = buttonJump.OnReleased;
        defense = buttonM1.IsPressing;

        rb = buttonM0.OnPressed;
        lb = buttonM1.OnPressed;



        /*
        bool newJump = Input.GetKey(keyB);
        if (newJump != lastJump && newJump == true)
        {
            jump = true;
        }
        else 
        {
            jump = false;
        }
        lastJump = newJump;


        bool newAttack = Input.GetKey(keyC);
        if (newAttack != lastAttack && newAttack == true)
        {
            rb = true;
        }
        else
        {
            rb = false;
        }
        lastAttack = newAttack;
        */



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
