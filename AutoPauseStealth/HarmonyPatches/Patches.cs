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
            Logger.log?.Debug($"Pausing game right after AudioTimeSyncControllerPatch::StartSong()");
            return;
        }
    }
}