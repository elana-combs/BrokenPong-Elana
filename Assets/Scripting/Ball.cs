using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum CollisionTag
    {
        BounceWall,
        Player,
        ScoreWall
        
    }


    [SerializeField] private float speed = 8f;
    [SerializeField] private List<string> collisionTags;
    
    private Vector2 direction;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioClip wallBounceSound;
    [SerializeField] private AudioClip playerHitSound;

    private float movementRange = 1f; //range is -1, 1 -- this is to fill a variable in ranges below.
    void Start()
    {
        ResetBall();
        audioSource = GetComponent<AudioSource>();
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
    //randomizes the range of a dedicated float
    private float RandomizeRange()
    {
        float range = Random.Range(-movementRange, movementRange);
        return range;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        //When the ball collides with the ScoreWall, the scoreSound will play, the ball will reset, and the score will increase for the respective player.
        if (other.CompareTag(collisionTags[(int)CollisionTag.ScoreWall]))
        {
            audioSource.clip = scoreSound;
            audioSource.Play();
            ResetBall();
            GameManager.IncrementScore(other.GetComponent<ScoreWall>().scoringPlayer);
        }

        //When the ball collides with the BounceWall, the wallBounceSound will play, and the ball will bounce off the wall.
        else if (other.CompareTag(collisionTags[(int) CollisionTag.BounceWall]))
        {
            audioSource.clip = wallBounceSound;
            audioSource.Play();
            direction.y = -direction.y;
        }
        
        //When the ball collides with the Player, the playerHitSound will play, the direction of the ball will change, and the speed will slightly increase.
        else if (other.CompareTag(collisionTags[(int)CollisionTag.Player]))
        {
            audioSource.clip = playerHitSound;
            audioSource.Play();
            direction.x = -direction.x;
            direction.y = transform.position.y - other.transform.position.y;
            direction = direction.normalized;
        }
    }

    
}
