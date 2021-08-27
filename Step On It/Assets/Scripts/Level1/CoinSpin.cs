using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    //每秒旋转的角度
    public float degPerSec = 60.0f;

    //旋转轴（向量)
    public Vector3 rotAxis = Vector3.up;

    // Use this for initialization
    private void Start()
    {
        rotAxis.Normalize();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(rotAxis, degPerSec * Time.deltaTime * 2);
    }

}
