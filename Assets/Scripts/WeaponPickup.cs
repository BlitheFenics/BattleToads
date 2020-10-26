using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    float x;
    // Start is called before the first frame update
    void Start()
    {
        x = Random.Range(0, 63);

        transform.Translate(new Vector2(x, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            Destroy(gameObject);
        }
    }
}
