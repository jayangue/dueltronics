using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_05_Manager : MonoBehaviour
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

        float caster_moving_angle;

        //Get caster current movement angle. If there is an error it means we target the AI
        try{    caster_moving_angle = caster.GetComponent<Player_Controller>().moving_angle;   }
        catch{  caster_moving_angle = caster.GetComponent<AI_Manager>().moving_angle;    }

        //Get the reverse angle
        caster_moving_angle = caster_moving_angle + 180 % 360;

        //Default stats
        int damage = 10;
        float distance_limit = 8f;
        float move_speed = 10f;
        float move_rotation = 5f;
        float[] move_angles = new float[] { -30f,-20f, -10f, 0f, 10f, 20f, 30f };  //north=90, south=270, west=180 east=0

        for (int i = 0; i < move_angles.Length; i++)
        {
            //Create projectile
            Vector2 caster_position = caster.transform.position;
            Transform created_prefab = Instantiate(ability_prefab, caster_position, Quaternion.identity);


            //Set prefab data
            float current_angle = caster_moving_angle + move_angles[i];
            created_prefab.GetComponent<Ability_05_Projectile_Script>().Setup(damage,current_angle, move_rotation, move_speed, distance_limit,master_manager,on_hit_sfx);
            created_prefab.GetComponent<Object_Information>().player_owner = owner;

            Sound_Manager.Play_Sound("ability_05_cast_sound", caster.transform.position);
        }
    }

}