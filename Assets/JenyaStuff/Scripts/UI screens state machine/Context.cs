public class Context
{
    private State _state = null;

    // Contructor to transition to the current state
    public Context(State state)
    {
        this.TransitionTo(state);
    }

    // Method that make the transition to the current state
    public void TransitionTo(State state)
    {
        this._state = state;

        // Calling the context method to change the state
        this._state.SetContext(this);
    }

    // Method to request the state logic
    public void Request()
    {
        // Calling our overwritten abstract method with the logic we need
        this._state.StateLogic();
    }
}
