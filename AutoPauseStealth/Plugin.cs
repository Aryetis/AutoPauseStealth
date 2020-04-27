using System;
using System.Reflection;
using BeatSaberMarkupLanguage.Settings;
using IPA;
using UnityEngine;
using HarmonyLib;
using IPALogger = IPA.Logging.Logger;

namespace AutoPauseStealth
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        public const string HarmonyId = "com.github.Aryetis.AutoPauseStealth";
        internal static Harmony harmony = new Harmony(HarmonyId);

        internal static Plugin instance { get; private set; }
        internal static string Name => "AutoPauseStealth";
        internal static AutoPauseStealthController PluginController { get { return AutoPauseStealthController.instance; } }

        [Init]
        public Plugin(IPALogger logger)
        {
            instance = this;
            Logger.log = logger;
            Logger.log.Debug("Logger initialized.");
        }

        #region Disableable
        [OnEnable]
        public void OnEnable()
        {
            BSMLSettings.instance.AddSettingsMenu("AutoPauseStealth", "AutoPauseStealth.Views.settings.bsml", PluginSettings.instance);
            new GameObject("AutoPauseStealthController").AddComponent<AutoPauseStealthController>();
            ApplyHarmonyPatches();
        }

        [OnDisable]
        public void OnDisable()
        {
            BSMLSettings.instance.RemoveSettingsMenu(PluginSettings.instance);
            if (PluginController != null)
                GameObject.Destroy(PluginController);
            RemoveHarmonyPatches();
        }
        #endregion

        #region Harmony
        public static void ApplyHarmonyPatches()
        {
            try
            {
                Logger.log.Debug("Applying Harmony patches.");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Logger.log.Critical("Error applying Harmony patches: " + ex.Message);
                Logger.log.Debug(ex);
            }
        }

        public static void RemoveHarmonyPatches()
        {
            try
            {
                // Removes all patches with this HarmonyId
                harmony.UnpatchAll(HarmonyId);
            }
            catch (Exception ex)
            {
                Logger.log.Critical("Error removing Harmony patches: " + ex.Message);
                Logger.log.Debug(ex);
            }
        }
        #endregion
    }
}