using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    private static GameManager instance;
    private WeaponFactory weaponFact;
    private DataBaseFu database;

    void Awake()
    {
        CheckSingle();
        CheckGameObject();
        database = new DataBaseFu();
        weaponFact = new WeaponFactory(database);


    }

    private void Start()
    {
        //weaponFact.CreateWeapon("Sword", transform);
    }



    void CheckSingle()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            return;
        }
        Destroy(this);
    }

    void CheckGameObject()
    {
        if (tag == "GM")
        {
            return;
        }
        else
        {
            Destroy(this);
        }
    }

}
