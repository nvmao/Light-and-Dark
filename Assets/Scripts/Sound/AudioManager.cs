using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] soundsEdit;

    Hashtable sounds = new Hashtable();

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach(Sound s in soundsEdit)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.source.pitch;
            s.source.loop = s.loop;

            sounds.Add(s.name, s);
        }
    }

    private void Start()
    {
        play("background1");
    }

    public void play(string name)
    {
        Sound s = (Sound)sounds[name];
        s.source.Play();
    }
    public void stop(string name)
    {
        Sound s = (Sound)sounds[name];
        s.source.Stop();
    }

    public void playBG()
    {
        Sound s = (Sound)sounds["gameOver"];
        if(s.source.isPlaying)
            s.source.Stop();

        Sound s2 = (Sound)sounds["background1"];
        s2.source.Play();
    }
}
