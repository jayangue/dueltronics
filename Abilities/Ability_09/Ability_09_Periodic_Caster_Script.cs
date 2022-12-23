using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_09_Periodic_Caster_Script : MonoBehaviour
{
    private GameObject caster;
    private GameObject target_object;
    private GameObject master_manager;
    private Transform on_hit_sfx;
    private Transform projectile_prefab;

    private int damage;
    private int amount;
    private float move_speed;
    private float move_angle;
    private int projectile_count;
    private string owner;

    //Other variables
    private float temp_time;

    public void Setup(int damage, int amount, float move_angle, float move_speed, string owner, GameObject caster, GameObject master_manager, Transform projectile_prefab, Transform on_hit_sfx)
    {
        //Set important variables
        this.damage = damage;
        this.amount = amount;
        this.move_speed = move_speed;
        this.move_angle = move_angle;
        this.owner = owner;
        this.caster = caster;
        this.master_manager = master_manager;
        this.projectile_prefab = projectile_prefab;
        this.on_hit_sfx = on_hit_sfx;

        temp_time = 0;
        projectile_count = 0;

    }

    public void Update()
    {
       transform.position = caster.transform.position;

        temp_time += Time.deltaTime;
        if (temp_time > 0.3)
        {
            temp_time = 0.0f;
            projectile_count += 1;

            //Get the reverse angle
            float move_rotation = move_angle - 180 % 360;

            //Create projectile
            Transform created_prefab = Instantiate(projectile_prefab, caster.transform.position, Quaternion.identity);

            //Set prefab data
            created_prefab.GetComponent<Ability_09_Projectile_Script>().Setup(damage, move_angle, move_rotation, move_speed, master_manager, on_hit_sfx);
            created_prefab.GetComponent<Object_Information>().player_owner = owner;
        }

        if(projectile_count >= amount)
        {
            Destroy(this.gameObject);
        }

    }

  
}
