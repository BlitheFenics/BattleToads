using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Source File Name: PlayerController
 * Author's Name: Phoenix Makins
 * Student Number: 101193192
 * Date Last Modified: 2020-12-13
 * Program Description: Takes player input, plays sound effects for the player, plays player animations, manages incoming and outgoing damage for the player, applies score values for the player, allows player to enter portal, allows player to jump
 * Revision History: created it, Added movement, added animations, added pickups, addedd health, added sound effects, added win/lose conditions, Added Internal documentation, added ability to jump
 */
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
    public bool jumpUp;
    public bool dontJump;
    public bool isGrounded = false;

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

        // Checks to see if the player is touching the ground. If they are switch to jump animation, if they aren't switch to idle animation
        if(isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }
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
        if(dontJump)
        {
            StopJumping();
        }
        else
        {
            if(jumpUp)
            {
                Jump();
            }
        }
    }

    // Checks to see if the player can jump and allows them to if they can, doesn;t allow them if they can't
    public void AllowJump(bool jump)
    {
        dontJump = false;
        jumpUp = jump;
    }
    public void DontAllowJump()
    {
        dontJump = true;
    }

    // If the player hits the 'B' button it applies a force to the player, removes the force otherwise
    public void Jump()
    {
        if (isGrounded == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
        }
    }
    public void StopJumping()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 0f), ForceMode2D.Impulse);
    }

    // Checks to see if the player is or isnt moving
    public void AllowMovement(bool movement)
    {
        dontMove = false;
        moveLeft = movement;
    }
    public void DontAllowMovement()
    {
        dontMove = true;
    }
    // Checks to see if the player is or isnt attacking
    public void Attacking(bool attack)
    {
        dontAttack = false;
        attacking = attack;
    }
    public void NotAttacking()
    {
        dontAttack= true;
    }
    // Moves the player left when they touch the left arrow button, flips their sprite left and plays the walk animation
    public void MoveLeft()
    {
        Vector3 characterScale = player.localScale;
        if(speed > 0)
        {
            characterScale.x = -4;
        }
        player.localScale = characterScale;
        Vector3 movement = new Vector3(-speed, 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        animator.SetFloat("speed", Mathf.Abs(speed));

        
    }
    // Moves the player right when they touch the right arrow button, flips their sprite right and plays the walk animation
    public void MoveRight()
    {
        Vector3 characterScale = player.localScale;
        if (speed > 0)
        {
            characterScale.x = 4;
        }
        player.localScale = characterScale;
        Vector3 movement = new Vector3(speed, 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        animator.SetFloat("speed", Mathf.Abs(speed));
    }
    // Makes the player idle and plays the idle animation
    public void StopMoving()
    {
        player.Translate(new Vector2(0, 0) * Time.deltaTime);
        animator.SetFloat("speed", 0);
    }
    // Plays the attack animation and sets an attacking bool to true
    public void Attack()
    {
        attack = true;
        animator.SetBool("attack", true);
    }
    // Stops playing the attack animation and sets an attacking bool to false
    public void StopAttacking()
    {
        attack = false;
        animator.SetBool("attack", false);
    }
    // Move the player Kinematically left or right
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
    // Checks colliosions with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks for collision with the staff plays a sound applies a score and switches all damage values and animations for the player to their weapon variant
        if (collision.gameObject.name == "staff")
        {
            weapon = true;
            animator.SetBool("weapon", true);
            audioSource.PlayOneShot(pickUp);
            Score.scoreValue += 100;
        }
        // Checks for collision with the dragon
        if (collision.gameObject.name == "Dragon")
        {
            // Applies damage to the player and plays a sound
            if (attack == false)
            {
                TakeDamage(2);
                audioSource.PlayOneShot(hurt);
            }
            if (attack == true)
            {
                // player attacks with fist and deals damage and plays a sound
                if (weapon == false)
                {
                    dragonHealth -= 1;
                    audioSource.PlayOneShot(punch);
                }
                // player attacks with weapon deals damage and plays a sound
                if (weapon == true)
                {
                    dragonHealth -= 2;
                    audioSource.PlayOneShot(swing);
                }
            }
            // Deletes the dragon when its health is 0 and applies a score
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
        // Checks for collision with the shovel enemy
        if (collision.gameObject.name == "Shovel Enemy")
        {
            // Applies damage to the player and plays a sound
            if (attack == false)
            {
                TakeDamage(2);
                audioSource.PlayOneShot(hurt);
            }
            if (attack == true)
            {
                // player attacks with fist and deals damage and plays a sound
                if (weapon == false)
                {
                    shovelHealth -= 1;
                    audioSource.PlayOneShot(punch);
                }
                // player attacks with weapon deals damage and plays a sound
                if (weapon == true)
                {
                    shovelHealth -= 2;
                    audioSource.PlayOneShot(swing);
                }
            }
            // Deletes the shovel enemy when its health is 0 and applies a score
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
        // Checks for collision with the bird
        if (collision.gameObject.name == "bird")
        {
            // Applies damage to the player and plays a sound
            if (attack == false)
            {
                TakeDamage(1);
                audioSource.PlayOneShot(hurt);
            }
            if (attack == true)
            {
                // player attacks with fist and deals damage and plays a sound
                if (weapon == false)
                {
                    birdHealth -= 1;
                    audioSource.PlayOneShot(punch);
                }
                // player attacks with weapon deals damage and plays a sound
                if (weapon == true)
                {
                    birdHealth -= 2;
                    audioSource.PlayOneShot(swing);
                }
            }
            // Deletes the bird when its health is 0 and applies a score
            if (birdHealth <= 0)
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
        // Checks for collision with the mouse
        if (collision.gameObject.name == "mouse")
        {
            // Applies damage to the player and plays a sound
            if (attack == false)
            {
                TakeDamage(1);
                audioSource.PlayOneShot(hurt);
            }
            if (attack == true)
            {
                // player attacks with fist and deals damage and plays a sound
                if (weapon == false)
                {
                    mouseHealth -= 1;
                    audioSource.PlayOneShot(punch);
                }
                // player attacks with weapon deals damage and plays a sound
                if (weapon == true)
                {
                    mouseHealth -= 2;
                    audioSource.PlayOneShot(swing);
                }
            }
            // Deletes the mouse when its health is 0 and applies a score
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with the platform this adds it's movement to their own
        if (collision.gameObject.name == "platform")
        {
            this.transform.parent = collision.transform;
        }

        // Checks for collision for the portal and switches the scene to then end game scene
        if (collision.gameObject.name == "portal")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // If the player stops colliding with the platform this stops them from adding it's movement to their own
        if (collision.gameObject.name == "platform")
        {
            this.transform.parent = null;
        }
    }

    // If the players health gets to 0 it takes them to the endgame screen
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
