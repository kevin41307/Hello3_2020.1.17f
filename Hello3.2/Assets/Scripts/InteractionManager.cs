using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IActorManagerInterface
{
    private CapsuleCollider interCol;
    public  List<EventCasterManager> overlapEcastms;
    private void Start()
    {
        interCol = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {

        EventCasterManager ecast = other.GetComponent<EventCasterManager>();
        if (ecast == null)
            return;
        if (overlapEcastms.Contains(ecast))
        {
            return;
        }
        else
        {
            overlapEcastms.Add(ecast);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EventCasterManager ecast = other.GetComponent<EventCasterManager>();
        overlapEcastms.Remove(ecast);

    }


    private void OnTriggerStay(Collider other)
    {
        //print(other.name);
        EventCasterManager[] ecastms = other.GetComponents<EventCasterManager>();
        foreach (var cast in ecastms)
        {
            //print(cast.eventName);
        }
    }
}
