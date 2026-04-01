using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public sealed class InputSystemUiDiagnostics : MonoBehaviour
{
    private float nextHeartbeatTime;
    private bool loggedStartup;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Install()
    {
        var go = new GameObject(nameof(InputSystemUiDiagnostics));
        DontDestroyOnLoad(go);
        go.hideFlags = HideFlags.DontSaveInBuild | HideFlags.DontSaveInEditor;
        go.AddComponent<InputSystemUiDiagnostics>();
    }

    private void Update()
    {
        if (!loggedStartup)
        {
            loggedStartup = true;
            LogStartup();
        }

        if (Time.unscaledTime >= nextHeartbeatTime)
        {
            nextHeartbeatTime = Time.unscaledTime + 2.0f;
            LogHeartbeat();
        }

        if (Touchscreen.current is not null)
        {
            var touch = Touchscreen.current.primaryTouch;
            if (touch.press.wasPressedThisFrame)
            {
                Debug.Log($"[InputSystemUiDiagnostics] touchDown pos={touch.position.ReadValue()}");
            }
        }
    }

    private static void LogStartup()
    {
        var es = EventSystem.current;
        Debug.Log(
            $"[InputSystemUiDiagnostics] startup platform={Application.platform} inputSystem={typeof(InputSystem).Assembly.GetName().Version} " +
            $"touchscreen={(Touchscreen.current is not null)} mouse={(Mouse.current is not null)}"
        );

        if (es == null)
        {
            Debug.LogWarning("[InputSystemUiDiagnostics] EventSystem.current is NULL");
            return;
        }

        var module = es.currentInputModule;
        Debug.Log($"[InputSystemUiDiagnostics] EventSystem={es.name} module={(module != null ? module.GetType().FullName : "NULL")}");

        if (module is InputSystemUIInputModule uiModule)
        {
            Debug.Log($"[InputSystemUiDiagnostics] uiModule actionsAsset={(uiModule.actionsAsset != null ? uiModule.actionsAsset.name : "NULL")}");
        }
    }

    private static void LogHeartbeat()
    {
        var es = EventSystem.current;
        if (es == null)
        {
            Debug.LogWarning("[InputSystemUiDiagnostics] heartbeat: EventSystem.current is NULL");
            return;
        }

        var module = es.currentInputModule;
        Debug.Log($"[InputSystemUiDiagnostics] heartbeat: module={(module != null ? module.GetType().Name : "NULL")}");
    }
}

