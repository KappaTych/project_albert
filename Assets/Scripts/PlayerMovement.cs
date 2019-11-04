using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum Direction
    {
        Left,
        Right
    }

	private Rigidbody2D rb;
	private float moveH, moveV;
	[SerializeField] private float moveSpeed = 2.0f;

    Direction direction = Direction.Left;
    Vector2 position;

    private void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        position = rb.position;
    }

    private void Update()
    {
        moveH = Input.GetAxisRaw("Horizontal") * moveSpeed;
		moveV = Input.GetAxisRaw("Vertical") * moveSpeed;
        //Direction newDirection = direction;
        //if (position.x < rb.position.x) {
        //    newDirection = Direction.Left;
        //} else if (position.x > rb.position.x) {
        //    newDirection = Direction.Right;
        //}
        //if (newDirection != direction) {
        //    rb.transform.localScale = new Vector3(-rb.transform.localScale.x, rb.transform.localScale.y, rb.transform.localScale.z);
        //}
        //position = rb.position;
        //direction = newDirection;
    }

	private void FixedUpdate()
	{
        rb.velocity = new Vector2(moveH, moveV);
	}
}
