using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private List<string> tags;
    [SerializeField] private string otherTag;
    private Vector2 position;

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
        transform.Translate(position * speed * Time.deltaTime);
    }

    //Resets the ball position to the center.
    private void ResetBall()
    {
        transform.position = Vector2.zero;
        position = new Vector2(RandomizeRange(), RandomizeRange()).normalized;
    }

    private float RandomizeRange()
    {
        float range = Random.Range(-movementRange, movementRange);
        return range;
    }
 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tags[0]))
        {
            ResetBall();
        }
        else if (other.CompareTag(otherTag))
        {
            position.y = -position.y;
        }
        else if (other.CompareTag("Player"))
        {
            position.x = -position.x;
            position.y = transform.position.y - other.transform.position.y;
            position = position.normalized;
        }
    }
}
