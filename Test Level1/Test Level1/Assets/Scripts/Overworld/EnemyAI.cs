using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAI : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject battleManager;

    bool isFollowing = false;

    private float distance = 2;
    private Transform target;
    private Player thePlayer;
    private float chaseRange= 0.6f;

    public int currentPatrolPoint = 0;
    Vector2 patrolPointOffset;
    public Vector2[] patrolPoints;
    Vector2[] worldspacePatrolPoints;

    //Movement
    private Rigidbody2D rb;
    public float speed = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        thePlayer = FindObjectOfType<Player>();

        //Raycast down to perfectly offset waypoints to the height of the ground.
        RaycastHit2D groundHit = Physics2D.Raycast(rb.position, Vector2.down, 5.0f);

        patrolPointOffset = new Vector2(0.0f, groundHit.distance);

        worldspacePatrolPoints = new Vector2[patrolPoints.Length];
        for (int i = 0; i < patrolPoints.Length; ++i)
        {
            worldspacePatrolPoints[i] = patrolPoints[i] + rb.position + patrolPointOffset;
        }
    }

    void Update()
    {
        //if below this distance, follow is true
        //else pathfind 
        if(GameObject.Find("battleManager").GetComponent<BattleScript>().inBattle == false)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        

        if (Vector2.Distance(transform.position, target.position) < distance)// && Vector2.Distance(transform.position, target.position) > chaseRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            isFollowing = true;
        }

        else
        {

            float distToNextPoint = Vector2.Distance(worldspacePatrolPoints[currentPatrolPoint], rb.position);
            if (distToNextPoint <= 0.5f)
            {
                //At patrol point.
                if (currentPatrolPoint < patrolPoints.Length - 1)
                {
                    currentPatrolPoint++;
                }
                else
                {
                    currentPatrolPoint = 0;
                }
            }

        }
      
    }

    private float velRef;
    private void FixedUpdate()
    {
        if (!isFollowing)
        {
            Vector2 vel = (worldspacePatrolPoints[currentPatrolPoint] - rb.position).normalized;

            float smoothedX = Mathf.SmoothDamp(rb.velocity.x, vel.x * speed, ref velRef, 0.25f, Mathf.Infinity, Time.fixedDeltaTime);

            // rb.AddForce(vel * Time.fixedDeltaTime * 500);
            rb.velocity = new Vector2(smoothedX, rb.velocity.y);
        }
        else
        {
            isFollowing = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            GameObject colEnemy = Instantiate(Prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            battleManager.GetComponent<BattleScript>().StartBattle(colEnemy);

            Destroy(this.gameObject);

        }
    }

    //Draw Patrol Points
    private void OnDrawGizmosSelected()
    {
        if (patrolPoints != null)
        {
            Gizmos.color = Color.red;
            float size = 0.25f;

            for (int i = 0; i < patrolPoints.Length; ++i)
            {
                Vector2 worldspacePoint2D = (Application.isPlaying) ? worldspacePatrolPoints[i] : patrolPoints[i] + new Vector2(transform.position.x, transform.position.y) + patrolPointOffset;
                Vector3 worldspacePoint3D = new Vector3(worldspacePoint2D.x, worldspacePoint2D.y, 0.0f);
                Gizmos.DrawLine(worldspacePoint3D - Vector3.up * size, worldspacePoint3D + Vector3.up * size);
                Gizmos.DrawLine(worldspacePoint3D - Vector3.left * size, worldspacePoint3D + Vector3.left * size);
            }
        }
    }
}
