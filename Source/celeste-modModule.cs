// Example usings.
using Celeste.Mod.UI;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;
using Celeste;
using Celeste.Mod;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celeste.Mod.Test;

public class TestModule : EverestModule {
    public static TestModule Instance { get; private set; }

    public override Type SettingsType => typeof(TestModuleSettings);
    public static TestModuleSettings Settings => (TestModuleSettings) Instance._Settings;

    public override Type SessionType => typeof(TestModuleSession);
    public static TestModuleSession Session => (TestModuleSession) Instance._Session;

    public override Type SaveDataType => typeof(TestModuleSaveData);
    public static TestModuleSaveData SaveData => (TestModuleSaveData) Instance._SaveData;

    public TestModule() {
        Instance = this;
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(TestModule), LogLevel.Verbose);
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(TestModule), LogLevel.Info);
    }

    public override void Load() {
        On.Celeste.ScreenWipe.ctor += ScreenWipe_Ctor;
    }

    public override void Unload() {
        On.Celeste.ScreenWipe.ctor -= ScreenWipe_Ctor;
    }

    private static void ScreenWipe_Ctor(On.Celeste.ScreenWipe.orig_ctor orig, ScreenWipe self, Scene scene, bool wipeIn, Action onComplete) {

        orig(self, scene, wipeIn, onComplete);
        
        if (!wipeIn && scene is Level level) {

            if (level.Session.Deaths > 0) {
                
                scene.OnEndOfFrame += () => {
                    if (self != null) {
                        self.Cancel();

                        Player player = self.Scene?.Entities?.FindFirst<Player>();

                        if (player == null) {
                            Vector2 spawnPoint = level.Session.RespawnPoint ?? level.DefaultSpawnPoint;

                            player = new Player(spawnPoint, PlayerSpriteMode.Madeline);
                            level.Add(player);
                        }
                    }
                };
            }
        }
    }
}