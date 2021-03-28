using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public ActorManager am;
    public float myfloat;
    PlayableDirector pd;

    public override void OnPlayableCreate (Playable playable)
    {
        //pd = (PlayableDirector)playable.GetGraph().GetResolver();
    }
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
    }

    public override void OnGraphStop(Playable playable)
    {
        //pd.playableAsset = null;
    }
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        am.LockUnLockActorController(false);
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        am.LockUnLockActorController(true);
    }

}
