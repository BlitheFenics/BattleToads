using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Source File Name: WeaponPickup
 * Author's Name: Phoenix Makins
 * Student Number: 101193192
 * Date Last Modified: 2020-10-25
 * Program Description: spawns the weapon at a random location and dissapears from the map when the player touches it
 * Revision History: created it, Added Internal documentation
 */
public class WeaponPickup : MonoBehaviour
{
    float x;
    // Start is called before the first frame update
    void Start()
    {
        // Sets a random number from 0 - 53 to the variable x
        x = Random.Range(0, 53);

        // Sets the x value to the objects x positition
        transform.Translate(new Vector2(x, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Deletes the object if it collides with the player

        if (collision.gameObject.name == "player")
        {
            Destroy(gameObject);
        }
    }
}
