using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_09_Manager : MonoBehaviour
{
    //Get the prefab because getting it from
    //the folder might cause lag and takes too long
    public Transform ability_caster_prefab;

    //Get the prefab because getting it from
    //the folder might cause lag and takes too long
    public Transform ability_projectile_prefab;

    //Get the prefab because getting it from
    //the folder might cause lag and takes too long
    public Transform on_hit_sfx;

    //Get the master manager object for 
    //the combat manager script
    public GameObject master_manager;

    //Get the prefab because getting it from
    //the folder might cause lag and takes too long
    public GameObject user_character;
    public GameObject enemy_character;

    public void Setup(GameObject caster)
    {
        string owner = caster.GetComponent<Object_Information>().player_owner;

        //Default stats
        int damage = 10;
        int amount = 10;
        float move_speed = 50f;

        GameObject target_object = null;

        if (owner == "user")
        {
            target_object = enemy_character;
        } 
        else if (owner == "enemy")
        {
            target_object = user_character;
        };

        //Get points
        Vector2 target_direction = caster.transform.position - target_object.transform.position;

        //Get angle
        float raw_angle = Mathf.Atan2(target_direction.y, target_direction.x) * Mathf.Rad2Deg;
        if (raw_angle < 0) { raw_angle += 360; };
        float move_angle = raw_angle;

        //Get the reverse angle
        move_angle = move_angle + 180 % 360;

        //Create projectile
        Vector2 caster_position = caster.transform.position;
        Transform created_prefab = Instantiate(ability_caster_prefab, caster_position, Quaternion.identity);


        //Set prefab data
        created_prefab.GetComponent<Ability_09_Periodic_Caster_Script>().Setup(damage, amount, move_angle, move_speed, owner, caster, master_manager, ability_projectile_prefab, on_hit_sfx);
        created_prefab.GetComponent<Object_Information>().player_owner = owner;
    }
}
