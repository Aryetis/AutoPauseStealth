using HarmonyLib;
using System;
using UnityEngine;

namespace AutoPauseStealth.Patches
{
    [HarmonyPatch(typeof(AudioTimeSyncController))]
    [HarmonyPatch("StartSong")]
    class AudioTimeSyncControllerPatch
    {
        static void Postfix(AudioTimeSyncController __instance)
        {
            __instance.Pause();
            AutoPauseStealthController.GamePause.Pause();
            AutoPauseStealthController.StabilityPeriodActive = true;
            Logger.log?.Debug($"Pausing game right after AudioTimeSyncControllerPatch::StartSong()");
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