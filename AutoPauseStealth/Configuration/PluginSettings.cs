using BeatSaberMarkupLanguage.Attributes;
using IPA.Config.Data;
using IPA.Config.Stores;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace AutoPauseStealth
{
    class PluginSettings
    {
        public static PluginSettings Instance { get; set; }

        private float DetermineMinFPSSub()
        {
            float hrmFrameRate = UnityEngine.XR.XRDevice.refreshRate;
            if (hrmFrameRate == 0.0f)
            {
                Logger.log?.Error("Couldn't get HRM FrameRate, assuming it's 80 fps => setting minimal fps to 75");
                hrmFrameRate = 80.0f;
            }

            return UnityEngine.Mathf.Round(UnityEngine.Mathf.Round(hrmFrameRate) / 5.0f) * 5 - 5.0f; // round to low 5.. then remove 5 again just in case eg : 72fps for pico4 with VD
        }

        public void Awake() // Non Unity
        {
            if (FpsThresold == 0.0f || DetectFpsThresoldOnStartup)
                FpsThresold = DetermineMinFPSSub();
            RecommendedFpsThresold = "Recommended Min FPS value for your headset is " + DetermineMinFPSSub();
        }

        [UIValue("fpsThresold")]
        public float FpsThresold { get; set; } = 0.0f;

        [UIValue("stabilityDurationCheck")]
        public float StabilityDurationCheck { get; set; } = 0.3f;

        [UIValue("maxWaitingTime")]
        public float MaxWaitingTime { get; set; } = 5.0f;

        [UIValue("reloadOnFailStab")]
        public bool ReloadOnFailStab { get; set; } = false;

        [UIValue("RecommendedFpsThresold")]
        public string RecommendedFpsThresold;

        [UIValue("DetectFpsThresoldOnStartup")]
        public bool DetectFpsThresoldOnStartup { get; set; } = true;
    }
}