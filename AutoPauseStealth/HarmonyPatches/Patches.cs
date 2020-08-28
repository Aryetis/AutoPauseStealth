using HarmonyLib;

namespace AutoPauseStealth.Patches
{
    [HarmonyPatch(typeof(AudioTimeSyncController))]
    [HarmonyPatch("StartSong")]
    class AudioTimeSyncControllerPatch
    {
        static void Postfix(AudioTimeSyncController __instance)
        {
            if (AutoPauseStealthController.StabilityPeriodActive)
            {
                AutoPauseStealthController.ScoreController.enabled = false;
                AutoPauseStealthController.SongController.PauseSong();
                Logger.log?.Debug($"AutoPauseStealthController.StabilityPeriodActive is true " +
                                  $"=> Pausing game right after AudioTimeSyncControllerPatch::StartSong()");
            }

            return;
        }
    }

    [HarmonyPatch(typeof(PauseMenuManager))]
    [HarmonyPatch("ShowMenu")]
    class PauseMenuManagerPatch
    {
        static void Postfix(PauseMenuManager __instance)
        {
            AutoPauseStealthController.instance.OnPauseShowMenu();
            return;
        }
    }
}