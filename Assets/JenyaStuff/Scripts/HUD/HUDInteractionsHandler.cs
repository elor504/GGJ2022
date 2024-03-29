using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDInteractionsHandler : MonoBehaviour
{
    private AudioSettings audioSettings;
    private HUDstate hudState;
    private bool isPauseAvailable;
    bool isFullScreenHUD;

    public Image ContinueB;
    public Image OptionsB;
    public Image HowToPlayB;
    public Image ExitMM;
    public Sprite defaultSprite;

    #region PublicFields
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject htpMenu;
    public GameObject resultMenu;
    #endregion

    void Start()
    {
        audioSettings = AudioSettings.ASInstance;

        // Defalut HUD settings
        hudState = HUDstate.pause;
        isPauseAvailable = true;
        isFullScreenHUD = UIInteractionsHandler.GetInstance.isFullScreenMenus;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            audioSettings.sfxAudio[4].Play();
            //ResetHover();

            if(hudState == HUDstate.result)
            {
                isPauseAvailable = false;

                resultMenu.SetActive(true);
                htpMenu.SetActive(false);
            }
            if (hudState == HUDstate.options)
            {
                // Close options menu and open pause menu
                pauseMenu.SetActive(true);
                optionsMenu.SetActive(false);

            }
            if (isPauseAvailable == false)
            {

                // Set a flag for pause menu
                isPauseAvailable = true;
            }
            else if (isPauseAvailable == true)
            {
                // Open pause menu
                pauseMenu.SetActive(true);

                // Change state
                hudState = HUDstate.pause;

                // Set a flag for pause menu
                isPauseAvailable = false;

                // Freeze time
                Time.timeScale = 0f;
            }
        }
    }

    public void Button_Restart()
    {
        // Unfreeze time
        Time.timeScale = 1f;
        audioSettings.sfxAudio[0].Play();

        SceneManager.LoadScene(1);
    }
    public void Button_Continue()
    {
        audioSettings.sfxAudio[0].Play();

        // Unfreeze time
        Time.timeScale = 1f;

        // Enable player input


        // Change state
        hudState = HUDstate.pause;

        // Close pause menu
        pauseMenu.SetActive(false);

        // Set a flag for pause menu
        isPauseAvailable = true;
    }
    public void Button_Options()
    {
        audioSettings.sfxAudio[0].Play();

        hudState = HUDstate.options;

        // Close pause menu and open options menu
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void Button_HowToPlay()
    {
        audioSettings.sfxAudio[0].Play();

        hudState = HUDstate.result;

        resultMenu.SetActive(false);
        htpMenu.SetActive(true);
    }
    public void Button_ExitToMM()
    {
        audioSettings.sfxAudio[0].Play();

        // Unfreeze time
        Time.timeScale = 1f;

        // Save volume settings
        audioSettings.SaveSoundSettings();

        SceneManager.LoadScene(0);
    }
    public void ScreenResolutionDropdownInputData(int value)
    {
        if (value == 0)
        {
            Debug.Log("Resolution 1");
            Screen.SetResolution(1920, 1080, isFullScreenHUD);
        }
        if (value == 1)
        {
            Debug.Log("Resolution 2");
            Screen.SetResolution(1366, 768, isFullScreenHUD);
        }
        if (value == 2)
        {
            Debug.Log("Resolution 3");
            Screen.SetResolution(1280, 720, isFullScreenHUD);
        }
    }
    public void FullScreenDropdownInputData(int value)
    {
        if (value == 0)
        {
            Debug.Log("Full screen");
            isFullScreenHUD = true;
            Screen.fullScreen = isFullScreenHUD;
        }
        if (value == 1)
        {
            Debug.Log("Window screen");
            isFullScreenHUD = false;
            Screen.fullScreen = isFullScreenHUD;
        }
    }

    private void ResetHover()
    {
        ContinueB.sprite = defaultSprite;
        OptionsB.sprite = defaultSprite;
        ExitMM.sprite = defaultSprite;
        HowToPlayB.sprite = defaultSprite;  
    }
}

enum HUDstate
{
    pause,
    options,
    result
}
