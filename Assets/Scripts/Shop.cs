using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;

public class Shop : MonoBehaviour
{
    static public int activePanel;
    static public bool isChanged = false;
    public Customization customization;
    public Menu menu;
    public GameObject[] panels;
    public GameObject hatsPanel;
    public GameObject facesPanel;
    public GameObject bodiesPanel;
    private Color32 hatColor = new Color32(0, 190, 255, 255);
    private Color32 faceColor = new Color32(29, 178, 94, 255);
    private Color32 bodyColor = new Color32(255, 23, 36, 255);
    private Color32 boughtColor = new Color32(128, 128, 128, 255);

    public void Start()
    {
        activePanel = 0;
    }
    public void OnEnable()
    {
        UpdateShop();
    }
    private void UpdateShop()
    {
        UpdateHats();
        UpdateFaces();
        UpdateBodies();
    }
    private void UpdateHats()
    {
        Transform obj;
        for (int i = 1; i < customization.hats.Length; i++)
        {
            obj = hatsPanel.transform.GetChild(i - 1);
            if (Customization.hatItems[i].isBougth)
                obj.GetChild(1).GetComponent<Image>().color = boughtColor;
            else obj.GetChild(1).GetComponent<Image>().color = hatColor;

            obj.GetChild(0).GetComponent<Image>().sprite = customization.hats[i];

            if (Customization.hatItems[i].isBougth)
            {
                obj.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
                obj.GetChild(1).GetChild(0).GetChild(1).GetComponent<LocalizeStringEvent>().enabled = true;
            }
            else
            {
                obj.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
                obj.GetChild(1).GetChild(0).GetChild(1).GetComponent<LocalizeStringEvent>().enabled = false;
                obj.GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>().text =
                    Customization.hatItems[i].cost.ToString("N0", new CultureInfo("en-us"));
            }

        }
    }
    private void UpdateFaces()
    {
        Transform obj;
        for (int i = 1; i < customization.faces.Length; i++)
        {
            obj = facesPanel.transform.GetChild(i - 1);
            if (Customization.faceItems[i].isBougth)
                obj.GetChild(1).GetComponent<Image>().color = boughtColor;
            else obj.GetChild(1).GetComponent<Image>().color = faceColor;

            obj.GetChild(0).GetComponent<Image>().sprite = customization.faces[i];

            if (Customization.faceItems[i].isBougth)
            {
                obj.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
                obj.GetChild(1).GetChild(0).GetChild(1).GetComponent<LocalizeStringEvent>().enabled = true;
            }
            else
            {
                obj.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
                obj.GetChild(1).GetChild(0).GetChild(1).GetComponent<LocalizeStringEvent>().enabled = false;
                obj.GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>().text =
                    Customization.faceItems[i].cost.ToString("N0", new CultureInfo("en-us"));
            }
        }
    }
    private void UpdateBodies()
    {
        Transform obj;
        for (int i = 1; i < customization.bodies.Length; i++)
        {
            obj = bodiesPanel.transform.GetChild(i - 1);
            if (Customization.bodyItems[i].isBougth)
                obj.GetChild(1).GetComponent<Image>().color = boughtColor;
            else obj.GetChild(1).GetComponent<Image>().color = bodyColor;

            obj.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = customization.tails[i];
            obj.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = customization.bodies[i];

            if (Customization.bodyItems[i].isBougth)
            {
                obj.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
                obj.GetChild(1).GetChild(0).GetChild(1).GetComponent<LocalizeStringEvent>().enabled = true;
            }
            else
            {
                obj.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
                obj.GetChild(1).GetChild(0).GetChild(1).GetComponent<LocalizeStringEvent>().enabled = false;
                obj.GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>().text =
                    Customization.bodyItems[i].cost.ToString("N0", new CultureInfo("en-us"));
            }
        }
    }
    public void BuyHat(int id)
    {
        if (Customization.hatItems[id].isBougth == false && Menu.coins >= Customization.hatItems[id].cost)
        {
            Customization.hatItems[id].isBougth = true;
            Menu.coins -= Customization.hatItems[id].cost;
            isChanged = true;
            menu.UpdateCoins();
            UpdateHats();
        }
    }
    public void BuyFace(int id)
    {
        if (Customization.faceItems[id].isBougth == false && Menu.coins >= Customization.faceItems[id].cost)
        {
            Customization.faceItems[id].isBougth = true;
            Menu.coins -= Customization.faceItems[id].cost;
            isChanged = true;
            menu.UpdateCoins();
            UpdateFaces();
        }
    }
    public void BuyBody(int id)
    {
        if (Customization.bodyItems[id].isBougth == false && Menu.coins >= Customization.bodyItems[id].cost)
        {
            Customization.bodyItems[id].isBougth = true;
            Menu.coins -= Customization.bodyItems[id].cost;
            isChanged = true;
            menu.UpdateCoins();
            UpdateBodies();
        }
    }
    public void OpenActivePanel()
    {
        panels[activePanel].SetActive(true);
    }
    public void CloseActivePanel()
    {
        panels[activePanel].SetActive(false);
    }
}
