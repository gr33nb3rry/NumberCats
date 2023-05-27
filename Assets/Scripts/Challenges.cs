using UnityEngine;
using UnityEngine.UI;

public class Challenges : MonoBehaviour
{
    static public int coinFlipPlayed = 0;
    static public int wheelPlayed = 0;
    [Header("Panels")]
    public GameObject challengesPanel;
    public GameObject coinFlipPanel;
    public GameObject wheelPanel;
    [Header("Progress")]
    public Slider progressSlider;
    public Text progressLeftText;
    public Text progressRightText;
    public Text progressCurrentText;
    [Header("Texts")]
    public Text coinFlipCountText;
    public Text wheelCountText;

    private int coinFlipTicketsCount = 0;
    private int wheelTicketsCount = 0;

    public void OnEnable()
    {
        UpdateChallenges();
        UpdateProgress();
    }
    private void UpdateChallenges()
    {
        coinFlipTicketsCount =
            (Settings.gamesPlayedEasy + Settings.gamesPlayedMedium + Settings.gamesPlayedHard) / 3 - coinFlipPlayed;
        wheelTicketsCount =
            (Settings.gamesPlayedEasy + Settings.gamesPlayedMedium + Settings.gamesPlayedHard) / 3 - wheelPlayed;
        coinFlipCountText.text = coinFlipTicketsCount.ToString();
        wheelCountText.text = wheelTicketsCount.ToString();
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
    public void Wheel()
    {
        if (wheelTicketsCount > 0)
        {
            wheelPlayed++;
            UpdateChallenges();
            Invoke("WheelOpen", 0.25f);
        }
    }
    private void WheelOpen()
    {
        challengesPanel.SetActive(true);
        wheelPanel.SetActive(true);
    }
}
