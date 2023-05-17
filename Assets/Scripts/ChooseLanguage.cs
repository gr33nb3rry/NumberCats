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
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Tutorial");
    }
}
