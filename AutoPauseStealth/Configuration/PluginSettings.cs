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
            float headsetRefreshRate = UnityEngine.XR.XRDevice.refreshRate;
            if (headsetRefreshRate < 0)
            {
                Logger.log?.Error("Couldn't get HRM FrameRate, assuming it's 80 fps => setting minimal fps to 75");
                headsetRefreshRate = 80.0f;
            }
            Logger.log?.Debug("Detected headset framerate: " + headsetRefreshRate);

            return UnityEngine.Mathf.Round(UnityEngine.Mathf.Round(headsetRefreshRate) / 5.0f) * 5 - 5.0f; // round to low 5.. then remove 5 again just in case eg : 72fps for pico4 with VD
        }

        private float m_FpsThresold = -1;
        [UIValue("fpsThresold")]
        public float FpsThresold 
        { 
            get 
            { 
                if (m_FpsThresold <= 0) 
                    m_FpsThresold = DetermineMinFPSSub(); 
                return m_FpsThresold; 
            }
            set { m_FpsThresold = value; } 
        }

        [UIValue("stabilityDurationCheck")]
        public float StabilityDurationCheck { get; set; } = 0.3f;

        [UIValue("maxWaitingTime")]
        public float MaxWaitingTime { get; set; } = 5.0f;

        [UIValue("reloadOnFailStab")]
        public bool ReloadOnFailStab { get; set; } = false;

        [UIValue("RecommendedFpsThresold")]
        public string RecommendedFpsThresold
        {
            get { return "Recommended Min FPS value for your headset is " + DetermineMinFPSSub(); }
            private set { }
        }

        [UIValue("DetectFpsThresoldOnStartup")]
        public bool DetectFpsThresoldOnStartup { get; set; } = true;
    }
}