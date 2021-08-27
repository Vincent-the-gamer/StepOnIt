using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void Click()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
