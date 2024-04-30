using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Animator animator;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip runClip;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] ParticleSystem moveParticle;
    [SerializeField] ParticleSystem jumpParticle;
    [SerializeField] ParticleSystem landParticle;




    private bool isGrounded;

    private void OnEnable()
    {
        player.OnJump += OnPlayerJump;
        player.OnGroundChange += OnGroundChange;
        if (moveParticle) moveParticle.Play();
    }

    private void OnDisable()
    {
        player.OnJump -= OnPlayerJump;
        player.OnGroundChange -= OnGroundChange;
        if (moveParticle) moveParticle.Stop();
    }


    private void Update()
    {
        player.transform.localScale = new Vector3(player.GetLastPlayerDirection(), 1, 1);
        animator.SetFloat("Movement", player.GetHorizontalInput());

        if (audioSource == null) return;
        if (isGrounded)
        {
            if (!audioSource.isPlaying && player.GetHorizontalInput() > 0)
            {
                audioSource.clip = runClip;
                audioSource.Play();
            }

            if (audioSource.isPlaying && player.GetHorizontalInput() <= 0)
            {
                if (audioSource.clip == runClip) audioSource.Stop();
            }
        }
    }


    private void OnPlayerJump()
    {
        animator.ResetTrigger("Jump");
        animator.SetTrigger("Jump");
        if (audioSource == null) return;
        audioSource.clip = jumpClip;
        audioSource.Play();
        if (jumpParticle) jumpParticle.Play();
    }
    private void OnGroundChange(bool isGrounded)
    {
        this.isGrounded = isGrounded;

        if (isGrounded)
        {
            animator.SetBool("IsGrounded", true);
            if (moveParticle) moveParticle.Play();
            if (landParticle) landParticle.Play();
        }
        else
        {
            animator.SetBool("IsGrounded", false);
            if (moveParticle) moveParticle.Stop();
            if (landParticle) landParticle.Stop();
        }
    }
}
