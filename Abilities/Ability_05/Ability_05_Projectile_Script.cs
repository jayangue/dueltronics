using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_05_Projectile_Script : MonoBehaviour
{
    private GameObject master_manager;
    private Transform on_hit_sfx;

    private int damage;
    private float move_angle;
    private float move_speed;
    private float move_rotation;
    private float distance_limit;

    private Vector2 starting_position;

    public void Setup(int damage,float move_angle, float move_rotation, float move_speed, float distance_limit, GameObject master_manager, Transform on_hit_sfx)
    {
        //Set important variables
        this.damage = damage;
        this.master_manager = master_manager;
        this.move_angle = move_angle;
        this.move_speed = move_speed;
        this.move_rotation = move_rotation;
        this.distance_limit = distance_limit;
        this.on_hit_sfx = on_hit_sfx;


        //Set move angle
        transform.eulerAngles = new Vector3(0, 0, move_angle);

        //Save starting position for distance calculation
        starting_position = transform.position;

    }

    public void Update()
    { 
        var new_position = transform.position;
        var radians = (Mathf.PI / 180) * (move_angle);

        new_position.x += (move_speed * Mathf.Cos(radians)) * Time.deltaTime;
        new_position.y += (move_speed * Mathf.Sin(radians)) * Time.deltaTime;

        transform.position = new_position;
        transform.Rotate(0, 0, move_rotation);

        if(Vector2.Distance(starting_position,transform.position) >= distance_limit )
        {
            Destroy(this.gameObject);
        };
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player Character"))
        {
            string source_owner = this.gameObject.GetComponent<Object_Information>().player_owner;
            string target_owner = collider.GetComponent<Object_Information>().player_owner;

            if(target_owner != source_owner)
            {
                Instantiate(on_hit_sfx, collider.transform.position, Quaternion.identity);
                Sound_Manager.Play_Sound("sfx_sound_explosion", collider.transform.position);

                StartCombat(source_owner,target_owner);
                Destroy(this.gameObject);
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