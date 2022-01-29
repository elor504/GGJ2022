using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInteractionsHandler : MonoBehaviour
{
    InterfaceHandler interfaceHandler;
    [HideInInspector] public bool isFullScreenMenus = true;

    public Image NewGameB;
    public Image OptionsB;
    public Image CreditsB;
    public Image ExitB;
    public Sprite defaultSprite;

    private static UIInteractionsHandler UIIHInstance;
    public static UIInteractionsHandler GetInstance => UIIHInstance;

    private void Awake()
    {
        if (UIIHInstance == null)
        {
            UIIHInstance = this;
        }
        else if (UIIHInstance != this)
        {
            Destroy(this.gameObject);
        }

    }

    private void Start()
    {
        interfaceHandler = InterfaceHandler.GetInstance;

        MenuInit();
    }


    #region MainMenu Interactions
    public void Button_NewGame()
    {
        AudioManagerCS.GetInstance.sfxAudio[0].Play();
        AudioManagerCS.GetInstance.musicAudio[0].Stop();
        SceneManager.LoadScene(1);
    }
    public void Button_Options()
    {
        AudioManagerCS.GetInstance.sfxAudio[0].Play();
        // Switch to OptionsMenu state and activate all its logic
        var context = new Context(new OptionsMenuState());
        context.Request();
    }
    public void Button_Credits()
    {
        AudioManagerCS.GetInstance.sfxAudio[0].Play();
        // Switch to CreditsMenu state and activate all its logic
        var context = new Context(new CreditsMenuState());
        context.Request();
    }
    public void Button_Exit()
    {
        AudioManagerCS.GetInstance.sfxAudio[0].Play();
        Application.Quit();
    }
    #endregion

    #region OptionsMenu Interactions
    public void ScreenResolutionDropdownInputData(int value)
    {
        if (value == 0)
        {
            Debug.Log("Resolution 1");
            Screen.SetResolution(1920, 1080, isFullScreenMenus);
        }
        if (value == 1)
        {
            Debug.Log("Resolution 2");
            Screen.SetResolution(1366, 768, isFullScreenMenus);
        }
        if (value == 2)
        {
            Debug.Log("Resolution 3");
            Screen.SetResolution(1280, 720, isFullScreenMenus);
        }
    }
    public void FullScreenDropdownInputData(int value)
    {
        if (value == 0)
        {
            Debug.Log("Full screen");
            isFullScreenMenus = true;
            Screen.fullScreen = isFullScreenMenus;
        }
        if (value == 1)
        {
            Debug.Log("Window screen");
            isFullScreenMenus = false;
            Screen.fullScreen = isFullScreenMenus;
        }
    }


    #endregion

    public void MenuInit()
    {
        // Switch to MainMenu state and activate all its logic (Starting state)
        var context = new Context(new MainMenuState());
        context.Request();
    }

    private void ResetHover()
    {
        NewGameB.sprite = defaultSprite;
        OptionsB.sprite = defaultSprite;
        CreditsB.sprite = defaultSprite;
        ExitB.sprite = defaultSprite;
    }

    private void Update()
    {
        // Back button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetHover();
            // Checking if we're on options or credits menu screen
            if (interfaceHandler.uIState == UIState.OptionsMenu || interfaceHandler.uIState == UIState.CreditsMenu)
            {
                AudioManagerCS.GetInstance.sfxAudio[4].Play();

                // Switch to MainMenu state and activate all its logic
                var context = new Context(new MainMenuState());
                context.Request();
            }
        }
    }
}
