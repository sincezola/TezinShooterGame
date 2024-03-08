using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoTextoRound : MonoBehaviour
{   
    private float timer;
    private float moveSpeed = 1000;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        MoveRoundText();
    }

    private void MoveRoundText()
    {
        rb.velocity = Vector3.up * moveSpeed * Time.deltaTime;

        if(transform.position.y >= 6.25f)
        {
            rb.velocity = Vector3.zero;

            enabled = false;
        }

        Debug.Log("oi");
    }
}
