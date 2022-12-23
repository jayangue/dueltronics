using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AI_Abilities
{
    public static void Cast(Vector2 waypoint, GameObject enemy, GameObject caster, GameObject master_manager, int ability_position)
    {
        switch (ability_position)
        {
            case 1:
                if (master_manager.GetComponent<BattleManager_Main>().enemy_ability1_cd <= 0.0f)
                {
                    if(CheckAvailability(waypoint, enemy, caster,master_manager,SaveLoad_Data.enemy_ability1_id))
                    {
                        float max_cd = master_manager.GetComponent<BattleManager_Main>().enemy_ability1_max_cd;
                        master_manager.GetComponent<BattleManager_Main>().enemy_ability1_cd = max_cd;
                        master_manager.GetComponent<BattleManager_Ability>().Activate_Ability(SaveLoad_Data.enemy_ability1_id, caster);
                    };
                };
                break;

            case 2:
                if (master_manager.GetComponent<BattleManager_Main>().enemy_ability2_cd <= 0.0f)
                {
                    if (CheckAvailability(waypoint,enemy, caster, master_manager, SaveLoad_Data.enemy_ability2_id))
                    {
                        float max_cd = master_manager.GetComponent<BattleManager_Main>().enemy_ability2_max_cd;
                        master_manager.GetComponent<BattleManager_Main>().enemy_ability2_cd = max_cd;
                        master_manager.GetComponent<BattleManager_Ability>().Activate_Ability(SaveLoad_Data.enemy_ability2_id, caster);
                    };
                };
                break;

            case 3:
                if (master_manager.GetComponent<BattleManager_Main>().enemy_ability3_cd <= 0.0f)
                {
                    if (CheckAvailability(waypoint,enemy, caster, master_manager, SaveLoad_Data.enemy_ability3_id))
                    {
                        float max_cd = master_manager.GetComponent<BattleManager_Main>().enemy_ability3_max_cd;
                        master_manager.GetComponent<BattleManager_Main>().enemy_ability3_cd = max_cd;
                        master_manager.GetComponent<BattleManager_Ability>().Activate_Ability(SaveLoad_Data.enemy_ability3_id, caster);
                    };
                };
                break;
        };

        
    }

    private static bool CheckAvailability(Vector2 waypoint, GameObject enemy, GameObject caster, GameObject master_manager, string ability_id)
    {
        switch(ability_id)
        {
            case "ability_01":
                return AI_Ability_01.Check(enemy,caster);
            case "ability_02":
                return AI_Ability_02.Check(enemy,caster);
            case "ability_03":
                return AI_Ability_03.Check(enemy, caster);
            case "ability_04":
                return AI_Ability_04.Check(enemy, caster);
            case "ability_05":
                return AI_Ability_05.Check(enemy,caster);
            case "ability_06":
                return AI_Ability_06.Check(caster,waypoint);
            case "ability_07":
                return AI_Ability_07.Check(caster, master_manager);
            case "ability_08":
                return AI_Ability_08.Check(caster, master_manager);
            case "ability_09":
                return AI_Ability_09.Check();
            default:
                return false;
        };

    }
    
}
