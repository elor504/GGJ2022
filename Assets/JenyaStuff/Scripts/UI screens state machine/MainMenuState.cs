public class MainMenuState : State
{
    InterfaceHandler interfaceHandler;

    // Getting a ref from interfaceManager to access menus containers
    public MainMenuState()
    {
        interfaceHandler = InterfaceHandler.GetInstance;
    }

    // Overwriting an abstract method and adding the logic for this state
    public override void StateLogic()
    {
        // Changing the enum state
        interfaceHandler.uIState = UIState.MainMenu;

        // Screens logic
        interfaceHandler.MainMenu.gameObject.SetActive(true);
        interfaceHandler.OptionsMenu.gameObject.SetActive(false);
        interfaceHandler.CreditsMenu.gameObject.SetActive(false);
    }
}
