using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelCommandes;

    public void HideTutorial()
    {
        panelCommandes.SetActive(false);
    }
}
