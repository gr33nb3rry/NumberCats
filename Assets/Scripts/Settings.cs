using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    static public string language = "EN";
    static public bool isSoundsOn = true;
    static public bool isMusicOn = true;
    static public int controls = 0;
    static public int gamesPlayed;
    static public int gamesWon;
    static public bool isChanged = false;

    private Color32 greyColor = new Color32(128, 128, 128, 255);
    private Color32 greenColor = new Color32(0, 255, 68, 255);

    [Header("Sounds")]
    public GameObject SoundsSlider;
    public GameObject MusicSlider;
    [Header("Controls")]
    public GameObject controlsLeft;
    public GameObject controlsRight;

    public void OnEnable()
    {
        UpdateSettings();
    }
    private void UpdateSettings()
    {
        UpdateSounds();
        UpdateMusic();
        UpdateControls();
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
}
