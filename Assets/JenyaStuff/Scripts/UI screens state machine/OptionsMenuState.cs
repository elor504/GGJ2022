public class OptionsMenuState : State
{
    InterfaceHandler interfaceHandler;

    // Getting a ref from interfaceManager to access menus containers
    public OptionsMenuState()
    {
        interfaceHandler = InterfaceHandler.GetInstance;
    }

    // Overwriting an abstract method and adding the logic for this state
    public override void StateLogic()
    {
        // Changing the enum state
        interfaceHandler.uIState = UIState.OptionsMenu;

        // Screens logic
        interfaceHandler.MainMenu.gameObject.SetActive(false);
        interfaceHandler.OptionsMenu.gameObject.SetActive(true);
        interfaceHandler.HowToPlayMenu.gameObject.SetActive(false);
    }
}
