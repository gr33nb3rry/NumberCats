using System.Linq;
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
    public Text[] wheelRewardsText;

    public Text rewardText;
    public Image rewardImage;
    public AudioClip winSound;
    public AudioSource audioSource;
    private int reward;
    private int[] wheelRewards = new int[] {300,1,150,2,50,3,10,4};
    public void OnEnable()
    {
        spinButton.SetActive(true);
        wheelCells.rotation = Quaternion.identity;
        UpdateRewards();
    }
    private void UpdateRewards()
    {
        for (int i = 0; i < wheelRewardsText.Length; i++)
        {
            wheelRewardsText[i].text = wheelRewards[i].ToString();
        }
    }
    public void Spin()
    {
        wheel.GetComponent<Animation>().Play();
        spinButton.SetActive(false);
        Invoke("Mix", 1);
    }
    private void Mix()
    {
        System.Random random = new System.Random();
        wheelRewards = wheelRewards.OrderBy(x => random.Next()).ToArray();
        UpdateRewards();
    }
    private void End()
    {
    }
    public void Collect()
    {
        menu.UpdateCoins();
        menu.UpdateLifes();
        endpanel.SetActive(false);
        gameObject.SetActive(false);
        challengesPanel.SetActive(false);
        saving.Save();
    }
}
