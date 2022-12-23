using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class Sound_Manager
{
    
    public static void Play_Sound(string sound_name)
    {
        Sound_Player(sound_name,false);
    }

    public static void Play_Sound(string sound_name, bool dontdestroyonload)
    {
        Sound_Player(sound_name, dontdestroyonload);
    }

    public static void Play_Sound(string sound_name, Vector3 position)
    {
        Sound_Player_Position(sound_name, position);
    }


    public static void Play_SceneLoadSound(string sound_name)
    {
        if (GameObject.Find(sound_name) != null)
        {
            Object.Destroy(GameObject.Find(sound_name));
            Play_Sound(sound_name);

        }
    }

    private static void Sound_Player(string sound_name, bool dontdestroyonload)
    {
        GameObject sound_game_object = new GameObject(sound_name);
        AudioSource audio_source = sound_game_object.AddComponent<AudioSource>();
        AudioClip audio_clip = Resources.Load<AudioClip>("Audio/Sounds/" + sound_name);

        audio_source.volume = SaveLoad_Data.settings_sound_volume;
        audio_source.PlayOneShot(audio_clip);

        Object.Destroy(sound_game_object, audio_clip.length);

        if(dontdestroyonload)
        {
            Object.DontDestroyOnLoad(sound_game_object);
        };
    }

    private static void Sound_Player_Position(string sound_name, Vector3 position)
    {
        GameObject sound_game_object = new GameObject(sound_name);
        sound_game_object.transform.position = position;
        AudioSource audio_source = sound_game_object.AddComponent<AudioSource>();
        AudioClip audio_clip = Resources.Load<AudioClip>("Audio/Sounds/" + sound_name);

        //Settings
        audio_source.volume = SaveLoad_Data.settings_sound_volume;
        audio_source.maxDistance = 0.3f;
        audio_source.spatialBlend = 1f;
        audio_source.rolloffMode = AudioRolloffMode.Logarithmic;
        audio_source.dopplerLevel = 0.001f;

        audio_source.PlayOneShot(audio_clip);

        Object.Destroy(sound_game_object, audio_clip.length);
    }
}