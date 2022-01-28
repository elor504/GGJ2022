using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDInteractionsHandler : MonoBehaviour
{
    private AudioSettings audioSettings;
    private HUDstate hudState;
    private bool isInventoryOpen, isPauseAvailable;
    bool isFullScreenHUD;

    #region PublicFields
    [Header("Menus")]
    public GameObject tabMenu;
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    [Header("Texts")]
    public TextMeshProUGUI pauseText;
    public TextMeshProUGUI inventoryText;

    [Header("Info tabs")]
    public GameObject infoTab;
    public GameObject inventoryTab;
    public GameObject infoLabel;
    public GameObject inventoryLabel;

    [Header("Images")]
    public GameObject ImagePause;
    public GameObject ImageInfo;
    #endregion

    void Start()
    {
        audioSettings = AudioSettings.ASInstance;

        // Defalut HUD settings
        tabMenu.SetActive(false);
        hudState = HUDstate.inventory;
        isInventoryOpen = false;
        isPauseAvailable = true;
        isFullScreenHUD = UIInteractionsHandler.GetInstance.isFullScreenMenus;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (hudState == HUDstate.options)
            {
                // Close options menu and open pause menu
                pauseMenu.SetActive(true);
                optionsMenu.SetActive(false);

            }
            if (hudState == HUDstate.info && isPauseAvailable == false || hudState == HUDstate.inventory && isPauseAvailable == false)
            {

                // Close tab menu and set state
                tabMenu.SetActive(false);
                hudState = HUDstate.inventory;

                // Change text
                pauseText.SetText("PAUSE");
                inventoryText.SetText("INVENTORY");

                // Set a flag for pause menu
                isPauseAvailable = true;
            }
            else if (isPauseAvailable == true)
            {
                // Open pause menu
                pauseMenu.SetActive(true);

                // Hide images
                ImagePause.SetActive(false);
                ImageInfo.SetActive(false);

                // Change state
                hudState = HUDstate.pause;

                // Set a flag for pause menu
                isPauseAvailable = false;

                // Freeze time
                Time.timeScale = 0f;

                // Disable player input

            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (hudState != HUDstate.pause)
            {
                // Set a flag for pause menu
                isPauseAvailable = false;

                // Activate TAB menu
                tabMenu.SetActive(true);

                // Change text
                pauseText.SetText("CLOSE");
                inventoryText.SetText("SWITCH TAB");


                if (hudState == HUDstate.info)
                {
                    // Update tab
                    UpdateTabMenu();
                    isInventoryOpen = false;

                    // Set state to next
                    hudState = HUDstate.inventory;
                }
                else if (hudState == HUDstate.inventory)
                {
                    // Update tab
                    UpdateTabMenu();
                    isInventoryOpen = true;

                    // Set state to next
                    hudState = HUDstate.info;
                }
            }
        }
    }

    public void UpdateTabMenu()
    {
        if (hudState == HUDstate.info)
        {
            infoTab.SetActive(true);
            inventoryTab.SetActive(false);

            // Visual feedback for tab change
            inventoryLabel.GetComponentInChildren<TextMeshProUGUI>().fontSize = 20;
            inventoryLabel.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            infoLabel.GetComponentInChildren<TextMeshProUGUI>().fontSize = 30;
            infoLabel.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
        }
        if (hudState == HUDstate.inventory)
        {
            inventoryTab.SetActive(true);
            infoTab.SetActive(false);

            // Visual feedback for tab change
            inventoryLabel.GetComponentInChildren<TextMeshProUGUI>().fontSize = 30;
            inventoryLabel.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
            infoLabel.GetComponentInChildren<TextMeshProUGUI>().fontSize = 20;
            infoLabel.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        }
    }
    public void Button_Continue()
    {
        // Unfreeze time
        Time.timeScale = 1f;

        // Enable player input


        // Change state
        hudState = HUDstate.inventory;

        // Close pause menu
        pauseMenu.SetActive(false);

        // Show images
        ImagePause.SetActive(true);
        ImageInfo.SetActive(true);

        // Set a flag for pause menu
        isPauseAvailable = true;
    }
    public void Button_Options()
    {
        hudState = HUDstate.options;

        // Close pause menu and open options menu
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void Button_ExitToMM()
    {
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
}

enum HUDstate
{
    info,
    inventory,
    pause,
    options
}
