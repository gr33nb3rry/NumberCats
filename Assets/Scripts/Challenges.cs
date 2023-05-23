using UnityEngine;
using UnityEngine.UI;

public class Challenges : MonoBehaviour
{
    static public int coinFlipPlayed = 0;
    [Header("Panels")]
    public GameObject challengesPanel;
    public GameObject coinFlipPanel;
    [Header("Progress")]
    public Slider progressSlider;
    public Text progressLeftText;
    public Text progressRightText;
    public Text progressCurrentText;
    [Header("Texts")]
    public Text coinFlipCountText;

    private int coinFlipTicketsCount = 0;

    public void OnEnable()
    {
        UpdateChallenges();
        UpdateProgress();
    }
    private void UpdateChallenges()
    {
        coinFlipTicketsCount =
            (Settings.gamesPlayedEasy + Settings.gamesPlayedMedium + Settings.gamesPlayedHard) / 3 - coinFlipPlayed;
        coinFlipCountText.text = coinFlipTicketsCount.ToString();
    }
    private void UpdateProgress()
    {
        int gamesPlayed =
            Settings.gamesPlayedEasy + Settings.gamesPlayedMedium + Settings.gamesPlayedHard;
        progressCurrentText.text =
            gamesPlayed.ToString();
        progressLeftText.text =
            (gamesPlayed / 3 * 3).ToString();
        progressRightText.text =
            (gamesPlayed / 3 * 3 + 3).ToString();

        progressSlider.minValue = gamesPlayed / 3 * 3;
        progressSlider.maxValue = gamesPlayed / 3 * 3 + 3;
        progressSlider.value =
            (Settings.gamesPlayedEasy + Settings.gamesPlayedMedium + Settings.gamesPlayedHard);
    }
    public void coinFlip()
    {
        if (coinFlipTicketsCount > 0)
        {
            coinFlipPlayed++;
            UpdateChallenges();
            Invoke("coinFlipOpen", 0.25f);
        }
    }
    private void coinFlipOpen()
    {
        challengesPanel.SetActive(true);
        coinFlipPanel.SetActive(true);
    }
}
