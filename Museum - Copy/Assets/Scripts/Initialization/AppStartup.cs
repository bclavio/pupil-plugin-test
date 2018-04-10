using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppStartup
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBeforeSceneLoadRuntimeMethod()
    {
        if (!Directory.Exists(AppConstants.DefaultEyeTrackingFolder))
            Directory.CreateDirectory(AppConstants.DefaultEyeTrackingFolder);
    }
}
