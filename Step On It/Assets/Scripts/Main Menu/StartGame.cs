using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //点击Start开始游戏
    public void ClickStart()
    {
      //异步加载第一关
      SceneManager.LoadScene("Level1");
    }
}
