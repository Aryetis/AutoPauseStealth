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
        public static bool IsMultiplayer;

        public void OnActiveSceneChanged(UnityEngine.SceneManagement.Scene prevScene, UnityEngine.SceneManagement.Scene nextScene)
        {
            //Logger.log?.Debug($"{name}: LoadingScene({nextScene.name} : {nextScene.path})");

            if (StabilityPeriodActive) // because of fast restart/exit combined with long StabilityDurationCheck
                CancelInvoke("StopStabilityCheckPeriod"); // Cancel previous session's StopStabilityCheckPeriod

            if (nextScene.name == "GameCore")
            {
                IsMultiplayer = (Resources.FindObjectsOfTypeAll<MultiplayerController>().LastOrDefault() != null);
                if (IsMultiplayer)
                {
                    Logger.log?.Info($"Starting Multiplayer play session, AutoPauseStealth will NOT pause the game !");
                    return;
                }

                // LastOrDefault() because BeatSaber don't know how to clean and keep every objects created per song play ... 
                // Lazy fix => LastOrDefault() should assure to grab SinglePlayer related objects ...
                // Note : Counter++ setting page load a """level""" (but without any ScoreController)
                ScoreController = Resources.FindObjectsOfTypeAll<ScoreController>().LastOrDefault();
                if (ScoreController == null)
                {
                    Logger.log?.Warn("Couldn't find ScoreController object, probably not a vanilla GameCore scene");
                    return;
                }

                SongController = Resources.FindObjectsOfTypeAll<SongController>().LastOrDefault();
                if (SongController == null)
                {
                    Logger.log?.Warn("Couldn't find SongController object, probably not a vanilla GameCore scene");
                    return;
                }

                RestartController = Resources.FindObjectsOfTypeAll<StandardLevelRestartController>().LastOrDefault();
                if (RestartController == null)
                {
                    RestartController = Resources.FindObjectsOfTypeAll<MissionLevelRestartController>().LastOrDefault();
                    if (RestartController == null)
                    {
                        RestartController = Resources.FindObjectsOfTypeAll<TutorialRestartController>().LastOrDefault();
                        if (RestartController == null)
                        {
                            Logger.log?.Warn("Couldn't find RestartController object, probably not a vanilla GameCore scene");
                            return;
                        }
                    }
                }

                StabilityPeriodActive = true;
                b_inGame = true;
                f_stabilityTimer = 0.0f;

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
            IsMultiplayer = false;
        }

        private void StopStabilityCheckPeriod() // In ideal times, shouldn't be called / should be canceled
        {
            Logger.log?.Debug($"StabilityCheckPeriod over, resuming game");
            StabilityPeriodActive = false;
            if (PluginSettings.Instance.ReloadOnFailStab)
            {
                Logger.log?.Debug($"fps too low for more than "+ PluginSettings.Instance.MaxWaitingTime + "s => RestartLevel()");
                RestartController.RestartLevel();
            }
            else
            {
                ScoreController.enabled = true;
                SongController.ResumeSong();
            }
        }

        private void Update()
        {
            if (b_inGame && StabilityPeriodActive && !IsMultiplayer) 
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
                        SongController.ResumeSong();
                    }
                }
                else
                    f_stabilityTimer = 0.0f;
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
