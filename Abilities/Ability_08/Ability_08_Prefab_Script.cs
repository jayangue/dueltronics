using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_08_Prefab_Script : MonoBehaviour
{
    private GameObject master_manager;
    private GameObject caster;
    private Transform heal_sfx;

    private string owner;
    private int duration;
    private int health_regen;


    private int duration_count;
    private int a_second_passed = 1;

    public void Setup(int duration,int health_regen,GameObject master_manager, GameObject caster, string owner)
    {
        //Set important variables
        this.owner = owner;
        this.caster = caster;
        this.master_manager = master_manager;
        this.caster = caster;
        this.duration = duration;
        this.health_regen = health_regen;

        //Change projectile to desired rotation
        transform.eulerAngles = new Vector3(0,0,0);

        //Make sure the starting count
        duration_count = 0;

        //Set second
        a_second_passed = 1;

    }

    public void Update()
    {
        Vector3 caster_size = caster.GetComponent<Renderer>().bounds.size;
        Vector3 adjustment_size = new Vector3(-1f,-1f,0f);

        transform.position = caster.transform.position;
        transform.localScale = caster_size + adjustment_size;

        if (Time.time >= a_second_passed)
        {
            a_second_passed = Mathf.FloorToInt(Time.time) + 1;
            duration_count += 1;

            //Increase health
            if (owner == "user")
            {
                master_manager.GetComponent<BattleManager_Main>().user_current_health += health_regen;
            }
            else if (owner == "enemy")
            {
                master_manager.GetComponent<BattleManager_Main>().enemy_current_health += health_regen;
            }

            Sound_Manager.Play_Sound("ability_08_cast_sound", caster.transform.position);
        }

        if (duration_count >= duration)
        {
            Destroy(this.gameObject);
        };
    }
}