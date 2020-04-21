using System.Linq;
using UnityEngine;

namespace AutoPauseStealth
{
    public class AutoPauseStealthController : MonoBehaviour
    {
        public static AutoPauseStealthController instance { get; private set; }

        public void OnActiveSceneChanged(UnityEngine.SceneManagement.Scene prevScene, UnityEngine.SceneManagement.Scene nextScene)
        {
            Logger.log?.Debug($"{name}: LoadingScene({nextScene.name})");

            if (b_stabilityPeriodActive) // because of fast restart/exit combined with long StabilityDurationCheck
                CancelInvoke("StopStabilityCheckPeriod");

            if (nextScene.name == "GameCore")
            {
                GamePause = Resources.FindObjectsOfTypeAll<GamePause>().FirstOrDefault();
                if (GamePause == null)
                    Logger.log?.Error("Couldn't find GamePause object");
                b_inGame = true;
                f_stabilityTimer = 0.0f;
                b_stabilityPeriodActive = true;
                Invoke("StopStabilityCheckPeriod", PluginSettings.instance.MaxWaitingTime);
            }
            else
            {
                GamePause = null;
                b_inGame = false;
                b_stabilityPeriodActive = false;
            }
        }



        private void Awake()
        {
            if (instance != null)
            {
                Logger.log?.Warn($"Instance of {this.GetType().Name} already exists, destroying.");
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this);
            instance = this;
            Logger.log?.Debug($"{name}: Awake()");
        }

        private void Start()
        {
            b_inGame = false;
            b_stabilityPeriodActive = false;
            Logger.log?.Debug($"{name}: Start()");
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void StopStabilityCheckPeriod()
        {
            Logger.log?.Info($"StabilityCheckPeriod over, resuming game");
            GamePause.Resume();
            b_stabilityPeriodActive = false;
        }

        private void Update()
        {
            if (b_inGame && b_stabilityPeriodActive) 
            {
                f_fps = 1.0f / Time.deltaTime;

                if (f_fps > PluginSettings.instance.FpsThresold)
                {
                    f_stabilityTimer += Time.deltaTime;
                    if (f_stabilityTimer >= PluginSettings.instance.StabilityDurationCheck)
                    {
                        Logger.log?.Info($"Initialization Lag finished, resuming game");
                        GamePause.Resume();
                        b_stabilityPeriodActive = false;
                    }
                }
                else
                {
                    f_stabilityTimer = 0.0f;
                }
            }
        }

        private void OnDestroy()
        {
            Logger.log?.Debug($"{name}: OnDestroy()");
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= OnActiveSceneChanged;
            instance = null;
        }



        private float f_fps;
        public static GamePause GamePause;
        private float f_stabilityTimer;
        private bool b_inGame;
        private bool b_stabilityPeriodActive;
    }
}
