using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public float speed;
    private bool moveLeft;
    private bool dontMove;
    private bool attacking;
    private bool dontAttack;
    private bool weapon = false;

    // Start is called before the first frame update
    void Start()
    {
        dontMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMoving();
    }
    void HandleMoving()
    {
        if (dontMove)
        {
            StopMoving();
        }
        else
        {
            if(moveLeft)
            {
                MoveLeft();
            }
            else if(!moveLeft)
            {
                MoveRight();
            }
        }
        if(dontAttack)
        {
            StopAttacking();
        }
        else
        {
            if(attacking)
            {
                Attack();
            }
        }
    }
    public void AllowMovement(bool movement)
    {
        dontMove = false;
        moveLeft = movement;
    }
    public void DontAllowMovement()
    {
        dontMove = true;
    }
    public void Attacking(bool attack)
    {
        dontAttack = false;
        attacking = attack;
    }
    public void NotAttacking()
    {
        dontAttack= true;
    }
    public void MoveLeft()
    {
        Vector3 characterScale = player.localScale;
        if(speed > 0)
        {
            characterScale.x = -4;
        }
        player.localScale = characterScale;
        player.Translate(new Vector2(-speed, 0) * Time.deltaTime);
        animator.SetFloat("speed", Mathf.Abs(speed));
    }
    public void MoveRight()
    {
        Vector3 characterScale = player.localScale;
        if (speed > 0)
        {
            characterScale.x = 4;
        }
        player.localScale = characterScale;
        player.Translate(new Vector2(speed, 0) * Time.deltaTime);
        animator.SetFloat("speed", Mathf.Abs(speed));
    }
    public void StopMoving()
    {
        player.Translate(new Vector2(0, 0) * Time.deltaTime);
        animator.SetFloat("speed", 0);
    }
    public void Attack()
    {
        animator.SetBool("attack", true);
    }
    public void StopAttacking()
    {
        animator.SetBool("attack", false);
    }
    void DirectInput()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if(x>0)
        {
            MoveRight();
        }
        else if(x<0)
        {
            MoveLeft();
        }
        else
        {
            StopMoving();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "staff")
        {
            weapon = true;
            animator.SetBool("weapon", true);
        }
    }
}
