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
        var enemy = collision.gameObject.GetComponent<Unit>();
        if (enemy.Healt < 1)
        {
            if (enemy.Name == "Unit-Level1")
            {
                // LevelManager.Instance.Gold += 1;
            }
            else if (enemy.Name == "Unit-Level2")
            {
                //LevelManager.Instance.Gold += 2;
            }
            else if (enemy.Name == "Unit-Level3")
            {
                //LevelManager.Instance.Gold += 4;
            }
            Destroy(collision.gameObject);
        }
        else
        {
            enemy.Healt--;
        }
        Destroy(gameObject);
    }
}
