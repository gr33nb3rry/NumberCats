using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public bool isPlayButton;
    public bool isRankButton;
    public bool isFooterButton;
    public bool isShopCategoryButton;
    public bool isShopBuyButton;
    public bool isDifficultyButton;
    public bool isCloseDifficultyButton;
    public bool isSettingsButton;
    public char difficulty;
    public Menu menu;
    public Shop shop;
    public AudioClip tapSound;
    public AudioSource audioSource;
    public void Click()
    {
        if (isPlayButton)
        {
            audioSource.clip = tapSound;
            audioSource.Play();
            GetComponent<Animation>().Stop();
            GetComponent<Animation>().Play("clickButtonPlay");
            menu.Invoke("CloseActivePanel", 0.91f);
            menu.Invoke("ChooseDifficulty", 1.41f);
        }
        else if (isRankButton)
        {
            audioSource.clip = tapSound;
            audioSource.Play();
            GetComponent<Animation>().Play("clickButtonPlay");
        }
        else if (isFooterButton)
        {
            if (Convert.ToInt32(gameObject.name) != Menu.activePanel)
            {
                GetComponent<Animation>().Stop();
                menu.footerPanels[Menu.activePanel].gameObject.GetComponent<Animation>().Play("offButtonFooter");
                GetComponent<Animation>().Play("clickButtonFooter");
                menu.CloseActivePanel();
                Menu.activePanel = Convert.ToInt32(gameObject.name);
                menu.Invoke("OpenActivePanel", 0.5f);
            }
        }
        else if (isShopCategoryButton)
        {
            if (Convert.ToInt32(gameObject.name) != Shop.activePanel)
            {
                GetComponent<Animation>().Play("clickButtonShopCat");
                shop.CloseActivePanel();
                Shop.activePanel = Convert.ToInt32(gameObject.name);
                shop.OpenActivePanel();
            }
        }
        else if (isShopBuyButton)
        {
            GetComponent<Animation>().Play("clickButtonBuy");
        }
        else if (isDifficultyButton)
        {
            GetComponent<Animation>().Play("clickButtonDifficulty");
            Game.difficulty = difficulty;
            SceneManager.LoadScene("Game");
        }
        else if (isCloseDifficultyButton)
        {
            menu.CloseDifficulty();
        }
        else if (isSettingsButton)
        {
            audioSource.clip = tapSound;
            audioSource.Play();
            GetComponent<Animation>().Play("clickButtonScale");
        }
    }
}
