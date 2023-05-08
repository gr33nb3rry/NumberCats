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
    public bool isStart;
    private string cat = "";
    private string coinsScoreHighscore = "";
    private string settings = "";
    private string stat = "";
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
        CatGenerate();
        CoinsScoreHSGenerate();
        SettingsGenerate();
        StatGenerate();
        var data = new Dictionary<string, object> {
            {"Cat", cat},
            {"CoinsScoreHighscore", coinsScoreHighscore},
            {"Settings", settings},
            {"Stat", stat}};
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        Debug.Log("Saved");
    }
    private async void DeleteAll()
    {
        await CloudSaveService.Instance.Data.ForceDeleteAsync("Cat");
        await CloudSaveService.Instance.Data.ForceDeleteAsync("CoinsScoreHighscore");
        await CloudSaveService.Instance.Data.ForceDeleteAsync("Settings");
        await CloudSaveService.Instance.Data.ForceDeleteAsync("Stat");
        Debug.Log("Deleted");
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
    public async void Load()
    {
        try
        {
            Dictionary<string, string> savedData = await CloudSaveService.Instance.Data.LoadAllAsync();
            string catTemp = savedData["Cat"];
            string coinsScoreHSTemp = savedData["CoinsScoreHighscore"];
            string settingsTemp = savedData["Settings"];
            string statTemp = savedData["Stat"];
            CatLoad(catTemp);
            CoinsScoreHighscoreLoad(coinsScoreHSTemp);
            SettingsLoad(settingsTemp);
            StatLoad(statTemp);
            Debug.Log("Loaded");
            StartCoroutine(LoadMenu());
        }
        catch (Exception)
        {
            Debug.Log("Loading error");
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
}
