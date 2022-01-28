using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceHandler : MonoBehaviour
{
    #region PublicFields
    private static InterfaceHandler IHInstance;
    public static InterfaceHandler GetInstance => IHInstance;
    [HideInInspector] public UIState uIState;
    #region ButtonImages
    [Header("Button sprites")]
    public Sprite OnHoverSprite;
    public Sprite OnPressSprite;
    #endregion
    [Header("Menus")]
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject CreditsMenu;
    #endregion



    private void Awake()
    {
        if (IHInstance == null)
        {
            IHInstance = this;
        }
        else if (IHInstance != this)
        {
            Destroy(this.gameObject);
        }

        // FPS lock
        Application.targetFrameRate = 200;
    }
}
