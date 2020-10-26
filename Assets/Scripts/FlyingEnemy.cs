using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Source File Name: FlyingEnemy
 * Author's Name: Phoenix Makins
 * Student Number: 101193192
 * Date Last Modified: 2020-10-25
 * Program Description: Moves flying enemies
 * Revision History: created it, Added Internal documentation
 */
public class FlyingEnemy : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        MoveLeft();
        MoveRight();
        MoveDown();
        MoveUp();
    }
    // Moves enemy left towards the player and flips their sprite so they face left
    public void MoveLeft()
    {
        var pos = GameObject.Find("player").transform.position;

        if (transform.position.x > pos.x)
        {
            Vector3 characterScale = transform.localScale;
            if (speed > 0)
            {
                characterScale.x = -4;
            }
            transform.localScale = characterScale;
            transform.Translate(new Vector2(-speed, 0) * Time.deltaTime);
        }
    }
    // Moves enemy right towards the player and flips their sprite so they face right
    public void MoveRight()
    {
        var pos = GameObject.Find("player").transform.position;

        if (transform.position.x < pos.x)
        {
            Vector3 characterScale = transform.localScale;
            if (speed > 0)
            {
                characterScale.x = 4;
            }
            transform.localScale = characterScale;
            transform.Translate(new Vector2(speed, 0) * Time.deltaTime);
        }
    }
    // Moves enemy down towards the player 
    public void MoveDown()
    {
        var pos = GameObject.Find("player").transform.position;

        if (transform.position.y > pos.y)
        {
            transform.Translate(new Vector2(0, -speed) * Time.deltaTime);
        }
    }
    // Moves enemy up towards the player
    public void MoveUp()
    {
        var pos = GameObject.Find("player").transform.position;

        if (transform.position.y < pos.y)
        {
            transform.Translate(new Vector2(0, speed) * Time.deltaTime);
        }
    }
}
