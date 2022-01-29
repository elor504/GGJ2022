public class HowToPlayState : State
{
    InterfaceHandler interfaceHandler;

    // Getting a ref from interfaceManager to access menus containers
    public HowToPlayState()
    {
        interfaceHandler = InterfaceHandler.GetInstance;
    }

    // Overwriting an abstract method and adding the logic for this state
    public override void StateLogic()
    {
        // Changing the enum state
        interfaceHandler.uIState = UIState.HowToPlayMenu;

        // Screens logic
        interfaceHandler.MainMenu.gameObject.SetActive(false);
        interfaceHandler.OptionsMenu.gameObject.SetActive(false);
        interfaceHandler.CreditsMenu.gameObject.SetActive(false);
        interfaceHandler.HowToPlayMenu.gameObject.SetActive(true);
    }
}
