using UnityEngine;
using System.Collections;

public static class MessageTypes
{
    public static readonly string EnemyAppear = "ENEMY_APPEAR";
    public static readonly string EnemyDisappear = "ENEMY_DISAPPEAR";
    public static readonly string EnemyLaunchMissile = "ENEMY_LAUNCH_MISSILE";
    public static readonly string EnemyMissileDestroyed = "ENEMY_MISSILE_DESTROYED";
    public static readonly string LockedTarget = "LOCKED_TARGET";
    public static readonly string LaunchMissile = "LAUNCH_MISSILE";
    public static readonly string MissileLauncherReloaded = "MISSILE_LAUNCHER_RELOADED";
    public static readonly string PlayerGetDamage = "PLAYER_GET_DAMAGE";
    public static readonly string CollisionAlert = "COLLISION_ALERT";
    public static readonly string CollisionAlertLifted = "COLLISION_ALERT_LIFTED";
    public static readonly string CraftDestroyed = "CRAFT_DESTROYED";
    public static readonly string AeroliteDestroyed = "AEROLITE_DESTROYED";
    public static readonly string EnemyDestroyed = "ENEMY_DESTROYED";
    public static readonly string DestroyAeroliteByMissile = "DESTROY_AEROLITE_BY_MISSILE";
    public static readonly string DifficultLevelUp = "DIFFICULT_LEVEL_UP";
    public static readonly string ComboIncrease = "COMBO_INCREASE";
    public static readonly string ComboEnd = "COMBO_END";
    public static readonly string CannonPowerUp = "CANNON_POWER_UP";
    public static readonly string CannonPowerDown = "CANNON_POWER_DOWN";
    public static readonly string OpenEnergyShield = "OPEN_ENERGY_SHIELD";
    public static readonly string CloseEnergyShield = "CLOSE_ENERGY_SHIELD";
    public static readonly string StartInfiniteMissile = "START_INFINITE_MISSILE";
    public static readonly string StopInfiniteMissile = "STOP_INFINITE_MISSILE";
    public static readonly string GainAeroliteScore = "GAIN_AEROLITE_SCORE";
    public static readonly string GainEnemyScore = "GAIN_ENEMY_SCORE";
    public static readonly string GainComboScore = "GAIN_COMBO_SCORE";
    public static readonly string CannonButtonIsDown = "CANNON_BUTTON_IS_DOWN";
    public static readonly string MissileButtonIsDown = "MISSILE_BUTTON_CLICK";
    public static readonly string GamePause = "GAME_PAUSE";
    public static readonly string GameResume = "GAME_RESUME";
    public static readonly string ShowHelpScreen = "SHOW_HELP_SCREEN";
    public static readonly string HideHelpScreen = "HIDE_HELP_SCREEN";
    public static readonly string PortalOpened = "PORTAL_OPENED";
    public static readonly string PortalDestroyed = "PORTAL_DESTROYED";
    public static readonly string GameOver = "GAME_OVER";
}
