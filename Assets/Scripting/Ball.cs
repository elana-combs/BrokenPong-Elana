using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum CollisionTag
    {
        ScoreWall,
        BounceWall,
        Player
    }


    [SerializeField] private float speed = 8f;
    [SerializeField] private List<string> tags;
    
    private Vector2 direction;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip1;
    [SerializeField] private AudioClip clip2;
    [SerializeField] private AudioClip clip3;

    private float movementRange = 1f; //range is -1, 1 -- this is to fill a variable in ranges below.
    void Start()
    {
        ResetBall();
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    //Resets the ball position to the center.
    private void ResetBall()
    {
        transform.position = Vector2.zero;
        direction = new Vector2(RandomizeRange(), RandomizeRange()).normalized;
    }

    private float RandomizeRange()
    {
        float range = Random.Range(-movementRange, movementRange);
        return range;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tags[(int)CollisionTag.ScoreWall]))
        {
            ResetBall();
        }
        else if (other.CompareTag(tags[(int) CollisionTag.BounceWall]))
        {
            direction.y = -direction.y;
        }
        else if (other.CompareTag(tags[(int)CollisionTag.Player]))
        {
            direction.x = -direction.x;
            direction.y = transform.position.y - other.transform.position.y;
            direction = direction.normalized;
        }
    }
}
