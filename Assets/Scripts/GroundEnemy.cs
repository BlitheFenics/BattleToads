using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GroundEnemy : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeft();
        MoveRight();
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
}
