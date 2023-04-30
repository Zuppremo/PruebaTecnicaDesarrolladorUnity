using UnityEngine;
using System.Collections;
using Assets.Scripts;

[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour

{
    [SerializeField] private float minVelocity = 0.05f;
    [SerializeField] private float birdCollider = 0.235f;
    [SerializeField] private float birdColliderBig = 0.5f;
    public BirdState State { get; private set; }


    private void Start()
    {
        GetReadyBirdToThrow();
        State = BirdState.BeforeThrown;
    }
    public virtual void ShootBird()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<TrailRenderer>().enabled = true; 
        GetComponent<Rigidbody2D>().isKinematic = false; 
        GetComponent<CircleCollider2D>().radius = birdCollider; 
        State = BirdState.Thrown;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Pig>() != null)
            collision.gameObject.GetComponent<Pig>().ReceiveDamage(50);
    }


    private void GetReadyBirdToThrow()
    {
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<TrailRenderer>().sortingLayerName = "Foreground";
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<CircleCollider2D>().radius = birdColliderBig;
    }
}
