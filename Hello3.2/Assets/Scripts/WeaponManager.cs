using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface
{
    //public ActorManager am;
    private Collider weaponColL;
    private Collider weaponColR;

    private GameObject whL;
    private GameObject whR;

    public WeaponController wcL;
    public WeaponController wcR;


    private void Start()
    {

        try
        {
            whR = transform.DeepFind("WeaponHandleR").GetComponent<WeaponController>().gameObject;
            weaponColR = whR.GetComponentInChildren<Collider>();
            wcR = BindWeaponController(whR);
        }
        catch (System.Exception)
        {
        }
        try
        {
            whL = transform.DeepFind("WeaponHandleL").GetComponent<WeaponController>().gameObject;
            weaponColL = whL.GetComponentInChildren<Collider>();
            wcL = BindWeaponController(whL);

        }
        catch (System.Exception)
        {
        }

    }
    

    public WeaponController BindWeaponController( GameObject go)
    {
        WeaponController tempInst;

        tempInst = go.GetComponent<WeaponController>();
        if(tempInst == null)
        {
            tempInst = go.AddComponent<WeaponController>();
        }
        tempInst.wm = this;
        return tempInst;
    }

    public void WeaponEnable()
    {
        weaponColL.enabled = true;
        weaponColR.enabled = true;
    }

    public void WeaponDisable()
    {
        weaponColL.enabled = false;
        weaponColR.enabled = false;
        if (am.ac.CheckStateTag("attackL"))
        {

        }
        if (am.ac.CheckStateTag("attackR"))
        {
        }
    }





}
