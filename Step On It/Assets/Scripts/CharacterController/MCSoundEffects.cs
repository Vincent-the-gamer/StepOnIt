using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MCSoundEffects : MonoBehaviour
{
    //音源AudioSource相当于播放器，而音效AudioClip相当于磁带
    public AudioSource audio1, audio2, bgm,complete_audio;
    private AudioClip run, jump, getCoin, bgm_clip,complete_bgm;//各种音效
    private Player player;
    private CoinCollide coincoll;
    public float bgm_volume;
    bool complete;

    private void Awake()
    {
        audio1 = gameObject.GetComponent<AudioSource>();
        //设置不一开始就播放音效
        audio1.playOnAwake = false;
        audio2 = gameObject.AddComponent<AudioSource>();
        audio2.playOnAwake = false;
        audio2.pitch = 1;
        complete = false;
        complete_audio = gameObject.AddComponent<AudioSource>();

        //加载音效文件，我把跳跃的音频文件命名为jump
        run = Resources.Load<AudioClip>("Music/run");
        jump = Resources.Load<AudioClip>("Music/jump");
        getCoin = Resources.Load<AudioClip>("Music/Bonus");
        complete_bgm = Resources.Load<AudioClip>("Music/Complete");
        player = GetComponent<Player>();
        coincoll = GetComponent<CoinCollide>();

        //BGM
        bgm = gameObject.AddComponent<AudioSource>();

        //设置不一开始就播放音效
        bgm.playOnAwake = false;
        //bgm.pitch = (float)0.92;
        bgm.pitch = 1;
        bgm_clip = Resources.Load<AudioClip>("Music/rise-and-shine");
        bgm.loop = true;

        complete_audio.playOnAwake = false;
        complete_audio.loop = false;
        complete_audio.clip = complete_bgm;
    }

    // Update is called once per frame
    void Update()
    {
        //加速跑的音效加速
        if (Input.GetKey(KeyCode.H) && !Input.GetKeyDown(KeyCode.G))
        {
            audio1.pitch = (float)1.5;
        }
        else audio1.pitch = 1;

        //跳跃
        if ((Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.T)) && !player.getOnAir() && !coincoll.getIsCollected())
        {
            audio1.clip = jump;
            audio1.loop = false;
            audio1.Play();
            if (player.getOnAir())
            {
                audio1.Stop();
            }
        }
        else if (Input.GetAxis("Horizontal") != 0 && !audio1.isPlaying && !player.getOnAir() && !coincoll.getIsCollected())
        {
            audio1.clip = run;
            audio1.loop = true;
            audio1.Play();
        }
        else if (Input.GetAxis("Horizontal") == 0 && audio1.isPlaying && !player.getOnAir() && !coincoll.getIsCollected())
        {
            audio1.Stop();
        }

        if (coincoll.getIsCollected())
        {
            audio2.clip = getCoin;
            audio2.loop = false;
            audio2.Play();
            coincoll.SetIsCollected(false);
        }

        bgm.clip = bgm_clip;
        if (!complete)
        {
            if (bgm.isPlaying)
            {
                return;
            }
            else
            {
                bgm.Play();
            }
            bgm.volume = bgm_volume;
        }
        else bgm.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {  
        if (other == GameObject.Find("Level/Ground/Finish").GetComponent<BoxCollider2D>())
        {
            complete = true;
            complete_audio.Play();
        }
    }
}
