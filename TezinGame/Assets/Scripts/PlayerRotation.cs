using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerRotation : MonoBehaviour
{
    private Rigidbody2D rb;
    private UnityEngine.Vector2 mousePosition;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {   
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        UnityEngine.Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;       
    }
}
