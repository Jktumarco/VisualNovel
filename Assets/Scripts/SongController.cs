using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongController : MonoBehaviour
{
    public static SongController S;


    public void Play(AudioSource audioSource )
    {
        audioSource.Play();
    }

    public void PlayDelay(AudioSource audioSource, float delay)
    {
        audioSource.PlayDelayed(delay);
    }

    public void Stop(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    public void Mute(AudioSource audioSource, bool mute)
    {
        audioSource.mute = mute;
    }


    public float SongTime(AudioSource audioSource)
    {
        return audioSource.clip.length;
    }

    public float TimeToFinal(AudioSource audioSource)
    {
        var allTime = audioSource.clip.length;
        var nowTime = audioSource.time;
        return allTime - nowTime;
    }

    public void MultitrackPlay( List<AudioSource> audioSource)
    {
        foreach (var item in audioSource)
        {
            item.Stop();
        }
    }

    public void MultitrackStop(List<AudioSource> audioSource)
    {
        foreach (var item in audioSource)
        {
            item.Play();
        }
    }


}
