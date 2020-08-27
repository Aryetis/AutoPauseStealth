using BeatSaberMarkupLanguage.Attributes;
using IPA.Config.Stores;
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
                Logger.log?.Error("Couldn't get HRM FrameRate, assuming it's 80 fps");
                hrmFrameRate = 80.0f;
            }
            return UnityEngine.Mathf.Round(hrmFrameRate) - 5.0f;
        }

        public void Awake() // Non Unity
        {
            Logger.log?.Info($"SANITY CHECK ARE YOU THERE");
            if (float.IsNaN(FpsThresold))
                FpsThresold = DetermineMinFPSSub();
            RecommendedFpsThresold = "Recommended Min FPS value for your headset is " + DetermineMinFPSSub();
        }

        [UIValue("fpsThresold")]
        public float FpsThresold { get; set; } = float.NaN;

        [UIValue("stabilityDurationCheck")]
        public float StabilityDurationCheck { get; set; } = 0.3f;

        [UIValue("maxWaitingTime")]
        public float MaxWaitingTime { get; set; } = 5.0f;

        [UIValue("reloadOnFailStab")]
        public bool ReloadOnFailStab { get; set; } = false;

        [UIValue("RecommendedFpsThresold")]
        public string RecommendedFpsThresold;
    }
}