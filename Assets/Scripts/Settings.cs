using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool isStart;
    static public int language = 0;
    static public bool isDarkThemeOn = false;
    static public bool isSoundsOn = true;
    static public bool isMusicOn = true;
    static public int controls = 0;
    static public int gamesPlayedEasy;
    static public int gamesWonEasy;
    static public int gamesPlayedMedium;
    static public int gamesWonMedium;
    static public int gamesPlayedHard;
    static public int gamesWonHard;
    static public bool isChanged = false;
    static public string playerID = "";

    private Color32 greyColor = new Color32(128, 128, 128, 255);
    private Color32 greenColor = new Color32(0, 255, 68, 255);
    public Color32 lightThemeColor = Color.white;
    public Color32 darkThemeColor = Color.black;
    private bool languageActive = false;

    [Header("Visuals")]
    public GameObject darkThemeSlider;
    public Image[] themePanels;
    public Text[] themeTexts;
    [Header("Sounds")]
    public GameObject SoundsSlider;
    public GameObject MusicSlider;
    public AudioSource[] audioSoundsSources;
    public AudioSource audioMusicSource;
    [Header("Controls")]
    public GameObject controlsLeft;
    public GameObject controlsRight;
    [Header("language")]
    public GameObject englishSelect;
    public GameObject frenchSelect;
    public GameObject latvianSelect;
    public GameObject russianSelect;
    [Header("Other")]
    public GameObject RulesPanel;
    public Text idText;

    public void OnEnable()
    {
        if (isStart == false)
            UpdateSettings();
    }
    private void UpdateSettings()
    {
        UpdateDarkTheme();
        UpdateSounds();
        UpdateMusic();
        UpdateControls();
        UpdateLanguage();
        idText.text = "ID: " + playerID;
    }
    private void UpdateDarkTheme()
    {
        if (isDarkThemeOn)
            darkThemeSlider.GetComponent<Animation>().Play("settingsSliderON");
        else
            darkThemeSlider.GetComponent<Animation>().Play("settingsSliderOFF");
    }
    private void UpdateSounds()
    {
        if (isSoundsOn)
            SoundsSlider.GetComponent<Animation>().Play("settingsSliderON");
        else
            SoundsSlider.GetComponent<Animation>().Play("settingsSliderOFF");
    }
    private void UpdateMusic()
    {
        if (isMusicOn)
            MusicSlider.GetComponent<Animation>().Play("settingsSliderON");
        else
            MusicSlider.GetComponent<Animation>().Play("settingsSliderOFF");
    }
    private void UpdateControls()
    {
        if (controls == 0)
        {
            controlsLeft.GetComponent<Image>().color = greenColor;
            controlsRight.GetComponent<Image>().color = greyColor;
        }
        else
        {
            controlsLeft.GetComponent<Image>().color = greyColor;
            controlsRight.GetComponent<Image>().color = greenColor;
        }
    }
    private void UpdateLanguage()
    {
        if (language == 0)
        {
            englishSelect.SetActive(true);
            frenchSelect.SetActive(false);
            latvianSelect.SetActive(false);
            russianSelect.SetActive(false);
        }
        else if (language == 1)
        {
            englishSelect.SetActive(false);
            frenchSelect.SetActive(true);
            latvianSelect.SetActive(false);
            russianSelect.SetActive(false);
        }
        else if (language == 2)
        {
            englishSelect.SetActive(false);
            frenchSelect.SetActive(false);
            latvianSelect.SetActive(true);
            russianSelect.SetActive(false);
        }
        else if (language == 3)
        {
            englishSelect.SetActive(false);
            frenchSelect.SetActive(false);
            latvianSelect.SetActive(false);
            russianSelect.SetActive(true);
        }
    }

    public void CheckForDarkTheme()
    {
        if (isDarkThemeOn)
        {
            for (int i = 0; i < themePanels.Length; i++)
            {
                themePanels[i].color = darkThemeColor;
            }
            for (int i = 0; i < themeTexts.Length; i++)
            {
                themeTexts[i].color = lightThemeColor;
            }
        }
        else
        {
            for (int i = 0; i < themePanels.Length; i++)
            {
                themePanels[i].color = lightThemeColor;
            }
            for (int i = 0; i < themeTexts.Length; i++)
            {
                themeTexts[i].color = darkThemeColor;
            }
        }
    }
    public void CheckForSounds()
    {
        if (isSoundsOn)
        {
            for (int i = 0; i < audioSoundsSources.Length; i++)
            {
                audioSoundsSources[i].volume = 1;
            }
        }
        else
        {
            for (int i = 0; i < audioSoundsSources.Length; i++)
            {
                audioSoundsSources[i].volume = 0;
            }
        }
    }
    public void CheckForMusic()
    {
        if (isMusicOn)
        {
            audioMusicSource.volume = 0.3f;
        }
        else
        {
            audioMusicSource.volume = 0;
        }
    }
    public void DarkThemeOnOff()
    {
        isDarkThemeOn = !isDarkThemeOn;
        isChanged = true;
        UpdateDarkTheme();
        CheckForDarkTheme();
    }
    public void SoundsOnOff()
    {
        isSoundsOn = !isSoundsOn;
        isChanged = true;
        UpdateSounds();
        CheckForSounds();
    }
    public void MusicOnOff()
    {
        isMusicOn = !isMusicOn;
        isChanged = true;
        UpdateMusic();
        CheckForMusic();
    }
    public void ControlsToLeft()
    {
        controls = 0;
        isChanged = true;
        UpdateControls();
    }
    public void ControlsToRight()
    {
        controls = 1;
        isChanged = true;
        UpdateControls();
    }
    public void LanguageChangeOnStart()
    {
        StartCoroutine(SetLocale(language));
    }
    public void LanguageChange(int id)
    {
        if (languageActive == true)
            return;
        StartCoroutine(SetLocale(id));
        language = id;
        isChanged = true;
        UpdateLanguage();
    }
    IEnumerator SetLocale(int id)
    {
        languageActive = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
        languageActive = false;
    }
    public void RulesOpen()
    {
        RulesPanel.SetActive(true);
    }
    public void RulesClose()
    {
        RulesPanel.SetActive(false);
    }
    public void CloseSettings()
    {
        gameObject.SetActive(false);
    }
    
}
