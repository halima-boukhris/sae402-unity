using System.Collections;
using UnityEngine;
 
public class BlockHit : MonoBehaviour
{
    // Pour le cours, limiter le nombre d'interactions possible avec le bloc
    // Et quand c'est atteint, changer l'opacité du sprite (voir ligne 15)
    private SpriteRenderer sr;
    private Animator animator;
 
    [SerializeField]
    private int maxHits = 2;
 
    [SerializeField]
    private bool isHidden = false;
 
    private PlatformEffector2D pe2d;
 
    private int currentHits = 0;
 
    private bool isAnimating = false;
 
    [SerializeField]
    private GameObject itemPrefab;
 
    void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        pe2d = GetComponent<PlatformEffector2D>();
 
        if (isHidden)
        {
            pe2d.enabled = true;
            sr.color = Color.clear;
        } else
        {
            pe2d.enabled = false;
            sr.color = Color.white;
        }
    }
 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
 
        ContactPoint2D contact = collision.GetContact(0);
 
        if (contact.normal.y > 0.5f && currentHits < maxHits && !isAnimating)
        {
            currentHits = currentHits + 1;
            animator.SetTrigger("Hit");
            pe2d.enabled = false;
            sr.color = Color.white;
 
            if (itemPrefab != null)
            {
                StartCoroutine(Hit());
            }
 
            if (currentHits == maxHits)
            {
                sr.color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
    }
 
    IEnumerator Hit()
    {
        isAnimating = true;
        GameObject item = Instantiate(
            itemPrefab,
            transform.position,
            Quaternion.identity
        );
        Collectible collectible = item.GetComponent<Collectible>();
        yield return item.transform.MoveBackAndForth(
            item.transform.localPosition + Vector3.up * 1.5f
        );
        collectible.Picked();
        isAnimating = false;
    }
}