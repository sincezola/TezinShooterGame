using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTXTAnim : MonoBehaviour
{   
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void AnimateRoundTXT()
    {
        anim.SetBool("K", true);

        Debug.Log("OI");
    }
}