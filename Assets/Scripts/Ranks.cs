using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class Ranks : MonoBehaviour
{
    [Header("Ranks")]
    public Sprite[] ranks;
    [Header("Easy")]
    public Image rankEasy;
    public LocalizeStringEvent winLoseRateEasyText;
    public Slider sliderEasy;
    [Header("Medium")]
    public Image rankMedium;
    public LocalizeStringEvent winLoseRateMediumText;
    public Slider sliderMedium;
    [Header("Hard")]
    public Image rankHard;
    public LocalizeStringEvent winLoseRateHardText;
    public Slider sliderHard;

    public void OnEnable()
    {
        UpdateRanks();
    }
    private void UpdateRanks()
    {
        UpdateRankEasy();
        UpdateRankMedium();
        UpdateRankHard();
    }
    private void UpdateRankEasy()
    {
        float wlRate = (float)Settings.gamesWonEasy / Settings.gamesPlayedEasy * 100;
        if (Settings.gamesPlayedEasy == 0)
            wlRate = 0;
        winLoseRateEasyText.StringReference.Add("x",
                new FloatVariable { Value = (float)System.Math.Round(wlRate, 1) });
        winLoseRateEasyText.StringReference.Add("w",
            new IntVariable { Value = Settings.gamesWonEasy });
        winLoseRateEasyText.StringReference.Add("l",
            new IntVariable { Value = Settings.gamesPlayedEasy - Settings.gamesWonEasy });
        winLoseRateEasyText.StringReference.RefreshString();

        rankEasy.sprite = GetRank(wlRate);

        sliderEasy.maxValue = Settings.gamesPlayedEasy;
        sliderEasy.value = Settings.gamesWonEasy;
    }
    private void UpdateRankMedium()
    {
        float wlRate = (float)Settings.gamesWonMedium / Settings.gamesPlayedMedium * 100;
        if (Settings.gamesPlayedMedium == 0)
            wlRate = 0;
        winLoseRateMediumText.StringReference.Add("x",
                new FloatVariable { Value = (float)System.Math.Round(wlRate, 1) });
        winLoseRateMediumText.StringReference.Add("w",
            new IntVariable { Value = Settings.gamesWonMedium });
        winLoseRateMediumText.StringReference.Add("l",
            new IntVariable { Value = Settings.gamesPlayedMedium - Settings.gamesWonMedium });
        winLoseRateMediumText.StringReference.RefreshString();

        rankMedium.sprite = GetRank(wlRate);

        sliderMedium.maxValue = Settings.gamesPlayedMedium;
        sliderMedium.value = Settings.gamesWonMedium;
    }
    private void UpdateRankHard()
    {
        float wlRate = (float)Settings.gamesWonHard / Settings.gamesPlayedHard * 100;
        if (Settings.gamesPlayedHard == 0)
            wlRate = 0;
        winLoseRateHardText.StringReference.Add("x",
                new FloatVariable { Value = (float)System.Math.Round(wlRate, 1) });
        winLoseRateHardText.StringReference.Add("w",
            new IntVariable { Value = Settings.gamesWonHard });
        winLoseRateHardText.StringReference.Add("l",
            new IntVariable { Value = Settings.gamesPlayedHard - Settings.gamesWonHard });
        winLoseRateHardText.StringReference.RefreshString();

        rankHard.sprite = GetRank(wlRate);

        sliderHard.maxValue = Settings.gamesPlayedHard;
        sliderHard.value = Settings.gamesWonHard;
    }
    public void RanksOpen()
    {
        gameObject.SetActive(true);
    }
    public void RanksClose()
    {
        gameObject.SetActive(false);
    }

    private Sprite GetRank(float wlRate)
    {
        switch (wlRate)
        {
            case < 8:
                return ranks[0];
            case < 16:
                return ranks[1];
            case < 24:
                return ranks[2];
            case < 32:
                return ranks[3];
            case < 40:
                return ranks[4];
            case < 48:
                return ranks[5];
            case < 56:
                return ranks[6];
            case < 64:
                return ranks[7];
            case < 72:
                return ranks[8];
            case < 80:
                return ranks[9];
            case < 88:
                return ranks[10];
            case < 96:
                return ranks[11];
            default:
                return ranks[12];
        }
    }
}
