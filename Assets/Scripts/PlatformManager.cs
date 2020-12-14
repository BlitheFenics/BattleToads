using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Source File Name: PlatformManager
 * Author's Name: Phoenix Makins
 * Student Number: 101193192
 * Date Last Modified: 2020-12-13
 * Program Description: Allowes platforms to move left and right and or scale up or down
 * Revision History: created it, Added movement, added scaleing
 */

public class PlatformManager : MonoBehaviour
{
    float moveSpeed = 3f, scaleSize = 2f;
    bool moveRight = true;
    bool scaleUp = true;
    public bool scalePlatform;
    public bool movePlatform;

    // Update is called once per frame
    void Update()
    {
        // Moves the platform left and right based on a range
        if(movePlatform == true)
        {
            if (transform.position.x > 4f)
            {
                moveRight = false;
            }
            if (transform.position.x < -4f)
            {
                moveRight = true;
            }

            if (moveRight)
            {
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            }
        }

        // Scales the platform based on a range
        if(scalePlatform == true)
        {
            if (transform.localScale.x > 1f && transform.localScale.y > 1f)
            {
                scaleUp = false;
            }
            if (transform.localScale.y < 0f && transform.localScale.y < 0f)
            {
                scaleUp = true;
            }

            if (scaleUp)
            {
                Vector2 scale = new Vector2(transform.localScale.x + scaleSize * Time.deltaTime, transform.localScale.y + scaleSize * Time.deltaTime);
                transform.localScale = scale;
            }
            else
            {
                Vector2 scale = new Vector2(transform.localScale.x - scaleSize * Time.deltaTime, transform.localScale.y - scaleSize * Time.deltaTime);
                transform.localScale = scale;
            }
        }
    }
}
