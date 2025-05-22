using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Die()
    {
        animator.SetBool("isDead", true);
    }
}