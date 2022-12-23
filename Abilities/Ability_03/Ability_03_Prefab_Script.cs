using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_03_Prefab_Script : MonoBehaviour
{
    private GameObject master_manager;
    private Transform on_hit_sfx;

    private List<GameObject> affected_targets;


    private int damage;
    private float max_size;
    private float stay_duration;
    private Vector3 scale_change;

    private int a_second_passed = 1;

    public void Setup(int damage,float starting_size, float scale_change, float max_size, float stay_duration, GameObject master_manager, Transform on_hit_sfx)
    {
        //Set important variables
        this.damage = damage;
        this.master_manager = master_manager;
        this.max_size = max_size;
        this.stay_duration = stay_duration;
        this.on_hit_sfx = on_hit_sfx;

        transform.localScale = new Vector3(starting_size,starting_size,0f);
        this.scale_change = new Vector3(scale_change, scale_change,0f);

        affected_targets = new List<GameObject>();
        a_second_passed = 1;

    }

    public void Update()
    {
        if (transform.localScale.x < max_size && transform.localScale.y < max_size)
        {
            transform.localScale += scale_change;
        }
        else
        {
            stay_duration -= 0.01f;

            if(stay_duration <= 0.00f)
            {
                Destroy(this.gameObject);
            };

        }

         if (Time.time >= a_second_passed)
         {
            a_second_passed = Mathf.FloorToInt(Time.time) + 1;
            StartCombat();
         }

    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player Character"))
        {
            string source_owner = this.gameObject.GetComponent<Object_Information>().player_owner;
            string target_owner = collider.GetComponent<Object_Information>().player_owner;

            if(target_owner != source_owner)
            {
                affected_targets.Add(collider.gameObject);
            };

        }
        else if (collider.CompareTag("Game Border"))
        {
            Destroy(this.gameObject);
        };
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player Character"))
        {
            string source_owner = this.gameObject.GetComponent<Object_Information>().player_owner;
            string target_owner = collider.GetComponent<Object_Information>().player_owner;

            if (target_owner != source_owner)
            {
                if(affected_targets.Contains(collider.gameObject))
                {
                    affected_targets.Remove(collider.gameObject);
                };
            };

        }
    }

    void StartCombat()
    {
        string source_owner = this.gameObject.GetComponent<Object_Information>().player_owner;
        string target_owner;

        if (master_manager != null)
        {
            foreach (GameObject target in affected_targets)
            {
                Instantiate(on_hit_sfx, target.transform.position, Quaternion.identity);
                target_owner = target.GetComponent<Object_Information>().player_owner;
                master_manager.GetComponent<BattleManager_Combat>().Start_BasicCombat(source_owner, target_owner, damage);
            }

        };
    }
    
}