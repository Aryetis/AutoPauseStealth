# AutoPauseStealth

## Description :

Automatically and stealthly pause your game (without showing any pause UI) at the start of every song waiting for potential lag spikes to end.

----------

## Why ?

If you're lagging at the start of some songs every once in a while for no reasons.
Then check this guide first : https://bsmg.wiki/support/#framerate-issues
You've done all of the steps mentioned in it? You bought a new computer especially to get rid of lags? You even sacrificed some virgins ? ... Should I get the police involved?
Ok so you can't rid of the startup lags, then try this mod as a last resort.

----------

## Multiplayer Support ?

No. The mod should not have any effects on multiplayer sessions.

----------

## Settings :

What are those ?

![InGameSettings](https://github.com/Aryetis/AutoPauseStealth/blob/master/AutoPauseStealth/Resources/SettingsMenuInGame.jpg)

**[Desired Minimal FPS]** Framerate will be considered stable once it reaches [Desired Minimal FPS]. **<ins> DO NOT SET THIS TO ANY VALUES ABOVE YOUR HEADSET REFRESH RATE </ins>**. The rule of thumb would be to choose the value right below your headset's refresh rate (or just follow the recommendation stated in-game above this setting). Otherwise you'll wait [Force GameStart duration] seconds at the start of every song for no reason or will experience a reload loop if you ticked [Reload if fps stabilization failed]

**[FPS Stability Check Duration]**, the mod will automatically unpause/start whenever fps counter is above **[Desired Minimal FPS]** for more than **[FPS Stability Check Duration]** seconds. (Anything above 0.3 is probably overkill)

**[Force GameStart duration]**, It's a railguard. If somehow your computer can't reach a stable framerate, the mod will unpause/start the song regardless of fps after **[Force GameStart duration]** seconds.

**[Reload if fps stabilization failed]**, Another railguard, if this box is ticked and the game fails to stabilize its fps then it will reload the song. Althought unlikely to happen, reloading has been proven effective in most of those cases. (Ticked off by default to avoid incomprehension if the user has set some crazy values for [Desired Minimal FPS]).

----------

**2 SOLUTIONS :**

1/ Manual installation :

- Check the <a href="https://github.com/Aryetis/AutoPauseStealth/releases">Release page</a> and donwload the latest AutoPauseStealth-\*.zip

- Use <a href="https://github.com/Assistant/ModAssistant">ModAssistant</a> to install the bare minimal mods / requirements : BSIPA, BeatSaberMarkupLanguage

- Copy the AutoPauseStealth.dll from the release's page .zip in the following folder [SteamFolder]\steamapps\common\Beat Saber\Plugins

**Or** 

2/ Wait for it to show up in ModAssistant and downnload it there

----------

## Known Issues

- "Sometimes it seems like I'm not lagging yet I'm left hanging up a long time at startup"

If you set too high values for both **[FPS Stability Check Duration]** and **[Force GameStart duration]** this kind of situation can happen as the mod will require your game's framerate to be above **[Desired Minimal FPS]** for the whoooole **[FPS Stability Check Duration]** to deem the fps stable. Therefore if your framerate dips below **[Desired Minimal FPS]** even for 0.01 second (because of some discord notification, web browser on the side loading something, windows doing its windows things, etc) then it's enough for the mod to keep the game paused. Flawed ? Maybe. As designed ? Yes. Solution ? Set your **[Desired Minimal FPS]** to a correct value and most importantly lower **[FPS Stability Check Duration]**. Hotel ? Trivago.

----------

## Licensing 

This mod and its source code is under the <a href="https://github.com/Aryetis/AutoPauseStealth/blob/master/LICENSE">MIT License</a>
