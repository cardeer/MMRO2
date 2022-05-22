using System;
using System.Collections.Generic;
using System.Text;

namespace MMRO2.Utils
{
    static class Stats
    {
        public static float BulletDamage()
        {
            Enums.Perks perk = Enums.Perks.IncreaseBulletDamage;
            return Settings.Gameplay.PerkStats[perk][Global.Instance.GameData.Perks[perk]];
        }

        public static float NumberOfBullets()
        {
            Enums.Perks perk = Enums.Perks.NumberOfBullets;
            return Settings.Gameplay.PerkStats[perk][Global.Instance.GameData.Perks[perk]];
        }

        public static float ReduceManaUsage()
        {
            Enums.Perks perk = Enums.Perks.ReduceManaUsage;
            return Settings.Gameplay.PerkStats[perk][Global.Instance.GameData.Perks[perk]];
        }

        public static float MaxMana()
        {
            Enums.Perks perk = Enums.Perks.IncreaseMaxMana;
            return Settings.Gameplay.PerkStats[perk][Global.Instance.GameData.Perks[perk]];
        }

        public static float SkillCooldown()
        {
            Enums.Perks perk = Enums.Perks.ReduceSkillCooldown;
            return Settings.Gameplay.PerkStats[perk][Global.Instance.GameData.Perks[perk]];
        }

        public static float ManaRegeneration()
        {
            Enums.Perks perk = Enums.Perks.ManaRegeneration;
            return Settings.Gameplay.PerkStats[perk][Global.Instance.GameData.Perks[perk]];
        }

        public static float MaxHP()
        {
            Enums.Perks perk = Enums.Perks.IncreaseMaxHP;
            return Settings.Gameplay.PerkStats[perk][Global.Instance.GameData.Perks[perk]];
        }

        public static float IceBullet()
        {
            Enums.Perks perk = Enums.Perks.IceBulletLevel;
            return Settings.Gameplay.PerkStats[perk][Global.Instance.GameData.Perks[perk]];
        }

        public static float FireBullet()
        {
            Enums.Perks perk = Enums.Perks.FireBulletLevel;
            return Settings.Gameplay.PerkStats[perk][Global.Instance.GameData.Perks[perk]];
        }

        public static float LightningBullet()
        {
            Enums.Perks perk = Enums.Perks.LightningBulletLevel;
            return Settings.Gameplay.PerkStats[perk][Global.Instance.GameData.Perks[perk]];
        }
    }
}
