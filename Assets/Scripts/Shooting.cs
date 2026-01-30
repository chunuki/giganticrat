using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{

    public InputAction attack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attack = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.WasPressedThisFrame()) 
        { 
            
        }
        
    }
}
