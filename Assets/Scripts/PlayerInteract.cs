using UnityEngine;
using StarterAssets;
public class PlayerInteract : MonoBehaviour
{
    private StarterAssetsInputs starterAssetsInputs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (starterAssetsInputs.scan)
        {
            Debug.Log("Scan key was detected!");
            float interactRange = 2.0f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            Debug.Log("Found " + colliderArray.Length + " colliders nearby.");
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent<NPCInteractable>(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact();
                }
            }
            starterAssetsInputs.scan = false;
        }
    }
}
