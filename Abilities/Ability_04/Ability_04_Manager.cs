using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_04_Manager : MonoBehaviour
{
    //Get the prefab because getting it from
    //the folder might cause lag and takes too long
    public Transform ability_prefab;

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
        float orbit_speed = 400f;
        float orbit_distance = 5f;
        float move_rotation = 20f;
        int orbit_max = 3;
        int damage = 75;

        // Create projectile
        Vector2 caster_position = caster.transform.position;
        Transform created_prefab = Instantiate(ability_prefab, caster_position, Quaternion.identity);


        //Set prefab data
        created_prefab.GetComponent<Ability_04_Projectile_Script>().Setup(damage,orbit_speed,orbit_distance,move_rotation,orbit_max,master_manager,caster,on_hit_sfx);
        created_prefab.GetComponent<Object_Information>().player_owner = owner;

        Sound_Manager.Play_Sound("ability_04_cast_sound", caster.transform.position);
    }

}