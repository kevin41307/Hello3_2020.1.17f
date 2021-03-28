using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManagerInterface
{
    public float HP = 25;
    public float HPMax = 25;
    //public ActorManager am;

    [Header("1st order state flags")]
    public bool isGround = false;
    public bool isRoll = false;
    public bool isJump = false;
    public bool isJab = false;
    public bool isFall = false;
    public bool isAttack = false;
    public bool isHit = false;
    public bool isDie = false;
    public bool isBlocked = false;
    public bool isDefense = false;

    [Header("2nd order state flags")]
    public bool isAllowDefense;
    public void Start()
    {
        HP = HPMax;
    }

    private void Update()
    {
        if (am.ac.isDummy) return;

        isGround = am.ac.CheckState("Idle - Walk");
        isRoll = am.ac.CheckState("roll");
        isJump = am.ac.CheckState("jump");
        isJab = am.ac.CheckState("jab");
        isFall = am.ac.CheckState("fall");
        isAttack = am.ac.CheckStateTag("attackR") || am.ac.CheckStateTag("attackL");
        isHit = am.ac.CheckState("hit");
        isDie = am.ac.CheckState("die");
        isBlocked = am.ac.CheckState("blocked");
        isDefense = am.ac.CheckState("defense1h", "Defense Layer");

    }

    public void AddHP(float value)
    {

        HP += HP + value;
        HP = Mathf.Clamp(HP, 0, HPMax);

        
        //解鎖相機攝像頭
    }
}
