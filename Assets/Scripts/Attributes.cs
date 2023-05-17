using UnityEngine;

public class Attributes : MonoBehaviour
{
   public void URLButton(string url)
    {
        Debug.Log("IS");
        Application.OpenURL(url);
        Debug.Log("YEAH");
    }
}
