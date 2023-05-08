using UnityEngine;

public class ShopItem
{
    public ShopItem(int cost)
    {
        this.cost = cost;
    }
    public int cost;
    public bool isBougth;
    public char type; //H-hat F-face B-body
}
