using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedBullet : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 2.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision.gameObject.GetComponent<Unit>().Healt -= LevelManager.Instance.Damage;

        if (collision.gameObject.GetComponent<Unit>().Healt < 1 && collision.gameObject.GetComponent<Unit>().Name == "Unit-Level1")
        {
            // LevelManager.Instance.Gold += 1;
        }
        if (collision.gameObject.GetComponent<Unit>().Healt < 1 && collision.gameObject.GetComponent<Unit>().Name == "Unit-Level2")
        {
            //LevelManager.Instance.Gold += 2;
        }
        if (collision.gameObject.GetComponent<Unit>().Healt < 1 && collision.gameObject.GetComponent<Unit>().Name == "Unit-Level3")
        {
            //LevelManager.Instance.Gold += 4;
        }
        if (collision.gameObject.GetComponent<Unit>().Healt < 1)
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
