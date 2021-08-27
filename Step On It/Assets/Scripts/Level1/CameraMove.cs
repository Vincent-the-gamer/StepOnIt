using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
    private GameObject player;  //主角
    private float speed;  //相机跟随速度
    private float smoothTime; //摄像机平滑移动的时间
    private Vector3 cameraVelocity = Vector3.zero;
    Hint hint;

    public float minPosx;  //相机不超过背景边界允许的最小值
    public float maxPosx;  //相机不超过背景边界允许的最大值


    void Start()
    {
        hint = GameObject.Find("GUI/Canvas/Hint").GetComponent<Hint>();
        player = GameObject.Find("MC");
        smoothTime = (float)0.0465; 
        speed = (float)1.19;
    }

    void LateUpdate()
    {
       FixCameraPos();
       HintFadeOut();
    }

    void FixCameraPos()
    {
        if (Input.GetKey(KeyCode.H))
        {
            smoothTime = (float)0.09;
            speed = (float)3.2;
        }
        else
        {
            smoothTime = (float)0.0465;
            speed = (float)1.19;
        }

        float pPosX = player.transform.position.x + (float)13.5;  //主角 x轴方向 时实坐标值
        float cPosX = transform.position.x;             //相机 x轴方向 时实坐标值
        if (pPosX - cPosX > 0.5)    // 并不是死死地跟随，是相机和主角之间距离超过某个值时才跟随
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(cPosX + speed, transform.position.y, transform.position.z), ref cameraVelocity, smoothTime);

        }
        if (pPosX - cPosX < -0.5)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(cPosX - speed, transform.position.y, transform.position.z), ref cameraVelocity, smoothTime);
        }
        float realPosX = Mathf.Clamp(transform.position.x, minPosx, maxPosx);  // 相机X轴方向 限制移动区间，防止超过背景边界
        transform.position = new Vector3(realPosX, transform.position.y, transform.position.z);
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(realPosX, transform.position.y, transform.position.z), ref cameraVelocity, smoothTime);
    }

    private void HintFadeOut()
    {
        if(transform.position.x > 50)
        {
            hint.Hint_FadeOut_Event();
        }
    }
   
}
