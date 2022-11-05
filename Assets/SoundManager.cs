using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioSource audioSource;
    public void PlaySound(string audio)
    {
        var Clip = Resources.Load("Sounds/" + audio) as AudioClip;
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(Clip);
        }
    }
    public void Step(AnimationEvent animationEvent)
    {
        var Clip = Resources.Load("Sounds/Footsteps/foot-soil-" + Random.Range(1,8).ToString()) as AudioClip;
        if(!footstepSource.isPlaying)
            footstepSource.PlayOneShot(Clip,Random.Range(.15f,.225f));
    }

    public void Sneak(AnimationEvent animationEvent)
    {
        var Clip = Resources.Load("Sounds/Footstep_Dirt_0" + Random.Range(0,1).ToString()) as AudioClip;
        if(!footstepSource.isPlaying&& animationEvent.animatorClipInfo.weight > 0.5)
            footstepSource.PlayOneShot(Clip,Random.Range(.1f,.15f));
    }

    public void Roll(AnimationEvent animationEvent)
    {
        var Clip = Resources.Load("Sounds/Footsteps/foot-grass-r3") as AudioClip;
        if(!footstepSource.isPlaying &&animationEvent.animatorClipInfo.weight > 0.5)
            footstepSource.PlayOneShot(Clip,Random.Range(.4f,.7f));
    }
}
