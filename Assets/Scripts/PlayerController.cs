using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthBar;
    public bool attack;
    public int birdHealth;
    public int dragonHealth;
    public int mouseHealth;
    public int shovelHealth;
    public AudioSource audioSource;
    public AudioClip punch;
    public AudioClip pickUp;
    public AudioClip swing;
    public AudioClip hurt;

    // Start is called before the first frame update
    void Start()
    {
        dontMove = true;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        audioSource = GetComponent<AudioSource>();
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
        attack = true;
        animator.SetBool("attack", true);
    }
    public void StopAttacking()
    {
        attack = false;
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
            audioSource.PlayOneShot(pickUp);
            Score.scoreValue += 100;
        }
        
        if (collision.gameObject.name == "Dragon")
        {
            if (attack == false)
            {
                TakeDamage(2);
                audioSource.PlayOneShot(hurt);
            }
            if (attack == true)
            {
                if (weapon == false)
                {
                    dragonHealth -= 1;
                    audioSource.PlayOneShot(punch);
                }
                if (weapon == true)
                {
                    dragonHealth -= 2;
                    audioSource.PlayOneShot(swing);
                }
            }
            if (dragonHealth <= 0)
            {
                Destroy(GameObject.Find("Dragon"));
                if (weapon == false)
                {
                    Score.scoreValue += 20;
                }
                if (weapon == true)
                {
                    Score.scoreValue += 40;
                }
            }
        }

        if (collision.gameObject.name == "Shovel Enemy")
        {
            if (attack == false)
            {
                TakeDamage(2);
                audioSource.PlayOneShot(hurt);
            }
            if (attack == true)
            {
                if (weapon == false)
                {
                    shovelHealth -= 1;
                    audioSource.PlayOneShot(punch);
                }
                if (weapon == true)
                {
                    shovelHealth -= 2;
                    audioSource.PlayOneShot(swing);
                }
            }
            if (shovelHealth <= 0)
            {
                Destroy(GameObject.Find("Shovel Enemy"));
                    if (weapon == false)
                    {
                        Score.scoreValue += 20;
                    }
                    if (weapon == true)
                    {
                        Score.scoreValue += 40;
                    }
            }
        }

        if (collision.gameObject.name == "bird")
        {
            if (attack == false)
            {
                TakeDamage(1);
                audioSource.PlayOneShot(hurt);
            }
            if(attack == true)
            {
                if (weapon == false)
                {
                    birdHealth -= 1;
                    audioSource.PlayOneShot(punch);
                }
                if (weapon == true)
                {
                    birdHealth -= 2;
                    audioSource.PlayOneShot(swing);
                }
            }
            if(birdHealth <= 0)
            {
                Destroy(GameObject.Find("bird"));
                if (weapon == false)
                {
                    Score.scoreValue += 10;
                }
                if (weapon == true)
                {
                    Score.scoreValue += 20;
                }
            }
        }

        if (collision.gameObject.name == "mouse")
        {
            if (attack == false)
            {
                TakeDamage(1);
                audioSource.PlayOneShot(hurt);
            }
            if (attack == true)
            {
                if(weapon==false)
                {
                    mouseHealth -= 1;
                    audioSource.PlayOneShot(punch);
                }
                if(weapon==true)
                {
                    mouseHealth -= 2;
                    audioSource.PlayOneShot(swing);
                }
            }
            if (mouseHealth <= 0)
            {
                Destroy(GameObject.Find("mouse"));
                if (weapon == false)
                {
                    Score.scoreValue += 10;
                }
                if (weapon == true)
                {
                    Score.scoreValue += 20;
                }
            }
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }
}
