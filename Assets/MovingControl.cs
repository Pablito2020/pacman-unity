using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingControl : MonoBehaviour
{
    Animator an;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            an.SetBool("isMoving", true);
            rb.velocity = new Vector2(3.0f, 0.0f);
        }
        else
        {
            an.SetBool("isMoving", false);
            rb.velocity = Vector2.zero;
        }
    }
}