using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        WalkingAnimation();
    }

    //Do walk animation
    private void WalkingAnimation()
    {
        if (animator != null)
        {
            animator.SetBool(IS_WALKING, player.IsWalking());
        }
    }
}