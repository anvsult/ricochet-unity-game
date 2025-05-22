using JetBrains.Annotations;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool shotByPlayer = false;    // Who shot this bullet
    public bool hasRicocheted = false;   // Has this bullet bounced yet
    public float speed = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        rb.linearVelocity = rb.linearVelocity.normalized * speed;
        GameObject hitObject = collision.gameObject;
        string tag = hitObject.tag;

        if (tag == "Wall")
        {
            AudioManager.Instance.PlaySFX("WallHit");
            Destroy(gameObject);
        }
        else if (tag == "Bouncy")
        {
            AudioManager.Instance.PlaySFX("BulletBounce");
            
        }
        else if (tag == "Player")
        {
            // Only process if bullet was NOT shot by player
            if (!shotByPlayer)
            {
                // Handle bullet hit on player
                // PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                // if (playerHealth != null)
                // {
                //     playerHealth.TakeDamage();
                // }
                
                // Destroy bullet after hitting player
                Destroy(gameObject);
            }
        }
        else if (tag == "Enemy")
        {
            // Only damage enemy if shot by player AND has ricocheted
            // if (shotByPlayer && hasRicocheted)
            if (hasRicocheted)
            {
                AudioManager.Instance.PlaySFX("EnemyHit");
                hitObject.GetComponent<Enemy>().Die();
                Destroy(gameObject);

            }
            else
            {
                AudioManager.Instance.PlaySFX("WallHit");
            }
        }
        // else if (collision.gameObject.CompareTag("Wall"))
        // {
        //     // Mark bullet as ricocheted
        //     hasRicocheted = true;
            
        //     // Calculate ricochet direction (basic reflection)
        //     Vector3 reflectDir = Vector3.Reflect(GetComponent<Rigidbody>().linearVelocity.normalized, collision.contacts[0].normal);
            
        //     // Apply ricochet velocity
        //     GetComponent<Rigidbody>().linearVelocity = reflectDir * GetComponent<Rigidbody>().linearVelocity.magnitude;
        // }
        hasRicocheted = true;
    }
    
}