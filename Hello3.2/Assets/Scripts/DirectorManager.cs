using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class DirectorManager : IActorManagerInterface
{
    PlayableDirector pd;

    [Header("===== Timeline Asset =====")]
    public TimelineAsset frontStab;
    public TimelineAsset openBox;

    [Header("===== Timeline Settings =====")]
    public ActorManager attacker;
    public ActorManager victim;

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    public void PlayOnStab(string timelineName, ActorManager attacker, ActorManager victim)
    {
        if (pd.state == PlayState.Playing)
        {
            return;
        }

        if (timelineName == "frontStab")
        {
            pd.playableAsset = Instantiate(frontStab);

            TimelineAsset timeline = pd.playableAsset as TimelineAsset;

            foreach (var track in timeline.GetOutputTracks())
            {
                if (track.name == "Attacker Script")
                {
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = clip.asset as MySuperPlayableClip;
                        MySuperPlayableBehaviour mybehav = myClip.template;
                        mybehav.myfloat = 66f;
                        myClip.am.exposedName = System.Guid.NewGuid().ToString(); //most important UNITY旺季初始化
                        pd.SetReferenceValue(myClip.am.exposedName, attacker);
                        pd.SetGenericBinding(track, attacker);
                    }


                }
                else if (track.name == "Victim Script")
                {
                    foreach (var clip in track.GetClips())
                    {
                        print(clip.displayName);
                        MySuperPlayableClip myClip = clip.asset as MySuperPlayableClip;
                        MySuperPlayableBehaviour mybehav = myClip.template;
                        mybehav.myfloat = 77f;
                        myClip.am.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(myClip.am.exposedName, victim);
                        print(myClip.am.exposedName);
                        pd.SetGenericBinding(track, victim);
                    }
                }
            }
            pd.Evaluate();
            pd.Play();
        }
        else if (timelineName == "openBox")
        {
            pd.playableAsset = Instantiate(openBox);

            TimelineAsset timeline = pd.playableAsset as TimelineAsset;

            foreach (var track in timeline.GetOutputTracks())
            {
                if (track.name == "Player Script")
                {
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = clip.asset as MySuperPlayableClip;
                        MySuperPlayableBehaviour mybehav = myClip.template;
                        mybehav.myfloat = 66f;
                        myClip.am.exposedName = System.Guid.NewGuid().ToString(); //most important UNITY旺季初始化
                        pd.SetReferenceValue(myClip.am.exposedName, attacker);
                        pd.SetGenericBinding(track, attacker);
                    }


                }
                else if (track.name == "Box Script")
                {
                    foreach (var clip in track.GetClips())
                    {
                        print(clip.displayName);
                        MySuperPlayableClip myClip = clip.asset as MySuperPlayableClip;
                        MySuperPlayableBehaviour mybehav = myClip.template;
                        mybehav.myfloat = 77f;
                        myClip.am.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(myClip.am.exposedName, victim);
                        print(myClip.am.exposedName);
                        pd.SetGenericBinding(track, victim);
                    }
                }
            }
            pd.Evaluate();
            pd.Play();

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayOnStab("frontStab", attacker,victim);

        }
            
    }

}
