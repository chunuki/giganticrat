using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using System.Collections;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera defaultCamera;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private float attackSpeed = 1f;

    private float timeUntilNextAttack = 0f;
    private bool aimToggle;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    public InputAction attack;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attack = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        bool animatorIsShooting = animator.GetBool("isShooting");

        //// DONT PISS ME OFF
        //bool isPlayingInLayer1 = animator.GetCurrentAnimatorStateInfo(1).IsName("Shoot");
        //bool isPlayingInLayer2 = animator.GetCurrentAnimatorStateInfo(2).IsName("Shoot");
        //bool isTransitioning = animator.IsInTransition(1) || animator.IsInTransition(2);

        if (animatorIsShooting /*&& !isPlayingInLayer1 && !isPlayingInLayer2 && !isTransitioning*/)
        {
            ResetMovementSpeed();
        }

        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (starterAssetsInputs.aimToggle)
        {
            aimToggle = !aimToggle;
            starterAssetsInputs.aimToggle = false;
        }

        // if hold right click or R is on or currently in shooting animation --> zoom in/face target
        if (starterAssetsInputs.aim || aimToggle || animatorIsShooting)
        {
            // Only activate camera and weight if actually aiming, but keep rotation lock if shooting
            if (starterAssetsInputs.aim || aimToggle)
            {
                aimVirtualCamera.gameObject.SetActive(true);
                thirdPersonController.SetSensitivity(aimSensitivity);
                animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 13f));
            }

            thirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = aimDirection;
        }
        // if do not hold right click and R is off --> zoom out
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 13f));
        }

        if (Time.time >= timeUntilNextAttack)
        {
            if (attack.WasPressedThisFrame())
            {
                animator.SetBool("isShooting", true);

                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                StartCoroutine(SpawnBulletWithDelay(mouseWorldPosition));

                timeUntilNextAttack = Time.time + attackSpeed;
            }
        }
    }

    private IEnumerator SpawnBulletWithDelay(Vector3 targetPosition)
    {
        // delay bullet spawn to not whack before animation lines up
        for (int i = 0; i < 5; i++)
        {
            yield return null;
        }

        Vector3 aimDir = (targetPosition - spawnBulletPosition.position).normalized;
        Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }

    // animation event to make woman move again
    public void ResetMovementSpeed()
    {
        animator.SetBool("isShooting", false);
    }
}