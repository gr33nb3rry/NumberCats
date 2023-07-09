using UnityEngine;
using UnityEngine.UI;

public class CoinFlip : MonoBehaviour, IChallenge
{
    public Menu menu;
    public Saving saving;
    public GameObject coin;
    public GameObject endpanel;
    public GameObject challengesPanel;
    public Text rewardText;
    public AudioClip[] winLoseSounds;
    public AudioSource audioSource;
    private bool isAbleToChoose = true;
    public int rewardWin = 100;
    public int rewardLose = 50;
    private int reward;
    private bool isWon;
    public void OnEnable()
    {
        isAbleToChoose = true;
    }
    public void Choose(int choice)
    {
        if (isAbleToChoose)
        {
            isAbleToChoose = false;
            int result = Random.Range(0, 2);
            int isRotateCoin = Random.Range(0, 2);
            Debug.Log($"result {result} rotate {isRotateCoin}");
            if (isRotateCoin == 0)
            {
                coin.GetComponent<Animation>().Play($"coinFlip{result}");
            }
            else if (isRotateCoin == 1)
            {
                coin.GetComponent<Animation>().Play($"coinFlipRotate{result}");
            }

            if (choice == result)
            {
                isWon = true;
                reward = rewardWin;
            }
            else
            {
                isWon = false;
                reward = rewardLose;
            }
            Invoke("End", 2.67f);
        }
    }
    private void End()
    {
        if (isWon)
        {
            endpanel.transform.GetChild(0).gameObject.SetActive(true);
            endpanel.transform.GetChild(1).gameObject.SetActive(false);
            audioSource.clip = winLoseSounds[0];
        }
        else
        {
            endpanel.transform.GetChild(0).gameObject.SetActive(false);
            endpanel.transform.GetChild(1).gameObject.SetActive(true);
            audioSource.clip = winLoseSounds[1];
        }
        audioSource.Play();
        rewardText.text = reward.ToString();
        endpanel.SetActive(true);
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
