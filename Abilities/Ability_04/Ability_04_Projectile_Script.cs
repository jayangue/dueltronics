using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_04_Projectile_Script : MonoBehaviour
{
    private GameObject master_manager;
    private GameObject caster;
    private Transform on_hit_sfx;

    private float orbit_speed;
    private float orbit_distance;
    private float move_rotation;
    private int damage;
    private int orbit_max;

    private float orbit_angle;
    private int orbit_count;
    private List<GameObject> affected_targets;

    public void Setup(int damage,float orbit_speed, float orbit_distance, float move_rotation, int orbit_max, GameObject master_manager, GameObject caster, Transform on_hit_sfx)
    {
        //Set important variables
        this.master_manager = master_manager;
        this.caster = caster;

        //Adjustable variables
        this.damage = damage;
        this.orbit_speed = orbit_speed;
        this.orbit_distance = orbit_distance;
        this.move_rotation = move_rotation;
        this.orbit_max = orbit_max;
        this.on_hit_sfx = on_hit_sfx;

        //Standard variables
        affected_targets = new List<GameObject>();
        this.orbit_angle = 0f;
        this.orbit_count = 0;

        //Set move angle
        transform.eulerAngles = new Vector3(0, 0, orbit_angle);

    }

    public void Update()
    {
        if (orbit_angle < 360f)
        {
            orbit_angle += orbit_speed * Time.deltaTime;
        }
        else if (orbit_angle >= 360f)
        {
            orbit_count += 1;
            orbit_angle = 0f;
            affected_targets = new List<GameObject>();

            if (orbit_count >= orbit_max)
            {
                Destroy(this.gameObject);
            };
        };

        var radians = (Mathf.PI / 180) * (orbit_angle);

        var caster_position = caster.transform.position;
        var new_position = new Vector3(0f, 0f, 0f);

        new_position.x = caster_position.x + (orbit_distance * Mathf.Cos(radians));
        new_position.y = caster_position.y + (orbit_distance * Mathf.Sin(radians));
        new_position.z = caster_position.z;

        this.transform.position = new_position;
        this.transform.Rotate(0, 0, move_rotation);

    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player Character"))
        {
            string source_owner = this.gameObject.GetComponent<Object_Information>().player_owner;
            string target_owner = collider.GetComponent<Object_Information>().player_owner;

            if(target_owner != source_owner)
            {
                if (!affected_targets.Contains(collider.gameObject))
                {
                    Instantiate(on_hit_sfx, collider.transform.position, Quaternion.identity);
                    Sound_Manager.Play_Sound("sfx_sound_hit", collider.transform.position);

                    affected_targets.Add(collider.gameObject);
                    StartCombat(source_owner, target_owner);
                };
            };

        }
        else if (collider.CompareTag("Game Border"))
        {
            Destroy(this.gameObject);
        };
    }

    void StartCombat(string source_owner,string target_owner)
    {
        if(master_manager != null)
        {
            master_manager.GetComponent<BattleManager_Combat>().Start_BasicCombat(source_owner,target_owner,damage);
        };
    }
}