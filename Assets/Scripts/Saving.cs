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
    public Customization customization;
    public bool isStart;
    private string cat = "";
    private string coinsScoreHighscore = "";
    private string settings = "";
    private string stat = "";
    private string hatBought = "";
    private string faceBought = "";
    private string bodyBought = "";
    async void Awake()
    {
        if (isStart)
        {
            try
            {
                await UnityServices.InitializeAsync();
                SignInAnonymouslyAsync();

            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
    }
    async void SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");
            Load();

            // Shows how to get the playerID
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
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Save();
        }
    }
    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Menu");
    }
    public async void Save()
    {
        cat = "";
        coinsScoreHighscore = "";
        settings = "";
        stat = "";
        hatBought = "";
        faceBought = "";
        bodyBought = "";
        CatGenerate();
        CoinsScoreHSGenerate();
        SettingsGenerate();
        StatGenerate();
        HatBoughtGenerate();
        FaceBoughtGenerate();
        BodyBoughtGenerate();
        var data = new Dictionary<string, object> {
            {"Cat", cat},
            {"CoinsScoreHighscore", coinsScoreHighscore},
            {"Settings", settings},
            {"Stat", stat},
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
    }
    private void StatGenerate()
    {
        stat += Settings.gamesPlayed.ToString();
        stat += ' ';
        stat += Settings.gamesWon.ToString();
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
        Settings.language = dataTemp[0];
        if (Convert.ToInt32(dataTemp[1]) == '1')
            Settings.isSoundsOn = true;
        else Settings.isSoundsOn = false;
        if (Convert.ToInt32(dataTemp[2]) == '1')
            Settings.isMusicOn = true;
        else Settings.isMusicOn = false;
    }
    private void StatLoad(string data)
    {
        string[] dataTemp = data.Split(' ');
        Settings.gamesPlayed = Convert.ToInt32(dataTemp[0]);
        Settings.gamesWon = Convert.ToInt32(dataTemp[1]);
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
