using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    EnemyCount enemyCount;
    MyCharacterController2D cc;
    AudioSource bgm, footstep;
    private TrailRenderer trail; //拖尾效果
    private Animator anim, count, gui_coin;
    float movespeed = 20f;
    GameObject finalcoin,finalenemy;
    float move;
    bool jump;
    bool stop;
    private bool alive = true;
    private bool complete;
    private bool facingRight = true;
    bool isHiding, onAir, isAttacking;
    Text coincount;
    int num;
    // Use this for initialization
    void Start()
    {
        enemyCount = GameObject.Find("Enemies").GetComponent<EnemyCount>();
        finalcoin = GameObject.Find("GUI/Canvas/Complete/FinalCoin");
        finalenemy = GameObject.Find("GUI/Canvas/Complete/FinalEnemy");
        finalcoin.SetActive(false);
        finalenemy.SetActive(false);
        stop = false;
        complete = false;
        cc = GetComponent<MyCharacterController2D>();
        anim = GetComponent<Animator>();
        count = GameObject.Find("GUI/Canvas/GUIText_CoinCount").GetComponent<Animator>();
        gui_coin = GameObject.Find("GUI/Canvas/GUICoin").GetComponent<Animator>();
        coincount = GameObject.Find("GUI/Canvas/Complete/FinalCoinCount").GetComponent<Text>();
        trail = GetComponent<TrailRenderer>();
        trail.emitting = false;
        footstep = GetComponent<MCSoundEffects>().audio1;
        bgm = GetComponent<MCSoundEffects>().bgm;
        isHiding = false;
        isAttacking = false;
        count.SetBool("isShown", false);
        count.SetBool("isHiden", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
        {
            return;
        }
        else if (alive)
        {
            if (!complete)
            {
                cc.Move(move, jump);
                Move();
                Attack();
                DieCheck();
                UIControl();
                num = int.Parse(GameObject.Find("GUI/Canvas/GUIText_CoinCount").GetComponent<Text>().text);
            }
            else
            {
                StartCoroutine(DelayStop(5));
            }
        }

        else if (!alive)
        {
            bgm.Stop();
            footstep.Stop();
            StartCoroutine(DelayRespawn(3));
        }
    }

    void Move()
    {
        anim.SetBool("isRun", false);
        anim.SetBool("isDash", false);
        move = Input.GetAxis("Horizontal");
        jump = Input.GetKeyDown(KeyCode.G);


        //判断脸面对哪一边
        if (move > 0)
        {
            facingRight = true;
        }
        else if (move < 0)
        {
            facingRight = false;
        }

        //jump = Input.GetButton("Jump");

        if (Input.GetKey(KeyCode.H))
        {
            if (move != 0)
            {
                anim.SetBool("isRun", false);
                anim.SetBool("isDash", true);
                if (facingRight)
                {
                    move = (move * movespeed) + 10f;
                }
                else
                {
                    move = (move * movespeed) - 10f;
                }
                trail.emitting = true;  //是否正在发射拖尾光
            }
        }
        else if (move != 0)
        {
            anim.SetBool("isRun", true);
            anim.SetBool("isDash", false);
            move *= movespeed;
            trail.emitting = false;
        }
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(DelayAttack(1));
            anim.SetTrigger("attack");
        }
        else
        {
            isAttacking = false;
        }
    }

    private void DieCheck()
    {
        if (transform.position.y <= (float)-14.5)
        {
            anim.SetTrigger("die");
            alive = false;
        }
        else alive = true;
    }

    private void UIControl()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetKey(KeyCode.G) || Input.GetKey(KeyCode.H))
        {
            count.SetBool("isMoving", true);
            gui_coin.SetBool("isMoving", true);
            StartCoroutine(UIShow());

        }
        else if (!Input.anyKey)
        {
            count.SetBool("isMoving", false);
            gui_coin.SetBool("isMoving", false);
            StartCoroutine(UIHide(3));
        }

    }
    //绑定controller事件
    public void OnAir()
    {
        onAir = true;
        anim.SetBool("isJump", true);
    }

    public void OnLand()
    {
        onAir = false;
        anim.SetBool("isJump", false);
    }


    IEnumerator UIHide(float seconds)
    {
        if (count.GetBool("isShown"))
        {
            count.SetBool("isShown", false);
            gui_coin.SetBool("isShown", false);
            yield return new WaitForSeconds(seconds);
            count.SetTrigger("counthide");
            count.SetBool("isHiden", true);
            gui_coin.SetTrigger("coinhide");
            gui_coin.SetBool("isHiden", true);
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator UIShow()
    {
        if (count.GetBool("isHiden"))
        {
            count.SetBool("isHiden", false);
            count.SetTrigger("countshow");
            gui_coin.SetBool("isHiden", false);
            gui_coin.SetTrigger("coinshow");
            yield return new WaitForSeconds((float)0.25);
            count.SetBool("isShown", true);
            gui_coin.SetBool("isShown", true);
        }
    }


    IEnumerator DelayAttack(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isAttacking = true;
    }
    //复活
    IEnumerator DelayRespawn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    IEnumerator DelayStop(float seconds)
    {
        cc.Move(movespeed, false);
        yield return new WaitForSeconds(seconds);
        cc.Move(0, false);
        anim.SetTrigger("idle");
        anim.SetBool("isRun", false);
        stop = true;
    }


    public bool getJump()
    {
        return jump;
    }


    public bool getOnAir()
    {
        return onAir;
    }

    public void setAlive(bool alive)
    {
        this.alive = alive;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == GameObject.Find("Level/Ground/Finish").GetComponent<BoxCollider2D>())
        {
            complete = true;
            finalcoin.SetActive(true);
            finalenemy.SetActive(true);
            Text coinCount = GameObject.Find("GUI/Canvas/Complete/FinalCoinCount").GetComponent<Text>();
            Text enemyCount = GameObject.Find("GUI/Canvas/Complete/FinalEnemyCount").GetComponent<Text>();
            coinCount.text = num + "";
            int enemy_count = this.enemyCount.getCount();
            enemyCount.text = enemy_count + "";
            CanvasGroup canvasGroup = GameObject.Find("GUI/Canvas/Complete").GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
        }
    }
}
