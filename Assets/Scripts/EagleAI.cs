using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EagleAI : MonoBehaviour
{

    public Transform target;

    public float speed = 200f;
    public float nextWayPointDistance = 3f;
    [SerializeField] private float damage;
    private int hits = 0;
    private bool isDead = false;
    [SerializeField] private float aggroRange;

    public Transform eagle;

    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;

    private GameObject aux;

    private Seeker seeker;
    private Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("CheckDist", 0, 1f);
    }

    void CheckDist()
    {
        float dist = Vector2.Distance(rb.position, target.position);

        if (dist <= aggroRange)
        {
            InvokeRepeating("UpdatePath", 0, .5f);
        }
        else
        {
            CancelInvoke("UpdatePath");
            path = null;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete (Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }

        if (rb.velocity.x >= 0.01f)
        {
            eagle.localScale = new Vector3(-5f, 5f, 5f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            eagle.localScale = new Vector3(5f, 5f, 5f);
        }
    }
}
