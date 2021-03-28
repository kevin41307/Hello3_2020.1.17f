using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class TesterDirector : MonoBehaviour
{
    PlayableDirector pd;

    Animator attacker;
    Animator receiver;



    private void Start()
    {
        pd = GetComponent<PlayableDirector>();    

    }

    void Update()
    {
        if(Input.GetKey(KeyCode.H))
        {
            foreach (var track in pd.playableAsset.outputs)
            {
                print(track.streamName);
                if( track.streamName == "Attack Animation" )
                {
                    pd.SetGenericBinding( track.sourceObject, attacker);
                }
            }
        }
    }

    void Restart()
    {
        pd.time = 0;
        pd.Stop();
        pd.Evaluate();
        pd.Play();
    }
}
