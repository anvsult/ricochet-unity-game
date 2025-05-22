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
        string colliderTag = hitObject.tag;

        if (colliderTag == "Wall")
        {
            AudioManager.Instance.PlaySFX("WallHit");
            Destroy(gameObject);
        }
        else if (colliderTag == "Bouncy")
        {
            AudioManager.Instance.PlaySFX("BulletBounce");

        }
        else if (colliderTag == "Enemy")
        {
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
        hasRicocheted = true;
    }

}