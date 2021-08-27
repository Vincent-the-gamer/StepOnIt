using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollide : MonoBehaviour
{
    float distance = 6f;
    GameObject[] objectCoin;
    Text coincount;
    int count;
    bool isCollected;
    ParticleSystem par;
    GameObject[] pickup_effects; //捡起金币的特效

    private void Start()
    {
        objectCoin = GameObject.FindGameObjectsWithTag("Coin");
        coincount = GameObject.Find("GUIText_CoinCount").GetComponent<Text>();
        coincount.text = "0";
        count = int.Parse(coincount.text);
        pickup_effects = GameObject.FindGameObjectsWithTag("PickUp");
        count = 0;

    }

    private void Update()
    {
        for (int i = 0; i < objectCoin.Length; i++)
        {
            if (objectCoin[i] != null && pickup_effects[i] != null)
            {
                par = pickup_effects[i].GetComponent<ParticleSystem>();
                par.transform.position = objectCoin[i].transform.position;
                float dis = Vector3.Distance(objectCoin[i].transform.position, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z));
                if (dis <= distance)
                {
                    ++count;
                    coincount.text = count + "";
                    isCollected = true;
                    par.Play();
                    Destroy(objectCoin[i]);
                    StartCoroutine(DelayDestroyParticle(pickup_effects, i));
                }
            }
            else continue;
        }
    }



    public bool getIsCollected()
    {
        return isCollected;
    }

    public void SetIsCollected(bool isCollected)
    {
        this.isCollected = isCollected;
    }

    IEnumerator DelayDestroyParticle(GameObject[] o,int iter)
    {
        yield return new WaitForSeconds(3);
        Destroy(o[iter]);
    }


}
