using System;

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
        // TODO: apply any hooks that should always be active
    }

    public override void Unload() {
        // TODO: unapply any hooks applied in Load()
    }

    // Optional, initialize anything after Celeste has initialized itself properly.
    public override void Initialize() {
    }

    // Optional, do anything requiring either the Celeste or mod content here.
    public override void LoadContent(bool firstLoad) {
    }
}