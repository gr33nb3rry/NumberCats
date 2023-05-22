using UnityEngine;

public class Challenges : MonoBehaviour
{
    public GameObject challengesPanel;
    public GameObject coinFlipPanel;

    public void coinFlip()
    {
        Invoke("coinFlipOpen", 0.25f);
    }
    private void coinFlipOpen()
    {
        challengesPanel.SetActive(true);
        coinFlipPanel.SetActive(true);
    }
}
