using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //Bind this on the click event
    public void ClickStart()
    {
      SceneManager.LoadScene("Level1");
    }
}
