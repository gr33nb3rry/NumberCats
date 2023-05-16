using System;
using UnityEngine;
using UnityEngine.UI;

public class SlotTutorial : MonoBehaviour
{
    private Tutorial tutorial;
    public void PutNumber()
    {
        tutorial = transform.parent.parent.parent.parent.GetComponent<Tutorial>();
        if (Tutorial.isAbleToPut == true && transform.GetChild(2).GetChild(0).GetComponent<Text>().text == "0")
        {
            tutorial.PutRandomNumber(Convert.ToInt32(gameObject.name));
        }
    }
}
