using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioClip freeze;
    public AudioClip ice;
    public AudioClip button;
    public AudioClip powerup;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip wilhelm;
    public AudioClip bomb;
    public AudioClip music;


    List<AudioClip> clips = new List<AudioClip>();
    Dictionary<AudioClip, AudioSource> sources = new Dictionary<AudioClip, AudioSource>();


    private void Awake() 
	{
		// MAKE INSTANCE
		if      (instance == null) { instance = this; }
		else if (instance != this) { Destroy(gameObject); }
    }

    private void Start() 
    {
        clips.Add(freeze);
        clips.Add(ice);
        clips.Add(button);
        clips.Add(powerup);
        clips.Add(win);
        clips.Add(lose);
        clips.Add(wilhelm);
        clips.Add(bomb);
        clips.Add(music);

        foreach(AudioClip c in clips)
        {
            AudioSource source = this.gameObject.AddComponent<AudioSource>();
            source.clip = c;
            sources.Add(c, source);

            if(c == bomb){ source.volume = 0.6f; }
            if(c == freeze){ source.volume = 0.7f; }
            if(c == music){ source.volume = 0.04f; source.loop = true; }
        }

        PlayClip(music);
    }

    public void PlayClip(AudioClip c)
    {
        sources[c].Play();
    }

    public IEnumerator PlayClipDelayed(AudioClip c, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayClip(c);
        yield return null;
    }
}
