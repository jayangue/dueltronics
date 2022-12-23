using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Manager : MonoBehaviour
{
    public GameObject music_manager;
    private static Music_Manager music_manager_instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if(music_manager_instance == null)
        {
            music_manager_instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        music_manager.GetComponent<AudioSource>().volume = SaveLoad_Data.settings_music_volume;
    }
}
