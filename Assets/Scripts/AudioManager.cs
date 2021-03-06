using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private AudioSource track1;
    private AudioSource track2;
    private bool isPlayingTrack1;
    public AudioClip defaultTrack;

    public static AudioManager instance;
    public AudioSource lastClip;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        track1 = gameObject.AddComponent<AudioSource>();
        track2 = gameObject.AddComponent<AudioSource>();

        isPlayingTrack1 = true;

        lastClip = track1;

        SwapTrack(defaultTrack);
    }

    public void SwapTrack(AudioClip newClip)
    {
        if (isPlayingTrack1)
        {
            if (track1.clip == newClip)
            {
                //Same song
            }
            else
            {
                track2.clip = newClip;
                track2.loop = true;
                track2.volume = 0.25f;
                track2.PlayDelayed(1);
                lastClip = track2;
                Debug.Log(lastClip.name);
                track1.Stop();
            }
            
            
        }
        else
        {
            if (track2.clip == newClip)
            {
                //Same song
            }
            else
            {
                track1.clip = newClip;
                track1.loop = true;
                track1.volume = 0.25f;
                track1.PlayDelayed(1);
                lastClip = track1;
                Debug.Log(lastClip.name);
                track2.Stop();
            }
            
            
        }
        isPlayingTrack1 = !isPlayingTrack1;
    }

    public void ReturnToDefault()
    {
        SwapTrack(defaultTrack);
    }

    public void PlayLastTrack()
    {
        lastClip.Play();
    }

    public void StopMusic()
    {
        if (track1.isPlaying)
        {
            track1.Stop();
        }
        if (track2.isPlaying)
        {
            track2.Stop();
        }
    }
}
