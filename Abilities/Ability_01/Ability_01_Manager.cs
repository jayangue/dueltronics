using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_01_Manager : MonoBehaviour
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
        int damage = 100;
        float move_speed = 10f;
        float move_rotation = 270f; //The correct model rotation
        float[] move_angles = new float[] { 90f, 270f, 180f, 0f };  //north=90, south=270, west=180 east=0

        for (int i = 0; i < move_angles.Length; i++)
        {
            //Create projectile
            Vector2 caster_position = caster.transform.position;
            Transform created_prefab = Instantiate(ability_prefab, caster_position, Quaternion.identity);
            

            //Set prefab data
            created_prefab.GetComponent<Ability_01_Projectile_Script>().Setup(damage,move_angles[i], move_speed, move_rotation, master_manager,on_hit_sfx);
            created_prefab.GetComponent<Object_Information>().player_owner = owner;
        }

        Sound_Manager.Play_Sound("ability_01_cast_sound",caster.transform.position);
    }

}