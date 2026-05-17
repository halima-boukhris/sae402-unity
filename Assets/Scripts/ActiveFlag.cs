using UnityEngine;

public class ActiveFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Vérifie que c'est le joueur
        {
            GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
