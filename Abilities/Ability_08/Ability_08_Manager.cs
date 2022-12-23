using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_08_Manager : MonoBehaviour
{
    //Get the prefab because getting it from
    //the folder might cause lag and takes too long
    public Transform ability_prefab;

    //Get the master manager object for 
    //the combat manager script
    public GameObject master_manager;

    public void Setup(GameObject caster)
    {
        string owner = caster.GetComponent<Object_Information>().player_owner;

        //Default stats
        int duration = 3;
        int health_regen = 50;

       //Create projectile
       Vector2 caster_position = caster.transform.position;
       Transform created_prefab = Instantiate(ability_prefab, caster_position, Quaternion.identity);
            

       //Set prefab data
       created_prefab.GetComponent<Ability_08_Prefab_Script>().Setup(duration,health_regen,master_manager,caster,owner);
       created_prefab.GetComponent<Object_Information>().player_owner = owner;

        Sound_Manager.Play_Sound("ability_08_cast_sound", caster.transform.position);
    }

}