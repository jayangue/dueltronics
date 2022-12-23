using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_07_Prefab_Script : MonoBehaviour
{
    private GameObject master_manager;
    private GameObject caster;

    private string owner;
    private int duration;
    private string status_name;

    private int duration_count;
    private int a_second_passed = 1;

    public void Setup(int duration,string status_name, GameObject master_manager, GameObject caster, string owner)
    {
        //Set important variables
        this.status_name = status_name;
        this.owner = owner;
        this.caster = caster;
        this.master_manager = master_manager;
        this.caster = caster;
        this.duration = duration;

        //Change projectile to desired rotation
        transform.eulerAngles = new Vector3(0,0,0);

        //Get original speed
        if(owner == "user")
        {
            if(!this.master_manager.GetComponent<BattleManager_Main>().user_status.Contains(status_name))
            {
                this.master_manager.GetComponent<BattleManager_Main>().user_status.Add(status_name);
            };
        }
        else if (owner == "enemy")
        {
            if (!this.master_manager.GetComponent<BattleManager_Main>().enemy_status.Contains(status_name))
            {
                this.master_manager.GetComponent<BattleManager_Main>().enemy_status.Add(status_name);
            };
        }

        //Make sure the starting count
        duration_count = 0;

        //Set second
        a_second_passed = 1;

    }

    public void Update()
    {
        Vector3 caster_size = caster.GetComponent<Renderer>().bounds.size;
        Vector3 increase_size = new Vector3(1.5f,1.5f,0f);

        transform.position = caster.transform.position;
        transform.localScale = caster_size + increase_size;

        if (Time.time >= a_second_passed)
        {
            a_second_passed = Mathf.FloorToInt(Time.time) + 1;
            duration_count += 1;
        }

        if (duration_count >= duration)
        {
            if (owner == "user")
            {
                if (this.master_manager.GetComponent<BattleManager_Main>().user_status.Contains(status_name))
                {
                    this.master_manager.GetComponent<BattleManager_Main>().user_status.Remove(status_name);
                };
            }
            else if (owner == "enemy")
            {
                if (this.master_manager.GetComponent<BattleManager_Main>().enemy_status.Contains(status_name))
                {
                    this.master_manager.GetComponent<BattleManager_Main>().enemy_status.Remove(status_name);
                };
            }

            Destroy(this.gameObject);
        };
    }
}