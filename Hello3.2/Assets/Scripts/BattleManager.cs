using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManagerInterface
{
    private CapsuleCollider defCol;

    private void Start()
    {
        defCol = GetComponent<CapsuleCollider>();

        defCol.center = new Vector3(0, 3.5f, 0);
        defCol.height = 7f;
        defCol.radius = 0.8f;
        defCol.isTrigger = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Weapon")
        {
            am.TryDoDamage();
        }
    }

}
