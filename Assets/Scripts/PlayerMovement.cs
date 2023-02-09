using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private Vector2 movement;

    private float halfWidth;
    private float halfHeight;

    private Vector3Int min;
    private Vector3Int max;
    [SerializeField] private Tilemap bounds;

    [SerializeField] private float speed;

    public AudioSource walkSound;

    public void ForceMove(float x, float y)
    {
        movement.x = x;
        movement.y = y;
    }


    void Start()
    {
        min = bounds.cellBounds.min;
        max = bounds.cellBounds.max;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    void Update()
    {

        if (!GameManager.instance.inGame)
        {
            animator.SetFloat("Speed", 0);
            return;
        };

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (!(movement.x == 0) || !(movement.y == 0))
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        walkSound.enabled = movement.x != 0 || movement.y != 0;
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.inGame) return;

        Vector3 newPos = rb.position + movement * speed * Time.fixedDeltaTime;
        if (bounds.GetTile(new Vector3Int(Mathf.FloorToInt(newPos.x), Mathf.FloorToInt(newPos.y))) == null) return;

        rb.MovePosition(newPos);

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        float camX = Mathf.Clamp(transform.position.x, min.x + halfWidth, max.x - halfWidth);
        float camY = Mathf.Clamp(transform.position.y, min.y + halfHeight, max.y - halfHeight);
        Camera.main.transform.position = new Vector3(camX, camY, Camera.main.transform.position.z);
    }
}
