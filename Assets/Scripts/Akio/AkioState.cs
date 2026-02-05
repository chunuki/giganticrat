using UnityEngine;

public enum AkioStateId
{
    Neutral
}

public interface AkioState
{
    AkioStateId GetId();
    void Enter(AkioAgent agent);
    void Update(AkioAgent agent);
    void Exit(AkioAgent agent);
}

