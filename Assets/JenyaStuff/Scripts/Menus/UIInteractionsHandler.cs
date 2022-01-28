using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInteractionsHandler : MonoBehaviour
{
    InterfaceHandler interfaceHandler;
    [HideInInspector] public bool isFullScreenMenus = true;

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
        SceneManager.LoadScene(1);
    }
    public void Button_Options()
    {
        // Switch to OptionsMenu state and activate all its logic
        var context = new Context(new OptionsMenuState());
        context.Request();
    }
    public void Button_Credits()
    {
        // Switch to CreditsMenu state and activate all its logic
        var context = new Context(new CreditsMenuState());
        context.Request();
    }
    public void Button_Exit()
    {
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

    private void Update()
    {
        // Back button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Checking if we're on options or credits menu screen
            if (interfaceHandler.uIState == UIState.OptionsMenu || interfaceHandler.uIState == UIState.CreditsMenu)
            {
                // Switch to MainMenu state and activate all its logic
                var context = new Context(new MainMenuState());
                context.Request();
            }
        }
        // Exit to main menu button
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Checking if we're on gamecompletion menu screen
            if (interfaceHandler.uIState == UIState.CompletionMenu)
            {
                // Switch to MainMenu state and activate all its logic
                var context = new Context(new MainMenuState());
                context.Request();
            }
        }
    }
}