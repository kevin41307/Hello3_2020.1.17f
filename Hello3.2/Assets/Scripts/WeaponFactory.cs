using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory 
{
    private DataBaseFu weaponDB;

    public WeaponFactory(DataBaseFu _weaponDB)
    {
        weaponDB = _weaponDB;
    }

    public GameObject CreateWeapon(string weaponName, Vector3 pos, Quaternion rot)
    {
        GameObject prefab = Resources.Load(weaponName) as GameObject;
        GameObject go = GameObject.Instantiate(prefab, pos, rot);

        WeaponData wdata = go.AddComponent<WeaponData>();
        wdata.ATK = weaponDB.weaponDataBase[weaponName]["ATK"].f;

        return go;
    }

    public bool CreateWeapon(string weaponName, string side, WeaponManager wm)
    {
        WeaponController wc;
        if (side == "L")
        {
            wc = wm.wcL;
        }
        else if (side == "R")
        {
            wc = wm.wcR;
        }
        else
        {
            return false;
        }


        GameObject prefab = Resources.Load(weaponName) as GameObject;
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(wm.transform);

        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        WeaponData wdata = go.AddComponent<WeaponData>();
        wdata.ATK = weaponDB.weaponDataBase[weaponName]["ATK"].f;

        return true;
    }

}
