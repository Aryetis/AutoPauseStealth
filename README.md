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

![InGameSettings](https://github.com/Aryetis/AutoPauseStealth/blob/master/AutoPauseStealth/Resources/SettingsMenuInGame.png)

[Autoset Min FPS], will automatically set the [Desired Minimal FPS] value based on your headset refreshrate.

[Desired Minimal FPS], FPS value to reach what is considered a stable framerate.

[FPS Stability Check Duration], FPS must be above [Desired Minimal FPS] for that much seconds before the mod will start the song.

[Force GameStart duration], It's a railguard. If somehow your computer can't reach a stable framerate at start under [Force GameStart duration] seconds, then it will start the song anyways.

----------

## How do I know if it works ?

Well if it works correctly you shouldn't notice it, otherwise it wouldn't be stealth would it.
Okay okay... If it works correctly you should be missing your sabers at the beginning of the song for [FPS Stability Check Duration] seconds.

----------

## Where do I download it ? How do I install it ?

Check the Release page https://github.com/Aryetis/AutoPauseStealth/releases and donwload the latest release

Use ModAssistant to install the bare minimal mods / requirements : BSIPA, SongLoader, BeatSaberMarkupLanguage, BS Utils

Copy the AutoPauseStealth.dll in the following folder [SteamFolder]\steamapps\common\Beat Saber\Plugins

----------

## Known Issues

The [Autoset Min FPS] button can be a little intrusive and like to reset [Desired Minimal FPS]'s value too.

"Sometimes it seems like I'm not lagging yet I'm left hanging up a long time at startup". If you set a big values for both [FPS Stability Check Duration] and [Force GameStart duration] this kind of situation can happen as the mod will require your game's framerate to be above [Desired Minimal FPS] for the whoooole [FPS Stability Check Duration]. And so if your framerate dips below [Desired Minimal FPS] even for 0.01 second, it's enough for the mod to keep the game paused. Flawed ? Maybe. As designed ? Yes. Solution ? Try to lower your [Desired Minimal FPS] and [FPS Stability Check Duration]. Hotel ? Trivago.
