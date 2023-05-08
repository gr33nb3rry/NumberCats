using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    public ShopItem[] hatItems = new ShopItem[]
    {
        new ShopItem(cost: 0){isBougth = true},
        new ShopItem(cost: 100),
        new ShopItem(cost: 100),
        new ShopItem(cost: 100),
        new ShopItem(cost: 100),
        new ShopItem(cost: 100),
        new ShopItem(cost: 100),
        new ShopItem(cost: 100),
        new ShopItem(cost: 100),
        new ShopItem(cost: 100),
        new ShopItem(cost: 100),
        new ShopItem(cost: 100),
    };
    public ShopItem[] faceItems = new ShopItem[]
    {
        new ShopItem(cost: 0){isBougth = true},
        new ShopItem(cost: 50),
        new ShopItem(cost: 50),
        new ShopItem(cost: 50),
        new ShopItem(cost: 50),
        new ShopItem(cost: 50),
        new ShopItem(cost: 50),
        new ShopItem(cost: 50),
    };
    public ShopItem[] bodyItems = new ShopItem[]
    {
        new ShopItem(cost: 0){isBougth = true},
        new ShopItem(cost: 50),
        new ShopItem(cost: 50),
        new ShopItem(cost: 50),
    };
    static public int hatChosen;
    static public int faceChosen;
    static public int bodyChosen;
    public List<int> hatBought = new List<int>();
    public List<int> faceBought = new List<int>();
    public List<int> bodyBought = new List<int>();
    [Header("Sprites")]
    public Sprite[] hats;
    public Sprite[] bodies;
    public Sprite[] tails;
    public Sprite[] faces;
    [Header("Cat")]
    public GameObject hat;
    public GameObject face;
    public GameObject body;
    public GameObject tail;
    [Header("Panels")]
    public GameObject[] arrowsLeft;
    public GameObject[] arrowsRight;
    [Header("Text")]
    public Text hatsCount;
    public Text facesCount;
    public Text bodiesCount;
    static public Vector3 bigHatScale = new Vector3(1, 1, 1);
    static public Vector2 bigHatPos = new Vector2(136, -312);
    static public Vector3 smallHatScale = new Vector3(0.6f, 0.6f, 0.6f);
    static public Vector2 smallHatPos = new Vector2(136, -252);

    void OnEnable()
    {
        CalculateBoughtCount();
        UpdateHat();
        UpdateFace();
        UpdateBody();
        UpdateStats();
    }
    private void CalculateBoughtCount()
    {
        hatBought.Clear();
        faceBought.Clear();
        bodyBought.Clear();
        for (int i = 0; i < hatItems.Length; i++)
        {
            if (hatItems[i].isBougth == true)
            {
                hatBought.Add(i);
            }
        }
        for (int i = 0; i < faceItems.Length; i++)
        {
            if (faceItems[i].isBougth == true)
            {
                faceBought.Add(i);
            }
        }
        for (int i = 0; i < bodyItems.Length; i++)
        {
            if (bodyItems[i].isBougth == true)
            {
                bodyBought.Add(i);
            }
        }
    }
    private void UpdateStats()
    {
        hatsCount.text = hatBought.Count.ToString();
        facesCount.text = faceBought.Count.ToString();
        bodiesCount.text = bodyBought.Count.ToString();
    }
    public void NextHat()
    {
        if (hatChosen < hatBought[hatBought.Count - 1])
        {
            for (int i = 0; i < hatBought.Count; i++)
            {
                if (hatBought[i] == hatChosen)
                {
                    hatChosen = hatBought[i+1];
                    break;
                }
            }
        }
        UpdateHat();
    }
    public void PreviousHat()
    {
        if (hatChosen > 0)
        {
            for (int i = 0; i < hatBought.Count; i++)
            {
                if (hatBought[i] == hatChosen)
                {
                    hatChosen = hatBought[i - 1];
                    break;
                }
            }
            UpdateHat();
        }
    }
    public void UpdateHat()
    {
        hat.GetComponent<Image>().sprite = hats[hatChosen];
        hat.GetComponent<RectTransform>().localScale *= 0.5f;
        if (hats[hatChosen].name[0] == 'B')
        {
            hat.GetComponent<RectTransform>().localScale = bigHatScale;
            hat.GetComponent<RectTransform>().anchoredPosition = bigHatPos;
        }
        else
        {
            hat.GetComponent<RectTransform>().localScale = smallHatScale;
            hat.GetComponent<RectTransform>().anchoredPosition = smallHatPos;
        }
        if (hatChosen < hatBought[hatBought.Count - 1])
            arrowsRight[0].SetActive(true);
        else
            arrowsRight[0].SetActive(false);
        if (hatChosen  > 0)
            arrowsLeft[0].SetActive(true);
        else
            arrowsLeft[0].SetActive(false);
    }

    public void NextBody()
    {
        if (bodyChosen < bodyBought[bodyBought.Count - 1])
        {
            for (int i = 0; i < bodyBought.Count; i++)
            {
                if (bodyBought[i] == bodyChosen)
                {
                    bodyChosen = bodyBought[i + 1];
                    break;
                }
            }
            UpdateBody();
        }
    }
    public void PreviousBody()
    {
        if (bodyChosen > 0)
        {
            for (int i = 0; i < bodyBought.Count; i++)
            {
                if (bodyBought[i] == bodyChosen)
                {
                    bodyChosen = bodyBought[i - 1];
                    break;
                }
            }
            UpdateBody();
        }
    }
    public void UpdateBody()
    {
        body.GetComponent<Image>().sprite = bodies[bodyChosen];
        tail.GetComponent<Image>().sprite = tails[bodyChosen];
        if (bodyChosen < bodyBought[bodyBought.Count - 1])
            arrowsRight[2].SetActive(true);
        else
            arrowsRight[2].SetActive(false);
        if (bodyChosen > 0)
            arrowsLeft[2].SetActive(true);
        else
            arrowsLeft[2].SetActive(false);
    }
    public void NextFace()
    {
        if (faceChosen < faceBought[faceBought.Count - 1])
        {
            for (int i = 0; i < faceBought.Count; i++)
            {
                if (faceBought[i] == faceChosen)
                {
                    faceChosen = faceBought[i + 1];
                    break;
                }
            }
            UpdateFace();
        }
    }
    public void PreviousFace()
    {
        if (faceChosen > 0)
        {
            for (int i = 0; i < faceBought.Count; i++)
            {
                if (faceBought[i] == faceChosen)
                {
                    faceChosen = faceBought[i - 1];
                    break;
                }
            }
            UpdateFace();
        }
    }
    public void UpdateFace()
    {
        face.GetComponent<Image>().sprite = faces[faceChosen];
        if (faceChosen < faceBought[faceBought.Count - 1])
            arrowsRight[1].SetActive(true);
        else
            arrowsRight[1].SetActive(false);
        if (faceChosen > 0)
            arrowsLeft[1].SetActive(true);
        else
            arrowsLeft[1].SetActive(false);
    }
}
