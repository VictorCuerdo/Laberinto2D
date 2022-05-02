using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 3f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 objetivo;
    private bool ismoving = false;
    public event EventHandler OnMove;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        objetivo = new Vector2(0, 0);
        OnMove?.Invoke(this, EventArgs.Empty);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        if (Vector3.Distance(transform.position, objetivo) == 0 && ismoving)
        {
            OnMove?.Invoke(this, EventArgs.Empty);
            ismoving = false;
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Mathf.Abs(rb.velocity.x) > 0 || Mathf.Abs(rb.velocity.y) > 0)
        {
            if (rb.velocity.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

        }
        else
        {

            if (horizontal != 0 && Mathf.Abs(horizontal) > Mathf.Abs(vertical) && !ismoving)
            {
                Debug.Log("Horizontal");
                if (horizontal > 0)
                {
                    if (BoardManager.Instance.grid.GetGridObject((int)(transform.position.x + 1f), (int)(transform.position.y)).isWalkable)
                    {
                        objetivo = new Vector2(transform.position.x + 1f, transform.position.y);
                        ismoving = true;

                    }
                }
                else
                {
                    if (BoardManager.Instance.grid.GetGridObject((int)(transform.position.x - 1f), (int)(transform.position.y)).isWalkable)
                    {
                        objetivo = new Vector2(transform.position.x - 1f, transform.position.y);
                        ismoving = true;

                    }
                }
            }
            if (vertical != 0 && Mathf.Abs(vertical) > Mathf.Abs(horizontal) && !ismoving)
            {
                Debug.Log("Vertical");
                if (vertical > 0)
                {
                    if (BoardManager.Instance.grid.GetGridObject((int)(transform.position.x), (int)(transform.position.y + 1f)).isWalkable)
                    {
                        objetivo = new Vector2(transform.position.x, transform.position.y + 1f);
                        ismoving = true;

                    }
                }
                else
                {
                    if (BoardManager.Instance.grid.GetGridObject((int)(transform.position.x), (int)(transform.position.y - 1f)).isWalkable)
                    {
                        objetivo = new Vector2(transform.position.x, transform.position.y - 1f);
                        ismoving = true;

                    }
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, objetivo, moveSpeed * Time.deltaTime);
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("LoseScene");
        }
        if (collision.gameObject.tag == "Goal")
        {
            if (PlayerPrefs.GetInt("level") == 4)
            {
                SceneManager.LoadScene("WinScene");
            }
            else
            {
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
                SceneManager.LoadScene("MainScene");
            }
        }
    }
}
