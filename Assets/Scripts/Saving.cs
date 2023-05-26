using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class Saving : MonoBehaviour
{
    public GameObject noWifiImage;
    public GameObject retryButton;
    public Customization customization;
    public Settings settingsPanel;
    public bool isStart;
    private string cat = "";
    private string coinsScoreHighscore = "";
    private string settings = "";
    private string stat = "";
    private string challenges = "";
    private string hatBought = "";
    private string faceBought = "";
    private string bodyBought = "";
    void Awake()
    {
        if (isStart)
        {
            try
            {
                Initialize();

            }
            catch (Exception e)
            {
                Debug.LogException(e);
                InitializeError();
            }
        }
        
    }
    private void InitializeError()
    {
        noWifiImage.SetActive(true);
        retryButton.SetActive(true);
    }
    public void Retry()
    {
        try
        {
            Initialize();

        }
        catch (Exception e)
        {
            Debug.LogException(e);
            InitializeError();
        }
    }
    async void Initialize()
    {
        await UnityServices.InitializeAsync();
        SignInAnonymouslyAsync();
    }
    async void SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");
            Load();

            // Shows how to get the playerID
            Settings.playerID = AuthenticationService.Instance.PlayerId;
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(0.1f);
        settingsPanel.LanguageChangeOnStart();
        yield return new WaitForSeconds(0.1f);
        if (Settings.gamesPlayedEasy > 0)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SceneManager.LoadScene("ChooseLanguage");
        }
    }
    public async void Save()
    {
        cat = "";
        coinsScoreHighscore = "";
        settings = "";
        stat = "";
        challenges = "";
        hatBought = "";
        faceBought = "";
        bodyBought = "";
        CatGenerate();
        CoinsScoreHSGenerate();
        SettingsGenerate();
        StatGenerate();
        ChallengesGenerate();
        HatBoughtGenerate();
        FaceBoughtGenerate();
        BodyBoughtGenerate();
        var data = new Dictionary<string, object> {
            {"Cat", cat},
            {"CoinsScoreHighscore", coinsScoreHighscore},
            {"Settings", settings},
            {"Stat", stat},
            {"Challenges", challenges},
            {"HatBought", hatBought},
            {"FaceBought", faceBought},
            {"BodyBought", bodyBought},
        };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        Debug.Log("Saved");
    }
    private void CatGenerate()
    {
        cat += Customization.hatChosen.ToString();
        cat += ' ';
        cat += Customization.faceChosen.ToString();
        cat += ' ';
        cat += Customization.bodyChosen.ToString();
    }
    private void CoinsScoreHSGenerate()
    {
        coinsScoreHighscore += Menu.coins.ToString();
        coinsScoreHighscore += ' ';
        coinsScoreHighscore += Menu.score.ToString();
        coinsScoreHighscore += ' ';
        coinsScoreHighscore += Menu.highScore.ToString();
    }
    private void SettingsGenerate()
    {
        settings += Settings.language.ToString();
        settings += ' ';
        if (Settings.isSoundsOn)
            settings += 1;
        else settings += 0;
        settings += ' ';
        if (Settings.isMusicOn)
            settings += 1;
        else settings += 0;
        settings += ' ';
        settings += Settings.controls.ToString();
        settings += ' ';
        if (Settings.isDarkThemeOn)
            settings += 1;
        else settings += 0;
    }
    private void StatGenerate()
    {
        stat += Settings.gamesPlayedEasy.ToString();
        stat += ' ';
        stat += Settings.gamesWonEasy.ToString();
        stat += ' ';
        stat += Settings.gamesPlayedMedium.ToString();
        stat += ' ';
        stat += Settings.gamesWonMedium.ToString();
        stat += ' ';
        stat += Settings.gamesPlayedHard.ToString();
        stat += ' ';
        stat += Settings.gamesWonHard.ToString();
    }
    private void ChallengesGenerate()
    {
        challenges += Challenges.coinFlipPlayed.ToString();
        challenges += ' ';
        challenges += 0.ToString();
        challenges += ' ';
        challenges += 0.ToString();
        challenges += ' ';
        challenges += 0.ToString();
    }
    private void HatBoughtGenerate()
    {
        for (int i = 0; i < Customization.hatBought.Count; i++)
        {
            hatBought += Customization.hatBought[i].ToString();
            if (i < Customization.hatBought.Count - 1)
            {
                hatBought += ' ';
            }
        }
    }
    private void FaceBoughtGenerate()
    {
        for (int i = 0; i < Customization.faceBought.Count; i++)
        {
            faceBought += Customization.faceBought[i].ToString();
            if (i < Customization.faceBought.Count - 1)
            {
                faceBought += ' ';
            }
        }
    }
    private void BodyBoughtGenerate()
    {
        for (int i = 0; i < Customization.bodyBought.Count; i++)
        {
            bodyBought += Customization.bodyBought[i].ToString();
            if (i < Customization.bodyBought.Count - 1)
            {
                bodyBought += ' ';
            }
        }
    }
    public async void Load()
    {
        try
        {
            Dictionary<string, string> savedData = await CloudSaveService.Instance.Data.LoadAllAsync();
            string catTemp = savedData["Cat"];
            string coinsScoreHSTemp = savedData["CoinsScoreHighscore"];
            string settingsTemp = savedData["Settings"];
            string statTemp = savedData["Stat"];
            if (savedData.ContainsKey("Challenges") == true)
            {
                string challengesTemp = savedData["Challenges"];
                ChallengesLoad(challengesTemp);
            }
            string hatBoughtTemp = savedData["HatBought"];
            string faceBoughtTemp = savedData["FaceBought"];
            string bodyBoughtTemp = savedData["BodyBought"];
            CatLoad(catTemp);
            CoinsScoreHighscoreLoad(coinsScoreHSTemp);
            SettingsLoad(settingsTemp);
            StatLoad(statTemp);
            HatBoughtLoad(hatBoughtTemp);
            FaceBoughtLoad(faceBoughtTemp);
            BodyBoughtLoad(bodyBoughtTemp);
            Debug.Log("Loaded");
            StartCoroutine(LoadMenu());
        }
        catch (Exception e)
        {
            Debug.Log("Loading error " + e);
            StartCoroutine(LoadMenu());
        }
    }
    private void CatLoad(string data)
    {
        string[] dataTemp = data.Split(' ');
        Customization.hatChosen = Convert.ToInt32(dataTemp[0]);
        Customization.faceChosen = Convert.ToInt32(dataTemp[1]);
        Customization.bodyChosen = Convert.ToInt32(dataTemp[2]);
    }
    private void CoinsScoreHighscoreLoad(string data)
    {
        string[] dataTemp = data.Split(' ');
        Menu.coins = Convert.ToInt32(dataTemp[0]);
        Menu.score = Convert.ToInt32(dataTemp[1]);
        Menu.highScore = Convert.ToInt32(dataTemp[2]);
    }
    private void SettingsLoad(string data)
    {
        string[] dataTemp = data.Split(' ');
        Settings.language = Convert.ToInt32(dataTemp[0]);
        if (Convert.ToInt32(dataTemp[1]) == 1)
            Settings.isSoundsOn = true;
        else Settings.isSoundsOn = false;
        if (Convert.ToInt32(dataTemp[2]) == 1)
            Settings.isMusicOn = true;
        else Settings.isMusicOn = false;
        Settings.controls = Convert.ToInt32(dataTemp[3]);
        if (Convert.ToInt32(dataTemp[4]) == 1)
            Settings.isDarkThemeOn = true;
        else Settings.isDarkThemeOn = false;
    }
    private void StatLoad(string data)
    {
        string[] dataTemp = data.Split(' ');
        Settings.gamesPlayedEasy = Convert.ToInt32(dataTemp[0]);
        Settings.gamesWonEasy = Convert.ToInt32(dataTemp[1]);
        Settings.gamesPlayedMedium = Convert.ToInt32(dataTemp[2]);
        Settings.gamesWonMedium = Convert.ToInt32(dataTemp[3]);
        Settings.gamesPlayedHard = Convert.ToInt32(dataTemp[4]);
        Settings.gamesWonHard = Convert.ToInt32(dataTemp[5]);
    }
    private void ChallengesLoad(string data)
    {
        string[] dataTemp = data.Split(' ');
        Challenges.coinFlipPlayed = Convert.ToInt32(dataTemp[0]);
    }
    private void HatBoughtLoad(string data)
    {
        string[] dataTemp = data.Split(' ');
        for (int i = 0; i < dataTemp.Length; i++)
        {
            Customization.hatItems[Convert.ToInt32(dataTemp[i])].isBougth = true;
        }
    }
    private void FaceBoughtLoad(string data)
    {
        string[] dataTemp = data.Split(' ');
        for (int i = 0; i < dataTemp.Length; i++)
        {
            Customization.faceItems[Convert.ToInt32(dataTemp[i])].isBougth = true;
        }
    }
    private void BodyBoughtLoad(string data)
    {
        string[] dataTemp = data.Split(' ');
        for (int i = 0; i < dataTemp.Length; i++)
        {
            Customization.bodyItems[Convert.ToInt32(dataTemp[i])].isBougth = true;
        }
    }
}
