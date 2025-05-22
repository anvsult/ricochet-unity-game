using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isDead = false;

    public void Die()
    {

        if (isDead) return;
        isDead = true;

        GetComponent<AnimationStateController>().Die();
        GetComponent<Collider>().enabled = false;
        tag = "DeadEnemy";
        Destroy(gameObject, 2f);
    }
}
