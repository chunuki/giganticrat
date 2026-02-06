using UnityEngine;
using StarterAssets;
using System;
public class PlayerInteract : MonoBehaviour
{

    private int menCollected = 0;

    public static event Action onMenCollectedChanged;
    private void ManCollected()
    {
        menCollected++;
    }

    public int GetMenCollected()
    {
        return menCollected;
    }
}
