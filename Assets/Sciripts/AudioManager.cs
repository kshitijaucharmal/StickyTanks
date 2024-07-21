using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }
    // void Start(){
    //     Play("Theme");
    // }
    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null){
            s.source.Play();
            return;
        }
        Debug.LogWarning("Sound: " + name + " not found!");

    }
    public void Stop(String name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null){
            s.source.Stop();
            return;
        }
        Debug.LogWarning("Sound: " + name + " not found!");
    }
}