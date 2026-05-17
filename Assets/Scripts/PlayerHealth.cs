using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer sr;
    public PlayerInvulnerable playerInvulnerable;
    [Tooltip("Please uncheck it on production")]
    public bool needResetHP = true;
    [Header("ScriptableObjects")]
    public PlayerData playerData;
    [Header("Debug")]
    public VoidEventChannel onDebugDeathEvent;
    [Header("Broadcast event channels")]
    public VoidEventChannel onPlayerDeath;
    public bool estMort = false;
    [Header("Camera Shake")]
    public CameraShakeEventChannel onCameraShake;
    public ShakeTypeVariable shakeInfo;
    [SerializeField]
private PlaySoundAtEventChannel onDeathSFX;
[SerializeField]
private AudioClip deathSound;
    private void Awake()
    {
        estMort = false;
        if (needResetHP || playerData.currentHealth <= 0)
        {
            playerData.currentHealth = playerData.maxHealth;
        }
    }
    private void OnEnable()
    {
        onDebugDeathEvent.OnEventRaised += Die;
    }
    public void TakeDamage(float damage)
    {
        if (playerInvulnerable.isInvulnerable && damage < float.MaxValue) return;
        playerData.currentHealth -= damage;
        onCameraShake?.Raise(shakeInfo);
        if (playerData.currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(playerInvulnerable.Invulnerable());
        }
    }
    private void Die()
    {
        estMort = true;
        if (onDeathSFX != null) onDeathSFX.Raise(deathSound, transform.position);
        onPlayerDeath?.Raise();
        GetComponent<Rigidbody2D>().simulated = false;
        transform.Rotate(0f, 0f, 45f);
        animator.SetTrigger("Death");
    }
    public void OnPlayerDeathAnimationCallback()
    {
        sr.enabled = false;
    }
    private void OnDisable()
    {
        onDebugDeathEvent.OnEventRaised -= Die;
    }
}
