using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_06_Prefab_Script : MonoBehaviour
{
    private GameObject master_manager;
    private GameObject caster;

    private string owner;
    private int duration;
    private int multiplier;

    private float caster_original_speed;
    private float new_speed;
    private int duration_count;
    private int a_second_passed = 1;

    public void Setup(int duration,GameObject master_manager, GameObject caster, string owner, int multiplier)
    {
        //Set important variables
        this.owner = owner;
        this.caster = caster;
        this.master_manager = master_manager;
        this.caster = caster;
        this.duration = duration;
        this.multiplier = multiplier;

        //Change projectile to desired rotation
        transform.eulerAngles = new Vector3(0,0,0);

        //Get original speed
        if(owner == "user")
        {
            caster_original_speed = this.master_manager.GetComponent<BattleManager_Main>().user_movespeed;
            new_speed = caster_original_speed * multiplier;
        }
        else if (owner == "enemy")
        {
            caster_original_speed = this.master_manager.GetComponent<BattleManager_Main>().enemy_movespeed;
            new_speed = caster_original_speed * multiplier;
        }

        //Make sure the starting count
        duration_count = 0;

        //Set second
        a_second_passed = 1;

    }

    public void Update()
    {
        Vector3 caster_size = caster.GetComponent<Renderer>().bounds.size;

        transform.position = caster.transform.position;
        transform.localScale = caster_size;

        //Increase speed
        if (owner == "user")
        {
            master_manager.GetComponent<BattleManager_Main>().user_movespeed = new_speed;
        }
        else if (owner == "enemy")
        {
            master_manager.GetComponent<BattleManager_Main>().enemy_movespeed = new_speed;
        }

        if (Time.time >= a_second_passed)
        {
            a_second_passed = Mathf.FloorToInt(Time.time) + 1;
            duration_count += 1;

            Sound_Manager.Play_Sound("ability_06_cast_sound", caster.transform.position);
        }

        if (duration_count >= duration)
        {
            if (owner == "user")
            {
                master_manager.GetComponent<BattleManager_Main>().user_movespeed = caster_original_speed;
            }
            else if (owner == "enemy")
            {
                master_manager.GetComponent<BattleManager_Main>().enemy_movespeed = caster_original_speed;
            }

            Destroy(this.gameObject);
        };
    }
}