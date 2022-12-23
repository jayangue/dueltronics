using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_03_Manager : MonoBehaviour
{
    //Get the prefab because getting it from
    //the folder might cause lag and takes too long
    public Transform spell_prefab;

    //Get the prefab because getting it from
    //the folder might cause lag and takes too long
    public Transform on_hit_sfx;

    //Get the master manager object for 
    //the combat manager script
    public GameObject master_manager;

    public void Setup(GameObject caster)
    {
        string owner = caster.GetComponent<Object_Information>().player_owner;

        //Default stats
        int damage = 50;
        float starting_size = 0.3f;
        float scale_change = 0.03f;
        float max_size = 3f;
        float stay_duration = 3f;

        //Create projectile
        Vector2 caster_position = caster.transform.position;
        Transform created_prefab = Instantiate(spell_prefab, caster_position, Quaternion.identity);

        //Set prefab data
        created_prefab.GetComponent<Ability_03_Prefab_Script>().Setup(damage,starting_size,scale_change, max_size, stay_duration, master_manager,on_hit_sfx);
        created_prefab.GetComponent<Object_Information>().player_owner = owner;

        Sound_Manager.Play_Sound("ability_03_cast_sound", caster.transform.position);
    }

}