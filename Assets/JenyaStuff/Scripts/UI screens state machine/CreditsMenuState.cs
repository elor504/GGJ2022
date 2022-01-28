public class CreditsMenuState : State
{
    InterfaceHandler interfaceHandler;

    // Getting a ref from interfaceManager to access menus containers
    public CreditsMenuState()
    {
        interfaceHandler = InterfaceHandler.GetInstance;
    }

    // Overwriting an abstract method and adding the logic for this state
    public override void StateLogic()
    {
        // Changing the enum state
        interfaceHandler.uIState = UIState.CreditsMenu;

        // Screens logic
        interfaceHandler.MainMenu.gameObject.SetActive(false);
        interfaceHandler.CreditsMenu.gameObject.SetActive(true);
    }
}
