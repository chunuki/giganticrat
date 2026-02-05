using UnityEngine;

public class AkioStateMachine
{
    public AkioStateMachine[] states;
    public AkioAgent agent;
    public AkioStateId currentState;

    public AkioStateMachine(AkioAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(AkioStateId)).Length;
        states = new AkioState[numStates];
    }

    public void RegisterState(AkioState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }

    public AkioState GetState(AkioStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }

    public void Update()
    {
        // do nothing
    }
}