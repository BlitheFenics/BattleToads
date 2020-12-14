using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Source File Name: Grounded
 * Author's Name: Phoenix Makins
 * Student Number: 101193192
 * Date Last Modified: 2020-12-14
 * Program Description: Checks to see if the player is touching the ground, checks to see if the player is touching a platform
 * Revision History: created it, Added ground check, added platform check
 */

public class Grounded : MonoBehaviour
{
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
    }

    // Tells the player whether or not their colliding with the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<PlayerController>().isGrounded = true;
        }

        // If the player collides with the platform this adds it's movement to their own
        if (collision.gameObject.name == "platform")
        {
            this.transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<PlayerController>().isGrounded = false;
        }

        // If the player stops colliding with the platform this stops them from adding it's movement to their own
        if (collision.gameObject.name == "platform")
        {
            this.transform.parent = null;
        }
    }
}
