using System.Linq;
using UnityEngine;

namespace AutoPauseStealth
{
    public class AutoPauseStealthController : MonoBehaviour
    {
        public static AutoPauseStealthController instance { get; private set; }
        public static ScoreController ScoreController;
        public static SongController SongController;
        public static ILevelRestartController RestartController;
        public static bool StabilityPeriodActive;

        public void OnActiveSceneChanged(UnityEngine.SceneManagement.Scene prevScene, UnityEngine.SceneManagement.Scene nextScene)
        {
            Logger.log?.Debug($"{name}: LoadingScene({nextScene.name})");

            if (StabilityPeriodActive) // because of fast restart/exit combined with long StabilityDurationCheck
                CancelInvoke("StopStabilityCheckPeriod"); // Cancel previous session's StopStabilityCheckPeriod

            if (nextScene.name == "GameCore")
            {
                StabilityPeriodActive = true;
                b_inGame = true;
                f_stabilityTimer = 0.0f;

                ScoreController = Resources.FindObjectsOfTypeAll<ScoreController>().FirstOrDefault();
                if (ScoreController == null)
                    Logger.log?.Error("Couldn't find ScoreController object");

                SongController = Resources.FindObjectsOfTypeAll<SongController>().FirstOrDefault();
                if (SongController == null)
                    Logger.log?.Error("Couldn't find SongController object");

                RestartController = Resources.FindObjectsOfTypeAll<StandardLevelRestartController>().FirstOrDefault();
                if (RestartController == null)
                {
                    RestartController = Resources.FindObjectsOfTypeAll<MissionLevelRestartController>().FirstOrDefault();
                    if (RestartController == null)
                    {
                        RestartController = Resources.FindObjectsOfTypeAll<TutorialRestartController>().FirstOrDefault();
                        if (RestartController == null)
                            Logger.log?.Error("Couldn't find RestartController object");
                    }
                }
                Invoke("StopStabilityCheckPeriod", PluginSettings.Instance.MaxWaitingTime);
            }
            else
                b_inGame = false;
        }

        // Prevent game from unpausing if paused during StabilityCheck period (side effect, it will stop the fps stabilization process... meh)
        public void OnPauseShowMenu()
        {
            if (StabilityPeriodActive)
            {
                Logger.log?.Debug($"Pause requested during StabilityCheck period => Turn off Stability check and cancel StopStabilityCheckPeriod routine");
                StabilityPeriodActive = false;
                CancelInvoke("StopStabilityCheckPeriod");
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
            StabilityPeriodActive = false;
            Logger.log?.Debug($"{name}: Start()");
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void StopStabilityCheckPeriod() // In ideal times, shouldn't be called / should be canceled
        {
            Logger.log?.Info($"StabilityCheckPeriod over, resuming game");
            StabilityPeriodActive = false;
            if (PluginSettings.Instance.ReloadOnFailStab)
                RestartController.RestartLevel();
            else
            {
                ScoreController.enabled = true;
                SongController.StartSong();
            }
        }

        private void Update()
        {
            if (b_inGame && StabilityPeriodActive) 
            {
                f_fps = 1.0f / Time.deltaTime;

                if (f_fps > PluginSettings.Instance.FpsThresold)
                {
                    f_stabilityTimer += Time.deltaTime;
                    if (f_stabilityTimer >= PluginSettings.Instance.StabilityDurationCheck)
                    {
                        Logger.log?.Info($"Initialization Lag finished, resuming game");
                        CancelInvoke("StopStabilityCheckPeriod");
                        StabilityPeriodActive = false;
                        ScoreController.enabled = true;
                        SongController.StartSong();
                    }
                }
                else
                {
                    f_stabilityTimer = 0.0f;
                    Logger.log?.Debug($"Fps dipping below threshold");
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
        private float f_stabilityTimer;
        private bool b_inGame;
    }
}
