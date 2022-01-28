using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesHelper : MonoBehaviour
{
    private static ScenesHelper SHInstance;
    public static ScenesHelper GetInstance => SHInstance;
    [HideInInspector] public UIState handlerUIState;

    private void Awake()
    {
        if (SHInstance == null)
        {
            SHInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (SHInstance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (this != SHInstance)
        {
            return;
        }

        if (level == 0)
        {
            InterfaceHandler.GetInstance.uIState = handlerUIState;
            UIInteractionsHandler.GetInstance.MenuInit();
        }
    }
}
