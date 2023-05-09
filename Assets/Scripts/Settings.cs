using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool isStart;
    static public int language = 0;
    static public bool isSoundsOn = true;
    static public bool isMusicOn = true;
    static public int controls = 0;
    static public int gamesPlayed;
    static public int gamesWon;
    static public bool isChanged = false;

    private Color32 greyColor = new Color32(128, 128, 128, 255);
    private Color32 greenColor = new Color32(0, 255, 68, 255);
    private bool languageActive = false;

    [Header("Sounds")]
    public GameObject SoundsSlider;
    public GameObject MusicSlider;
    [Header("Controls")]
    public GameObject controlsLeft;
    public GameObject controlsRight;
    [Header("language")]
    public GameObject englishSelect;
    public GameObject frenchSelect;
    public GameObject latvianSelect;
    public GameObject russianSelect;

    public void OnEnable()
    {
        if (isStart == false)
            UpdateSettings();
    }
    private void UpdateSettings()
    {
        UpdateSounds();
        UpdateMusic();
        UpdateControls();
        UpdateLanguage();
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


    public void SoundsOnOff()
    {
        isSoundsOn = !isSoundsOn;
        isChanged = true;
        UpdateSounds();
    }
    public void MusicOnOff()
    {
        isMusicOn = !isMusicOn;
        isChanged = true;
        UpdateMusic();
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

    public void CloseSettings()
    {
        gameObject.SetActive(false);
    }
    
}
