public abstract class State
{
    protected Context _context;

    // Method to change the context
    public void SetContext(Context context)
    {
        this._context = context;
    }

    // Abstract method to overwrite in different states
    public abstract void StateLogic();
}

// Creating enum state machine
public enum UIState
{
    MainMenu,
    OptionsMenu,
    CreditsMenu,
    CompletionMenu
}
