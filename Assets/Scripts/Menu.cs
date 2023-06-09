using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using System.Globalization;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    static public int coins;
    static public int lifes;
    static public int score;
    static public int highScore;
    static public int activePanel = 1;
    public Customization customization;
    public Saving saving;
    public Settings settings;
    public ShortAd shortAd;
    [Header("Panels")]
    public GameObject customizePanel;
    public GameObject playPanel;
    public GameObject shopPanel;
    public GameObject settingsPanel;
    public GameObject difficultyPanel;
    public GameObject challengesPanel;
    [Header("Footer")]
    public GameObject[] footerPanels;
    public GameObject customizeButton;
    public GameObject homeButton;
    public GameObject shopButton;
    public GameObject settingsButton;
    [Header("Cat")]
    public GameObject hat;
    public GameObject face;
    public GameObject body;
    public GameObject tail;
    [Header("Cats")]
    public GameObject catMenu;
    public GameObject catCustomization;
    [Header("Texts")]
    public LocalizeStringEvent highScoreText;
    public LocalizeStringEvent scoreText;
    public Text coinsText;
    public Text lifesText;
    [Header("Audio")]
    public AudioClip[] footerSounds;
    public AudioSource footerAudio;

    static public int gamesPlayedOnSession = 0;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        customization.CalculateBoughtCount();
        CheckHighScore();
        UpdateCoins();
        UpdateLifes();
        UpdateHighScore();
        UpdateScore();
        UpdateCat();
        settings.CheckForDarkTheme();
        settings.CheckForSounds();
        settings.CheckForMusic();

        if (gamesPlayedOnSession > 0 && gamesPlayedOnSession % 3 == 0)
        {
            shortAd.ShowAd();
        }
    }
    private void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }
    }
    public void ChooseDifficulty()
    {
        difficultyPanel.SetActive(true);
        difficultyPanel.GetComponent<Animation>().Play("difficultyOpen");
    }
    public void ChallengesOpen()
    {
        challengesPanel.SetActive(true);
        challengesPanel.GetComponent<Animation>().Play("challengesOpen");
    }
    public void CloseDifficulty()
    {
        StartCoroutine(CloseDifficultyTemp());
    }
    IEnumerator CloseDifficultyTemp()
    {
        difficultyPanel.GetComponent<Animation>().Play("difficultyClose");
        yield return new WaitForSeconds(0.5f);
        difficultyPanel.SetActive(false);
        OpenActivePanel();
    }
    public void ChallengesClose()
    {
        StartCoroutine(ChallengesCloseTemp());
    }
    IEnumerator ChallengesCloseTemp()
    {
        challengesPanel.GetComponent<Animation>().Play("challengesClose");
        yield return new WaitForSeconds(0.5f);
        challengesPanel.SetActive(false);
        OpenActivePanel();
    }
    private void UpdateHighScore()
    {
        highScoreText.StringReference.Add("x",
                new IntVariable { Value = highScore });
        highScoreText.StringReference.RefreshString();
    }
    private void UpdateScore()
    {
        scoreText.StringReference.Add("x",
                new IntVariable { Value = score });
        scoreText.StringReference.RefreshString();
    }
    public void UpdateCoins()
    {
        coinsText.text = coins.ToString("N0", new CultureInfo("en-us"));
    }
    public void UpdateLifes()
    {
        lifesText.text = lifes.ToString("N0", new CultureInfo("en-us"));
    }
    private void UpdateCat()
    {
        UpdateHat();
        body.GetComponent<Image>().sprite = customization.bodies[Customization.bodyChosen];
        tail.GetComponent<Image>().sprite = customization.tails[Customization.bodyChosen];
        face.GetComponent<Image>().sprite = customization.faces[Customization.faceChosen];
    }
    private void UpdateHat()
    {
        hat.GetComponent<Image>().sprite = customization.hats[Customization.hatChosen];
        hat.GetComponent<RectTransform>().localScale *= 0.5f;
        if (customization.hats[Customization.hatChosen].name[0] == 'B')
        {
            hat.GetComponent<RectTransform>().localScale = Customization.bigHatScale;
            hat.GetComponent<RectTransform>().anchoredPosition = Customization.bigHatPos;
        }
        else
        {
            hat.GetComponent<RectTransform>().localScale = Customization.smallHatScale;
            hat.GetComponent<RectTransform>().anchoredPosition = Customization.smallHatPos;
        }
    }
    public void OpenActivePanel()
    {
        //CloseAllButActivePanel();
        footerAudio.clip = footerSounds[activePanel];
        footerAudio.Play();
        StartCoroutine(OpenPanel(activePanel));
    }
    public void CloseActivePanel()
    {
        StartCoroutine(ClosePanel(activePanel));
    }
    IEnumerator ClosePanel(int id)
    {
        switch (id)
        {
            case 0:
                customizePanel.GetComponent<Animation>().Play("customizeClose");
                yield return new WaitForSeconds(0.5f);
                customizePanel.SetActive(false);
                if (Customization.isChanged == true)
                {
                    saving.Save();
                    Customization.isChanged = false;
                }
                break;
            case 1:
                playPanel.GetComponent<Animation>().Play("playClose");
                yield return new WaitForSeconds(0.5f);
                playPanel.SetActive(false);
                break;
            case 2:
                shopPanel.GetComponent<Animation>().Play("shopClose");
                yield return new WaitForSeconds(0.5f);
                shopPanel.SetActive(false);
                customization.CalculateBoughtCount();
                if (Shop.isChanged == true)
                {
                    saving.Save();
                    Shop.isChanged = false;
                }
                break;
            case 3:
                settingsPanel.GetComponent<Animation>().Play("settingsClose");
                yield return new WaitForSeconds(0.5f);
                settingsPanel.SetActive(false);
                if (Settings.isChanged == true)
                {
                    saving.Save();
                    Settings.isChanged = false;
                }
                break;
        }
    }
    IEnumerator OpenPanel(int id)
    {
        switch (id)
        {
            case 0:
                customizePanel.SetActive(true);
                customizePanel.GetComponent<Animation>().Play("customizeOpen");
                yield return new WaitForSeconds(0.5f);
                break;
            case 1:
                UpdateCat();
                playPanel.SetActive(true);
                playPanel.GetComponent<Animation>().Play("playOpen");
                yield return new WaitForSeconds(0.5f);
                break;
            case 2:
                shopPanel.SetActive(true);
                shopPanel.GetComponent<Animation>().Play("shopOpen");
                yield return new WaitForSeconds(0.5f);
                break;
            case 3:
                settingsPanel.SetActive(true);
                settingsPanel.GetComponent<Animation>().Play("settingsOpen");
                yield return new WaitForSeconds(0.5f);
                break;
        }
    }
}
