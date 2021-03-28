using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseFu 
{
    private string weaponDatabaseFilename = "weaponData";

    public readonly JSONObject weaponDataBase;

    public DataBaseFu()
    {
        TextAsset weaponContent = Resources.Load(weaponDatabaseFilename) as TextAsset;
        //print(myText.text);
        weaponDataBase = new JSONObject(weaponContent.text);
        
    }
}
