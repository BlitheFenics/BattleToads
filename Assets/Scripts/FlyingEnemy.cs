using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void MoveDown()
    {
        var pos = GameObject.Find("player").transform.position;

        if (transform.position.y > pos.y)
        {
            transform.Translate(new Vector2(0, -speed) * Time.deltaTime);
        }
    }
    public void MoveUp()
    {
        var pos = GameObject.Find("player").transform.position;

        if (transform.position.y < pos.y)
        {
            transform.Translate(new Vector2(0, speed) * Time.deltaTime);
        }
    }
}
