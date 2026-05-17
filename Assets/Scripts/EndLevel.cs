using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EndLevel : MonoBehaviour
{
    public ParticleSystem particles;
    public AudioClip audioClip;

    [Space (10)]
    [Header("Scene's name to load after the collider is triggered")]
    public string nextLevelName;
    [Space (10)]

    [Header("Broadcast event channels")]
    public StringEventChannel onLevelEnded;
    public PlaySoundAtEventChannel sfxAudioChannel;

    private bool hasBeenTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasBeenTriggered)
        {
            hasBeenTriggered = true;

            if (!string.IsNullOrEmpty(nextLevelName))
            {
                // On vérifie chaque objet avant de l'utiliser pour éviter le "NullReference"
                if (particles != null) particles.Play();

                if (sfxAudioChannel != null) sfxAudioChannel.Raise(audioClip, transform.position);

                if (onLevelEnded != null) onLevelEnded.Raise(nextLevelName);
            } 
            else 
            {
                Debug.LogError("Nom du prochain niveau (nextLevelName) manquant dans l'Inspector !");
            }
        }
    }
}
