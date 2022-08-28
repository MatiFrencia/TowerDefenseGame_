using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Instance of the class
    [Header("Turret Attributes")]
    public static Turret Instance;
    public Transform ShootPoint;
    public GameObject Bullet;

    [Header("Turret Properties")]
    public int Lives = 5;
    public int CurrentLives = 5;
    public float ViewDistance;
    public float FireRate;
    public float Force;
    public float Speed = 1.5f;
    public bool isChildTurret = false;

    private float nextTimeToFire = 0;
    private Transform Target;
    private Vector3 closeEnemyRef;
    private Vector2 Direction;
    private bool Detected = false;


    void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentLives <= 0)
        {
            CurrentLives = 0;
            Destroy(gameObject);
            if (!isChildTurret)
            {
                Time.timeScale = 0;
                //scene management
                return;
            }
        }

        if (Target != null)
        {
            Vector2 targetPose = Target.position;
            Direction = targetPose - (Vector2)transform.position;
            RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, ViewDistance);
            if (rayInfo)
            {
                if (rayInfo.collider.gameObject.tag == "Unit")
                {
                    if (Detected == false)
                    {
                        Detected = true;
                    }
                }
                else
                {
                    if (Detected == true)
                    {
                        Detected = false;
                    }
                }
            }
            if (Detected)
            {
                if (Time.time > nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / FireRate;
                    Shoot();
                }
            }
        }
    }
    void Shoot() 
    {
        //LevelManager.Instance.CalculateCriticalFactor();
        GameObject BulletIns = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force * Speed);
        //LevelManager.Instance.Damage = LevelManager.Instance.oldDamage;
    }


    private void OnDrawGizmosSelected()
    {
        //Draw View Distance
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ViewDistance);
        // Draw View Deaw Collision Area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .25f);
        // Draw View Draw Enemy distance from player
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, closeEnemyRef);
    }
    
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Unit");
        float shortDistance = Mathf.Infinity;
        GameObject closeEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (isChildTurret && distance <= .35f)
            {
                Destroy(gameObject);
            }
            if (distance < shortDistance)
            {
                shortDistance = distance;
                closeEnemy = enemy;
                closeEnemyRef = enemy.transform.position;
            }
        }
        if (closeEnemy != null && shortDistance <= ViewDistance)
        {
            Target = closeEnemy.transform;
        }
        else
        {
            Target = null;
        }
    }

}
