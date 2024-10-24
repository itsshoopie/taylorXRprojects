using DefaultCompany.Singleton;//Make sure namespace matches company name in Player settings.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{

    [Header("Background Music Tracks")]

    [SerializeField]
    private AudioClip[] tracks; //Select from list and eventually randomized
    private AudioSource audioSource;

    [Header("Events")]
    public Action onCurrentTrackEnded;

    public void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(ShuffleWhenStopsPlaying());
        ShuffleAndPlay();
    }

    public void ShuffleAndPlay(GameState gameState = GameState.Playing)//GameState comes from enum
    {
        if (tracks.Length > 0)
        {
            Debug.Log("Shuffle and play is working");

            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            audioSource.clip = tracks[UnityEngine.Random.Range(0,tracks.Length -1)];
            audioSource.Play();
        }
    }

    private IEnumerator ShuffleWhenStopsPlaying() //Play another track when one completes
    {
        while (true)
        {
            yield return new WaitUntil(() => !audioSource.isPlaying);
            ShuffleAndPlay();
            onCurrentTrackEnded?.Invoke(); //Invokes an action to anything listening
        }
    }

}
