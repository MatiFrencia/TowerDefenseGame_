using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string Name = "Unite-Level1";
    public float Healt = 1;
    public float MoveSpeed = 100f;
    private Rigidbody2D Rb;
    private Vector2 Movement;
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        Rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            return;
        }
        Vector3 direction = Target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        Movement = direction;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, Target.position);
    }

    private void FixedUpdate()
    {
        MoveCharacter(Movement);
    }

    void MoveCharacter(Vector2 direction)
    {
        Rb.MovePosition((Vector2)transform.position + (MoveSpeed * Time.deltaTime * direction));
        
        if (Vector3.Distance(transform.position, Target.position) <= 0.22f)
        {
            Target.GetComponent<Turret>().CurrentLives--;
            Destroy(gameObject);
        }
    }
}
