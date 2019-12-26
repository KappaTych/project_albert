using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Enemy
    public int playerDamage;
    //private Animator animator;
    private Transform target;

    // moving Object
    //Time it will take object to move, in seconds.
    public float moveTime = 0.1f;
    //Layer on which collision will be checked.
    public LayerMask blockingLayer;

    //The BoxCollider2D component attached to this object.
    private Collider2D col;
    //The Rigidbody2D component attached to this object.
    private Rigidbody2D rb2D;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //animator = GetComponent<Animator>();

        col = GetComponent<Collider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        prepareMove();
        if (isActiveMove)
            SmoothMovement();
    }

    private Vector3 end = new Vector3(0, 0, 0);
    private Vector2 prev_dir = new Vector2(0, 0);
    private float sqrRemainingDistance = 0;
    private bool isActiveMove = true;
    private void SmoothMovement()
    {
        //var rb = gameObject.GetComponent<Rigidbody2D>();

        //rb.velocity = prev_dir * moveTime;
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        isActiveMove = sqrRemainingDistance > float.Epsilon;
        //Find a new position proportionally closer to the end, based on the moveTime
        Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, (1f / moveTime) * Time.deltaTime);
        //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
        rb2D.MovePosition(newPostion);
        //Recalculate the remaining distance after moving.
        sqrRemainingDistance = (transform.position - end).sqrMagnitude;
    }


    private void prepareMove()
    {
        //Declare variables for X and Y axis move directions, these range from -1 to 1.
        //These values allow us to choose between the cardinal directions: up, down, left and right.
        int xDir = 0;
        int yDir = 0;

        //If the difference in positions is approximately zero (Epsilon) do the following:
        if (Mathf.Abs(target.position.y - transform.position.y) > float.Epsilon)
            //If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
            yDir = target.position.y > transform.position.y ? 1 : -1;
        //If the difference in positions is not approximately zero (Epsilon) do the following:
        if (Mathf.Abs(target.position.x - transform.position.x) > float.Epsilon)
            //Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
            xDir = target.position.x > transform.position.x ? 1 : -1;

        var dir = new Vector2(xDir, yDir);
        if (isActiveMove && dir == prev_dir)
            return;

        Vector2 start_move = transform.position;
        Vector2 end_move = start_move + new Vector2(xDir, yDir);

        //Disable the boxCollider so that linecast doesn't hit this object's own collider.
        col.enabled = false;
        //Hit will store whatever our linecast hits when Move is called.
        //Cast a line from start point to end point checking collision on blockingLayer.
        RaycastHit2D hit = Physics2D.Linecast(start_move, end_move, (int)blockingLayer);
        //Re-enable boxCollider after linecast
        col.enabled = true;

        //Check if anything was hit
        isActiveMove = hit.transform == null;
        if (isActiveMove)
        {
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            end = end_move;
            prev_dir = dir;
        }

        //Get a component reference to the component of type T attached to the object that was hit
        //var entity = hit.transform.gameObject.GetEntity<CoreEntity>();
        //If canMove is false and hitComponent is not equal to null, meaning MovingObject is blocked and has hit something it can interact with.
        //if (!canMove && entity != null)
        //{
            //Call the LoseFood function of hitPlayer passing it playerDamage, the amount of foodpoints to be subtracted.
            //hitPlayer.LoseFood(playerDamage);
            //Set the attack trigger of animator to trigger Enemy attack animation.
            //animator.SetTrigger("enemyAttack");
        //}
    }
}
