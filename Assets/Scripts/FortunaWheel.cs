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
    public Image[] wheelRewardsImages;
    public Sprite[] coinsLifesImages;

    public Text rewardText;
    public Image rewardImage;
    public AudioClip winSound;
    public AudioSource audioSource;
    private int reward;
    private int[] wheelRewardsCoins = new int[] {300,150,50,10};
    private int[] wheelRewardsLifes = new int[] {1, 2, 3, 4};
    private int isRewardCoins;
    public void OnEnable()
    {
        spinButton.SetActive(true);
        wheelCells.rotation = Quaternion.identity;
        UpdateRewards(1);
        MixImagesToCoin();
    }
    private void UpdateRewards(int isRewardCoins)
    {
        for (int i = 0; i < wheelRewardsText.Length / 2; i++)
        {
            if (isRewardCoins == 0)
            {
                wheelRewardsText[i * 2].text = wheelRewardsLifes[i].ToString();
                wheelRewardsText[i * 2 + 1].text = wheelRewardsCoins[i].ToString();
            }
            else
            {
                wheelRewardsText[i * 2].text = wheelRewardsCoins[i].ToString();
                wheelRewardsText[i * 2 + 1].text = wheelRewardsLifes[i].ToString();
            }
        }
    }
    public void Spin()
    {
        wheel.GetComponent<Animation>().Play();
        spinButton.SetActive(false);
        Invoke("Mix", 1);
        Invoke("Win", 5);
    }
    private void Mix()
    {
        System.Random random = new System.Random();
        isRewardCoins = Random.Range(0, 2);
        wheelRewardsCoins = wheelRewardsCoins.OrderBy(x => random.Next()).ToArray();
        wheelRewardsLifes = wheelRewardsLifes.OrderBy(x => random.Next()).ToArray();

        UpdateRewards(isRewardCoins);
        if (isRewardCoins == 1)
        {
            Debug.Log(isRewardCoins);
            MixImagesToCoin();
        }
        else
        {
            Debug.Log(isRewardCoins);
            MixImagesToLife();
        }
    }
    private void MixImagesToCoin()
    {
        for (int i = 0; i < wheelRewardsImages.Length / 2; i++)
        {
            wheelRewardsImages[i * 2].sprite = coinsLifesImages[0];
            wheelRewardsImages[i * 2 + 1].sprite = coinsLifesImages[1];
        }
    }
    private void MixImagesToLife()
    {
        for (int i = 0; i < wheelRewardsImages.Length / 2; i++)
        {
            wheelRewardsImages[i * 2].sprite = coinsLifesImages[1];
            wheelRewardsImages[i * 2 + 1].sprite = coinsLifesImages[0];
        }
    }
    private void Win()
    {
        if (isRewardCoins == 1)
        {
            reward = wheelRewardsCoins[1];
            rewardImage.sprite = coinsLifesImages[0];
        }
        else
        {
            reward = wheelRewardsLifes[1];
            rewardImage.sprite = coinsLifesImages[1];
        }
        rewardText.text = reward.ToString();
        endpanel.SetActive(true);
    }
    public void Collect()
    {
        GiveReward();
        menu.UpdateCoins();
        menu.UpdateLifes();
        endpanel.SetActive(false);
        gameObject.SetActive(false);
        challengesPanel.SetActive(false);
        saving.Save();
    }
    private void GiveReward()
    {
        if (isRewardCoins == 1)
        {
            Menu.coins += reward;
        }
        else
        {
            Menu.lifes += reward;
        }
    }
}
