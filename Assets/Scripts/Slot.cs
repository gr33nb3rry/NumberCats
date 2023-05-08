using System;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Game game;
    public void PutNumber()
    {
        game = transform.parent.parent.parent.parent.GetComponent<Game>();
        if (Game.isAbleToPut == true && transform.GetChild(2).GetChild(0).GetComponent<Text>().text == "0")
        {
            game.PutRandomNumber(Convert.ToInt32(gameObject.name));
        }
    }
}
