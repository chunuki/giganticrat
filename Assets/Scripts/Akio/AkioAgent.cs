using UnityEngine;

public class AkioAgent : MonoBehaviour
{

    public AkioStateMachine fsm;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fsm = new AkioStateMachine(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
