using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLanguage : MonoBehaviour
{
    public void LoadTutorial()
    {
        StartCoroutine(LoadTutorialTemp());
    }
    IEnumerator LoadTutorialTemp()
    {
        yield return new WaitForSeconds(0.05f);
        SceneManager.LoadScene("Tutorial");
    }
}
