using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Source File Name: Grounded
 * Author's Name: Phoenix Makins
 * Student Number: 101193192
 * Date Last Modified: 2020-12-13
 * Program Description: Checks to see if the player is touching the ground
 * Revision History: created it, Added ground check
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
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<PlayerController>().isGrounded = false;
        }
    }
}
