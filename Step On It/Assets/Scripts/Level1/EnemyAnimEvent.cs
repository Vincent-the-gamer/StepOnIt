using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void OnAir()
    {
        anim.SetBool("isJump", true);
    }
    public void OnLand()
    {
        anim.SetBool("isJump", false);
    }

}
