using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public ActorController ac;

    [Header("Auto generate if null")]
    public WeaponManager wm;
    public StateManager sm;
    public BattleManager bm;
    public DirectorManager dm;
    public InteractionManager im;

    
    GameObject test;
    private void Awake()
    {
        GameObject sensor = null;
        try
        {
            sensor = transform.DeepFind("Sensor").gameObject;
        }
        catch (System.Exception)
        {
            throw;
        }

        ac = GetComponent<ActorController>();
        
        wm = Bind<WeaponManager>(gameObject);
        sm = Bind<StateManager>(gameObject);
        dm = Bind<DirectorManager>(gameObject);
        bm = Bind<BattleManager>(sensor);
        im = Bind<InteractionManager>(sensor);
        ac.OnAction += Action1;
        ac.OnAction += Action2;
    }

    private T Bind<T>(GameObject go) where T : IActorManagerInterface
    {
        if( go == null )
        {
            return null;
        }
        T tempInstance;

        tempInstance = go.GetComponent<T>();
        if( tempInstance == null )
        {
            tempInstance = go.AddComponent<T>();
        }
        tempInstance.am = this;
        return tempInstance;
    }

    public void TryDoDamage()
    {
        if(sm.isDefense)
        {

        }
        else
        {
            if(sm.HP <= 0)
            {
                if (sm.HP > 0)
                {
                    Hit();
                }
                else
                {
                    Die();
                }
            }
        }
    }


    public void DoAction()
    {
        if(im.overlapEcastms.Count != 0)
        {
            if (im.overlapEcastms[0].active == true)
            {
                if (im.overlapEcastms[0].eventName == "frontStab")
                    dm.PlayOnStab("frontStab", this, im.overlapEcastms[0].am);
                else if (im.overlapEcastms[0].eventName == "openBox")
                {
                    im.overlapEcastms[0].active = false;
                    dm.PlayOnStab("openBox", this, im.overlapEcastms[0].am);
                }

            }

        }
        
    }
    public void Action1()
    {
        print("action1");
    }
    public void Action2()
    {
        print("action2");
    }
    public void Blocked()
    {
        ac.IssueTrigger("blocked");
    }
    
    public void Hit()
    {
        ac.IssueTrigger("hit");
    }

    public void Die()
    {
        ac.IssueTrigger("die");
        ac.pi.inputEnable = false;
    }

    public void LockUnLockActorController(bool value)
    {
        ac.SetBool("lock", value);
    }


}
