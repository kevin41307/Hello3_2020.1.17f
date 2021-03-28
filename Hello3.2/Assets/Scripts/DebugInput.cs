using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInput : MonoBehaviour
{
    public AIController_Paladin boss;
    // Update is called once per frame
    void Update()
    {
     
        
        if(Input.GetKeyDown(KeyCode.F1))
        {
            boss.gameObject.SetActive(true);
        }
    }
}
