using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    static public char difficulty;
    private int score;
    private int coins;
    private int roundCount;
    private int roundNumber;
    private int randomNumber;
    private bool isWon;
    private Color32[] colors = new Color32[]
    {
        new Color32(255,76,51, 255), new Color32(238,186,48, 255),
        new Color32(190,155,123, 255), new Color32(216,150,255, 255),
        new Color32(113,199,236, 255), new Color32(255,139,148, 255),
        new Color32(119,171,89, 255), new Color32(201,223,138, 255),
        new Color32(134,206,203, 255), new Color32(190,200,209, 255),
        new Color32(227,93,106, 255), new Color32(210,165,109, 255),
        new Color32(131,208,201, 255), new Color32(255,203,133, 255),
        new Color32(255,203,133, 255), new Color32(255,102,102, 255),
        new Color32(114,137,218, 255), new Color32(100,151,177, 255),
    };
    static public bool isAbleToPut;
    private GameObject slots;
    private int[,] numbers;
    private int idLastPut;
    private int catSoundCount = 0;
    private bool isExtraLifeGot = false;
    int breakpoint = 100;
    private int randomLeft = 1;
    private int randomRight = 1000;
    [Header("Panels")]
    public Customization customization;
    public LevelPlayAds ads;
    public Saving saving;
    public Settings settings;
    public EventTrigger settingsButton;
    public GameObject settingsPanel;
    public GameObject playPanel;
    public GameObject slotsEasy;
    public GameObject canvas;
    public GameObject round;
    public GameObject endPanel;
    public GameObject extraLifePanel;
    public GameObject loadPanel;
    public GameObject tutorialPanel;
    [Header("Cat")]
    public GameObject cat;
    public GameObject hat;
    public GameObject face;
    public GameObject body;
    public GameObject tail;
    [Header("Texts")]
    public LocalizeStringEvent roundStart;
    public LocalizeStringEvent roundText;
    public Text randomNumberText;
    public LocalizeStringEvent chanceText;
    public LocalizeStringEvent scoreText;
    public Text coinsText;
    [Header("Audio")]
    public AudioClip[] catSounds;
    public AudioClip[] winLoseSounds;
    public AudioClip coinsSound;
    public AudioSource catAudio;
    public AudioSource coinsAudio;
    public AudioSource roundAudio;
    public AudioSource randomNumberAudio;
    public AudioClip slotOpeningClip;
    public AudioClip[] randomnumberClip;
    public AudioClip[] music;

    private int[] randomNumbers = new int[5] {505, 2, 140, 512, 906};
    private int[] putSlots = new int[5] {2, 0, 1, 3, 4};
    private int tutorialStep = 0;
    public LocalizeStringEvent tutorialText;

    public void Start()
    {
        settings.CheckForDarkTheme();
        settings.CheckForSounds();
        settings.CheckForMusic();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        slots = slotsEasy;
        roundCount = 5;
        settings.audioMusicSource.clip = music[0];

        settings.audioMusicSource.Play();
        slots.SetActive(true);
        roundNumber = 1;
        isWon = false;
        settingsButton.enabled = false;
        UpdateCat();
        UpdateRoundText();
        UpdateChanceText(100);
        StartCoroutine(Starting());
    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        settingsPanel.GetComponent<Animation>().Play("settingsOpen");
    }
    private void UpdateCat()
    {
        body.GetComponent<Image>().sprite = customization.bodies[0];
        tail.GetComponent<Image>().sprite = customization.tails[0];
        face.GetComponent<Image>().sprite = customization.faces[0];
        hat.GetComponent<Image>().sprite = customization.hats[0];
        hat.GetComponent<RectTransform>().localScale = Customization.smallHatScale;
    }
    private void UpdateRoundText()
    {
        roundText.StringReference.Add("x",
                new IntVariable { Value = roundNumber });
        roundText.StringReference.Add("y",
            new IntVariable { Value = roundCount });
        roundText.StringReference.RefreshString();
    }
    IEnumerator Starting()
    {
        playPanel.GetComponent<Animation>().Play("Start");
        yield return new WaitForSeconds(1);
        cat.GetComponent<Animation>().Play("catIdle");
        yield return new WaitForSeconds(0.83f);
        //round count animation
        for (int i = 0; i < roundCount; i++)
        {
            roundAudio.Play();
            roundStart.StringReference.Add("x",
                new IntVariable { Value = 1 });
            roundStart.StringReference.Add("y",
                new IntVariable { Value = i + 1 });
            roundStart.StringReference.RefreshString();
            yield return new WaitForSeconds((float)3 / roundCount);
        }
        //slots animation
        yield return new WaitForSeconds(2.09f);
        round.GetComponent<Animation>().Play("roundIdle");
        for (int i = 0; i < roundCount; i++)
        {
            randomNumberAudio.clip = slotOpeningClip;
            randomNumberAudio.Play();
            if (i == roundCount - 1)
            {
                slots.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
            slots.transform.GetChild(i).GetChild(2).GetComponent<Image>().color = 
                colors[Random.Range(0,colors.Length)];
            slots.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        //ready for first round
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(RandomNumberGeneration());
        yield return new WaitForSeconds(5.5f);

        TutorialOpen("step_0");

        AvailableSlotShaking();
        settingsButton.enabled = true;
    }
    private void TutorialOpen(string entry)
    {
        tutorialText.StringReference.TableEntryReference = entry;
        tutorialPanel.SetActive(true);
    }
    public void TutorialClose()
    {
        if (tutorialStep == 0)
        {
            tutorialStep++;
            tutorialPanel.SetActive(false);
            TutorialOpen("step_1");
        }
        else if (tutorialStep == 1)
        {
            tutorialPanel.GetComponent<Image>().raycastTarget = false;
            tutorialPanel.transform.GetChild(2).gameObject.SetActive(false);
            tutorialStep++;
            tutorialPanel.SetActive(false);
            TutorialOpen("step_2");
        }
        else
        {
            tutorialPanel.SetActive(false);
        }
    }
    IEnumerator RandomNumberGeneration()
    {
        randomNumber = randomNumbers[roundNumber - 1];
        canvas.GetComponent<Animation>().Play("RandomNumberZoom");
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 180; i++)
        {
            randomNumberText.text = ((int)((i + 1) * ((float)randomNumber / 180))).ToString();
            yield return new WaitForEndOfFrame();
        }
        randomNumberText.text = randomNumber.ToString();
        randomNumberAudio.clip = randomnumberClip[Random.Range(0,3)];
        randomNumberAudio.Play();
    }
    private void AvailableSlotShaking()
    {
        for (int i = 0; i < roundCount; i++)
        {
            if (slots.transform.GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>().text == "0")
            {
                slots.transform.GetChild(i).GetComponent<Animation>().Play("slotShake");
            }
        }
        isAbleToPut = true;
    }
    private void StopSlotShaking()
    {
        for (int i = 0; i < roundCount; i++)
        {
            if (slots.transform.GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>().text == "0")
            {
                slots.transform.GetChild(i).GetComponent<Animation>().Stop("slotShake");
                slots.transform.GetChild(i).GetChild(2).GetComponent<RectTransform>().rotation = Quaternion.identity;
            }
        }
    }
    public void PutRandomNumber(int id)
    {
        if (id == putSlots[roundNumber - 1])
        {
            TutorialClose();
            randomNumberAudio.clip = slotOpeningClip;
            randomNumberAudio.Play();

            settingsButton.enabled = false;
            StopSlotShaking();
            slots.transform.GetChild(id).GetChild(2).GetChild(0).GetComponent<Text>().text =
                randomNumber.ToString();
            slots.transform.GetChild(id).GetComponent<Animation>().Play("slotClick");
            isAbleToPut = false;
            idLastPut = id;
            Invoke("CheckForOrder", 2);
        }
        else
        {
            Debug.Log("NO");
        }
    }
    private void CheckForOrder()
    {
        //list of slots ids with numbers
        List<int> slotsWithNumber = new List<int>();
        for (int i = 0; i < roundCount; i++)
        {
            if (slots.transform.GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>().text != "0")
            {
                slotsWithNumber.Add(i);
            }
        }
        //2d array with slot id and its number
        numbers = new int[slotsWithNumber.Count, 2];
        for (int i = 0; i < slotsWithNumber.Count; i++)
        {
            numbers[i, 0] = slotsWithNumber[i];
            numbers[i, 1] = System.Convert.ToInt32(slots.transform.GetChild(slotsWithNumber[i]).GetChild(2).GetChild(0).GetComponent<Text>().text);
        }
        breakpoint = 100;
        
        StartCoroutine(CheckForOrderAnimation(slotsWithNumber, breakpoint));
    }
    IEnumerator CheckForOrderAnimation(List<int> slotsWithNumber, int breakpoint)
    {
        for (int i = 0; i < roundCount; i++)
        {
            if (i == breakpoint)
            {
                slots.transform.GetChild(i).GetChild(2).GetChild(1).gameObject.SetActive(false);
                slots.transform.GetChild(i).GetChild(2).GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                slots.transform.GetChild(i).GetChild(2).GetChild(1).gameObject.SetActive(true);
                slots.transform.GetChild(i).GetChild(2).GetChild(2).gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < roundCount; i++)
        {
            slots.transform.GetChild(i).GetChild(2).GetChild(1).gameObject.SetActive(false);
            slots.transform.GetChild(i).GetChild(2).GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        NextRound();
    }
    private void CatSound()
    {
        catAudio.clip = catSounds[catSoundCount];
        catAudio.Play();
        if (catSoundCount < catSounds.Length - 1)
        {
            catSoundCount++;
        }
        else
        {
            catSoundCount = 0;
        }
    }
    private void NextRound()
    {
        if (roundNumber < roundCount)
        {
            CatSound();
            roundNumber++;
            if (roundNumber % 2 == 0)
                StartCoroutine(CatJump());
            StartCoroutine(Round());
        }
        else
        {
            Win();
        }
    }
    IEnumerator Round()
    {
        settingsButton.enabled = false;
        round.GetComponent<Animation>().Play("roundIncrease");
        UpdateRoundText();
        Chance();
        ScoreIncrease();
        CoinsIncrease(score);
        yield return new WaitForSeconds(1);
        round.GetComponent<Animation>().Play("roundIdle");
        StartCoroutine(RandomNumberGeneration());
        yield return new WaitForSeconds(5.5f);
        TutorialOpen($"step_{roundNumber + 1}");
        AvailableSlotShaking();
        settingsButton.enabled = true;
    }
    private void Chance()
    {
        float chance = 0; 
        float randomDivide = (float)randomRight / 100;
        if (numbers.GetLength(0) == 1)
        {
            if (numbers[0,0] == 0)
            {
                chance += (float)(randomRight - numbers[0, 1]) / randomDivide;
            }
            else if (numbers[0,0] == roundCount - 1)
            {
                chance += (float)(numbers[0, 1] - 1) / randomDivide;
            }
            else
            {
                chance += (float)(numbers[0, 1] - 1) / randomDivide;
                chance += (float)(randomRight - numbers[0, 1]) / randomDivide;
            }
        }
        bool isFirstTime = false;
        for (int i = 1; i < numbers.GetLength(0); i++)
        {
            if (i == numbers.GetLength(0) - 1 && numbers[i, 0] < roundCount - 1)
            {
                chance += (float)(randomRight - numbers[i, 1]) / randomDivide;
            }
            if (numbers[0,0] > 0 && isFirstTime == false)
            {
                chance += ((float)numbers[0, 1] - 1) / randomDivide;
                isFirstTime = true;
            }
            if (numbers[i, 0] - numbers[i - 1, 0] > 1)
            {
                chance += (float)(numbers[i, 1] - numbers[i - 1, 1] - 1) / randomDivide;
            }

        }
        UpdateChanceText((float)System.Math.Round(chance, 1));
    }
    private void UpdateChanceText(float value)
    {
        chanceText.StringReference.Add("x",
                new FloatVariable { Value = value});
        chanceText.StringReference.RefreshString();
    }
    private void ScoreIncrease()
    {
        score += roundNumber - 1;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        scoreText.StringReference.Add("x",
                new IntVariable { Value = score });
        scoreText.StringReference.RefreshString();
        scoreText.gameObject.GetComponent<Animation>().Play();
    }
    private void CoinsIncrease(int value)
    {
        coins += value;
        UpdateCoinsText();
        coinsAudio.clip = coinsSound;
        coinsAudio.Play();
    }
    private void UpdateCoinsText()
    {
        coinsText.text = coins.ToString("N0", new CultureInfo("en-us"));
        coinsText.gameObject.GetComponent<Animation>().Play();
    }
    private void Win()
    {
        isWon = true;
        if (difficulty == 'E')
            CoinsIncrease(100);
        else if (difficulty == 'M')
            CoinsIncrease(150);
        else if (difficulty == 'H')
            CoinsIncrease(200);
        OpenEndPanel(isWin: true);
        catAudio.clip = winLoseSounds[0];
        catAudio.Play();
    }
    private void Lose()
    {
        if (isExtraLifeGot == false)
        {
            OpenExtraLifePanel();
        }
        else
        {
            OpenEndPanel(isWin: false);
        }
        catAudio.clip = winLoseSounds[1];
        catAudio.Play();
    }
    private void OpenExtraLifePanel()
    {
        extraLifePanel.SetActive(true);
        isExtraLifeGot = true;
    }
    public void ExtraLifeGet()
    {
        ads.ShowRewardedAd();
        extraLifePanel.SetActive(false);
        StartCoroutine(ExtraLifeGetTemp());
    }
    IEnumerator ExtraLifeGetTemp()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < roundCount; i++)
        {
            slots.transform.GetChild(i).GetChild(2).GetChild(1).gameObject.SetActive(false);
            slots.transform.GetChild(i).GetChild(2).GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        int randomRangeLeft = 0;
        int randomRangeRight = 0;
        for (int i = 0; i < numbers.GetLength(0); i++)
        {
            if (numbers[i, 0] == idLastPut)
            {
                if (i == 0)
                {
                    //randomRangeLeft = numbers[i + 1, 1] / 2;
                    randomRangeLeft = 1;
                    randomRangeRight = numbers[i + 1, 1];
                    break;
                }
                if (i == numbers.GetLength(0) - 1)
                {
                    randomRangeLeft = numbers[i - 1, 1];
                    //randomRangeRight = numbers[i - 1, 1] + (1001 - numbers[i - 1, 1]) / 2;
                    randomRangeRight = 1001;
                    break;
                }
                randomRangeLeft = numbers[i - 1, 1];
                randomRangeRight = numbers[i + 1, 1];
                break;
            }
        }
        randomNumber = Random.Range(randomRangeLeft, randomRangeRight);
        yield return new WaitForSeconds(1);
        Text slotText = slots.transform.GetChild(idLastPut).GetChild(2).GetChild(0).GetComponent<Text>();
        for (int i = 0; i < 180; i++)
        {
            slotText.text = ((int)((i + 1) * ((float)randomNumber / 180))).ToString();
            yield return new WaitForEndOfFrame();
        }
        slotText.text = randomNumber.ToString();
        slotText.gameObject.GetComponent<Animation>().Play("slotText");
        randomNumberAudio.Play();

        NextRound();
    }
    public void ExtraLifeNo()
    {
        extraLifePanel.SetActive(false);
        OpenEndPanel(isWin: false);
    }
    private void OpenEndPanel(bool isWin)
    {
        if (isWin)
        {
            endPanel.transform.GetChild(0).gameObject.SetActive(true);
            endPanel.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            endPanel.transform.GetChild(0).gameObject.SetActive(false);
            endPanel.transform.GetChild(1).gameObject.SetActive(true);
        }
        endPanel.transform.GetChild(2).GetChild(0).GetChild(2).GetComponent<Text>().text =
            coins.ToString("N0", new CultureInfo("en-us"));

        endPanel.SetActive(true);
    }
    public void Collect()
    {
        Menu.coins += coins;

        Menu.score += score;

        Settings.gamesWonEasy++;
        Settings.gamesPlayedEasy++;

        saving.Save();
        Settings.isChanged = false;
        StartCoroutine(LoadMenu());
    }
    IEnumerator LoadMenu()
    {
        loadPanel.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Menu");
    }
    IEnumerator CatJump()
    {
        cat.GetComponent<Animation>().Stop("catIdle");
        cat.GetComponent<Animation>().Play("catJump");
        yield return new WaitForSeconds(1.5f);
        cat.GetComponent<Animation>().Play("catIdle");
    }
}
