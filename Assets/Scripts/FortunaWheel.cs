using UnityEngine;
using UnityEngine.UI;

public class FortunaWheel : MonoBehaviour
{
    public Menu menu;
    public Saving saving;
    public GameObject endpanel;
    public GameObject challengesPanel;

    public GameObject spinButton;
    public RectTransform wheelCells;
    public GameObject wheel;

    public Text rewardText;
    public Image rewardImage;
    public AudioClip winSound;
    public AudioSource audioSource;
    private int reward;
    public void OnEnable()
    {
        spinButton.SetActive(true);
        wheelCells.rotation = Quaternion.identity;
    }
    public void Spin()
    {
        wheel.GetComponent<Animation>().Play();
        spinButton.SetActive(false);
    }
    private void End()
    {
    }
    public void Collect()
    {
        Menu.coins += reward;
        menu.UpdateCoins();
        endpanel.SetActive(false);
        gameObject.SetActive(false);
        challengesPanel.SetActive(false);
        saving.Save();
    }
}
