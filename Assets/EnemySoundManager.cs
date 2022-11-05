using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : SoundManager
{
    public AudioSource collisionSource;
    public AudioSource vocalSource;

    [Header("Sounds")]
    public string deathSound;
    public string[] talk;

    public void Talk(string audio)
    {
        var Clip = Resources.Load("Sounds/" + talk[Random.Range(0,talk.Length)]) as AudioClip;
        if(!vocalSource.isPlaying)
        {
            vocalSource.PlayOneShot(Clip);
        }
    }

    public void Die(string audio)
    {
        var Clip = Resources.Load("Sounds/" + deathSound) as AudioClip;
        vocalSource.PlayOneShot(Clip);
    }
    public void Collide(string audio)
    {
        var Clip = Resources.Load("Sounds/" + audio) as AudioClip;
        if(!collisionSource.isPlaying)
        {
            collisionSource.PlayOneShot(Clip);
        }
    }
}