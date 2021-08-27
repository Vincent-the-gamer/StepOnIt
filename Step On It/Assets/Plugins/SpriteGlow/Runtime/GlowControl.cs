using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriteGlow;
public class GlowControl : MonoBehaviour
{
    SpriteGlowEffect sge;
    // Start is called before the first frame update
    void Start()
    {
        sge = GetComponent<SpriteGlowEffect>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            sge.enabled = true;
        }
        else sge.enabled = false;
    }
}
