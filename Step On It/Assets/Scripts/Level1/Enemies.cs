using System.Collections;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    EnemyCount enemyCount;
    EnemyController ec;
    private Animator anim;
    Player player;
    bool jump;
    float movespeed = 8f;
    float move;
    Vector3 initial;
    System.Random random;
    int pos_neg;
    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        pos_neg = random.Next(-2, 2);
        if (pos_neg == 0)       
        {
            move = -movespeed;
        }
        else
        {
            move = pos_neg * movespeed;
        }

        enemyCount = GetComponentInParent<EnemyCount>();
        ec = GetComponent<EnemyController>();
        initial = transform.position;
        anim = GetComponent<Animator>();
        player = GameObject.Find("MC").GetComponent<Player>();
        anim.SetBool("isWalk", true);
        

    }
    // Update is called once per frame
    void Update()
    {   
        ec.Move(move, jump);
        AutoMove();
        //Debug.Log(enemyCount.getCount());
    }
         

    private void AutoMove()
    {
        if (transform.position.x - initial.x <= -10)
        {
            move = movespeed;
            anim.SetBool("isWalk", true);
        }
        else if(transform.position.x - initial.x >= 10) {
            move = -movespeed;
            anim.SetBool("isWalk", true);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other == GetComponent<BoxCollider2D>()) //敌人互相碰撞时，反向移动
        {
            move = -movespeed;
        }
        else if (player.getOnAir())
        {
           if(other == GameObject.Find("MC").GetComponent<CapsuleCollider2D>())
            {
                move = 0;
                anim.SetTrigger("die");
                Destroy(GetComponent<BoxCollider2D>());
                Destroy(GetComponent<CapsuleCollider2D>());
                enemyCount.setCount(enemyCount.getCount() + 1);
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GameObject.Find("MC").GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2000));
                GameObject.Find("MC").GetComponent<Animator>().SetBool("isJump", true);
                StartCoroutine(DelayDestroy());  
            }
        }
        else {
            if (other == GameObject.Find("MC").GetComponent<CapsuleCollider2D>())
            {
                Destroy(GetComponent<CapsuleCollider2D>());
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GameObject.Find("MC").GetComponent<Animator>().SetTrigger("die");
                player.setAlive(false);
            }
        }
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds((float)0.45);
        Destroy(gameObject);
    }


}
