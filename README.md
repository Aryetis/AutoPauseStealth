# AutoPauseStealth

## Description :

Automatically and stealthly pause your game at the start of every song waiting for potential lag spikes to end.

----------

## Why ?

If you're lagging at the start of some songs every once in a while for no reasons.
Then check this guide first : https://bsmg.wiki/support/#framerate-issues
You've done of the steps mentioned in it? You bought a new computer especially to get rid of lags? You even sacrificed some virgins ? ... Should I get the police involved?
Ok so you can't rid of the startup lags, then try this mod as a last resort.

----------

## Settings :

What are those ?

![InGameSettings](https://github.com/Aryetis/AutoPauseStealth/blob/master/AutoPauseStealth/Resources/SettingsMenuInGame.jpg)

**[Desired Minimal FPS]**, FPS value to reach what is considered a stable framerate.

**[FPS Stability Check Duration]**, FPS must be above **[Desired Minimal FPS]** for that much seconds before the mod will start the song. (Anything above 0.3 should be overkill)

**[Force GameStart duration]**, It's a railguard. If somehow your computer can't reach a stable framerate at start under **[Force GameStart duration]** seconds, then it will start the song anyways.

**[Reload if fps stabilization failed]**, Another railguard, if this box is ticked and the game fails to stabilize its fps at the beginning of a song, then it will reload the song. Althought unlikely to happen, reloading has been proven effective in most of the cases. (Ticked off by default to avoid incomprehension if the user is using bad settings.

----------

## Where do I download it ? How do I install it ?

Check the Release page https://github.com/Aryetis/AutoPauseStealth/releases and donwload the latest release

Use ModAssistant to install the bare minimal mods / requirements : BSIPA, SongLoader, BeatSaberMarkupLanguage, BS Utils

Copy the AutoPauseStealth.dll in the following folder [SteamFolder]\steamapps\common\Beat Saber\Plugins

Or 

Wait for it to show up in ModAssistant and downnload it there

----------

## Known Issues

- "Sometimes it seems like I'm not lagging yet I'm left hanging up a long time at startup"

If you set big values for both **[FPS Stability Check Duration]** and **[Force GameStart duration]** this kind of situation can happen as the mod will require your game's framerate to be above **[Desired Minimal FPS]** for the whoooole **[FPS Stability Check Duration]**. Therefore if your framerate dips below **[Desired Minimal FPS]** even for 0.01 second, it's enough for the mod to keep the game paused. Flawed ? Maybe. As designed ? Yes. Solution ? Try to lower your **[Desired Minimal FPS]** and **[FPS Stability Check Duration]**. Hotel ? Trivago.

