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
            DetermineMinFPSSub();
            parserParams.EmitEvent("cancel");
            Logger.log?.Info($"Determining MinFPS as {FpsThresold}");
        }

        private void DetermineMinFPSSub()
        {
            float hrmFrameRate = UnityEngine.XR.XRDevice.refreshRate;
            if (hrmFrameRate == 0.0f)
            {
                Logger.log?.Error("Couldn't get HRM FrameRate, assuming it's 80 fps");
                hrmFrameRate = 80.0f;
            }
            FpsThresold = UnityEngine.Mathf.Round(hrmFrameRate) - 5.0f;
        }

        public void Awake()
        {
            ConfigIntializationOk = false;
            bsConfig = new Config(Plugin.Name);

            if (float.TryParse(bsConfig.GetString("Settings", "fpsThresold"), out float fpsTParsed))
                FpsThresold = fpsTParsed;
            else
                DetermineMinFPSSub();
            StabilityDurationCheck = (float.TryParse(bsConfig.GetString("Settings", "stabilityDurationCheck"), out float sdcParsed))
                ? sdcParsed
                : 0.3f;
            MaxWaitingTime = (float.TryParse(bsConfig.GetString("Settings", "maxWaitingTime"), out float mwtParsed))
                ? mwtParsed
                : 5.0f;

            ConfigIntializationOk = true;
        }

        [UIParams]
        private BSMLParserParams parserParams;
        private Config bsConfig;
        private float fpsThresold;
        private float stabilityDurationCheck;
        private float maxWaitingTime;
        public bool ConfigIntializationOk;
    }
}