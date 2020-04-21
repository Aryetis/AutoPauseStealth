using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Parser;
using BS_Utils.Utilities;
using System;

namespace AutoPauseStealth
{
    class PluginSettings : PersistentSingleton<PluginSettings>
    {
        [UIValue("fpsThresold")]
        public float FpsThresold 
        {
            get => bsConfig.GetFloat("Settings", "fpsThresold", fpsThresold);
            set => bsConfig.SetFloat("Settings", "fpsThresold", value);
        }
        [UIValue("stabilityDurationCheck")]
        public float StabilityDurationCheck
        {
            get => bsConfig.GetFloat("Settings", "stabilityDurationCheck", stabilityDurationCheck);
            set => bsConfig.SetFloat("Settings", "stabilityDurationCheck", value);
        }
        [UIValue("maxWaitingTime")]
        public float MaxWaitingTime
        {
            get => bsConfig.GetFloat("Settings", "maxWaitingTime", maxWaitingTime);
            set => bsConfig.SetFloat("Settings", "maxWaitingTime", value);
        }

        [UIAction("DetermineMinFPS")]
        public void DetermineMinFPS()
        {
            FpsThresold = UnityEngine.Mathf.Round(UnityEngine.XR.XRDevice.refreshRate) - 5.0f;
            parserParams.EmitEvent("cancel");
            Logger.log?.Info($"Determining MinFPS as {FpsThresold}");
        }

        public void Awake()
        {
            bsConfig = new Config(Plugin.Name);

            FpsThresold = (Enum.TryParse(bsConfig.GetString("Modes", "Menu", "Interactive"), out float fpsTParsed))
                ? fpsTParsed
                : 70.0f;
            StabilityDurationCheck = (Enum.TryParse(bsConfig.GetString("Modes", "Menu", "Interactive"), out float sdcParsed))
                ? sdcParsed
                : 0.25f;
            MaxWaitingTime = (Enum.TryParse(bsConfig.GetString("Modes", "Menu", "Interactive"), out float mwtParsed))
                ? mwtParsed
                : 5.0f;
        }

        [UIParams]
        private BSMLParserParams parserParams;
        private Config bsConfig;
        private float fpsThresold;
        private float stabilityDurationCheck;
        private float maxWaitingTime;
    }
}