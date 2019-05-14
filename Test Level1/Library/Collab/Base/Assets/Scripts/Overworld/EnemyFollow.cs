using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

    public float speed;
    public float distance;
    private Transform target;
    private Player thePlayer;
    public float chaseRange;

    

    //animation
    private Animator anim;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        thePlayer = FindObjectOfType<Player>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            thePlayer.enemy_contact = true;
        }
    }

    void Update()
    {

        if (Vector2.Distance(transform.position, target.position) < distance && Vector2.Distance(transform.position, target.position) > chaseRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }




       // anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
       // anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
      
       

    }


}
