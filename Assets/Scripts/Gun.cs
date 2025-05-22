using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("Basic Settings")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 1f;      // Time between shots
    public float bulletSpeed = 15f;    // Bullet velocity
    public float bulletLifetime = 10f; // How long bullets exist

    [Header("Cooldown UI")]
    public Image cooldownIndicator;   
    public Image crosshair;

    private float nextFireTime;       // Time when we can shoot next
    
    [Header("Weapon Position Animation")]
    private Transform weaponTransform;          
    public float centerX = 0.04f;
    public float defaultX = 0.3f;
    public float returnSpeed = 1f;             // How fast it returns to original position

    private enum WeaponAnimationState
    {
        Idle,
        MovingToCenter,
        HoldAtCenter,
        Returning
    }

    private WeaponAnimationState animationState = WeaponAnimationState.Idle;
    private float holdTimer = 0f;
    public float moveSpeed = 16f;      // Speed to move to center
    public float centerHoldDuration = 0.15f; // Pause time at center

    void Start()
    {
        weaponTransform = gameObject.transform;
    }
    void Update()
    {
        // Handle shooting
        if (Time.time >= nextFireTime && Input.GetButtonDown("Fire1"))
        {
            Shoot();
            nextFireTime = Time.time + fireRate;

            // Start the cooldown visual
            if (cooldownIndicator != null)
                cooldownIndicator.fillAmount = 1f;
            if (crosshair != null)
                crosshair.enabled = false;
        }

        // Update cooldown fill visually
        if (cooldownIndicator != null)
        {
            float timeLeft = nextFireTime - Time.time;
            cooldownIndicator.fillAmount = Mathf.Clamp01(timeLeft / fireRate);
        }

        if (crosshair != null && Time.time >= nextFireTime)
            crosshair.enabled = true;

        // Handle weapon animation states
        if (weaponTransform != null)
        {
            Vector3 currentPos = weaponTransform.localPosition;

            switch (animationState)
            {
                case WeaponAnimationState.MovingToCenter:
                    currentPos.x = Mathf.Lerp(currentPos.x, centerX, Time.deltaTime * moveSpeed);
                    weaponTransform.localPosition = currentPos;

                    if (Mathf.Abs(currentPos.x - centerX) < 0.005f)
                    {
                        currentPos.x = centerX;
                        weaponTransform.localPosition = currentPos;
                        animationState = WeaponAnimationState.HoldAtCenter;
                        holdTimer = centerHoldDuration;
                    }
                    break;

                case WeaponAnimationState.HoldAtCenter:
                    holdTimer -= Time.deltaTime;
                    if (holdTimer <= 0)
                    {
                        animationState = WeaponAnimationState.Returning;
                    }
                    break;

                case WeaponAnimationState.Returning:
                    currentPos.x = Mathf.Lerp(currentPos.x, defaultX, Time.deltaTime * returnSpeed);
                    weaponTransform.localPosition = currentPos;

                    if (Mathf.Abs(currentPos.x - defaultX) < 0.005f)
                    {
                        currentPos.x = defaultX;
                        weaponTransform.localPosition = currentPos;
                        animationState = WeaponAnimationState.Idle;
                    }
                    break;
            }
        }
    }

    void Shoot()
    {

        // Begin weapon animation: move to center
        if (weaponTransform != null)
        {
            animationState = WeaponAnimationState.MovingToCenter;
        }
        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Set bullet ownership
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
            bulletScript.shotByPlayer = CompareTag("Player");

        // Apply velocity
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = firePoint.forward * bulletSpeed;

        

        // Cleanup
        Destroy(bullet, bulletLifetime);
    }
}