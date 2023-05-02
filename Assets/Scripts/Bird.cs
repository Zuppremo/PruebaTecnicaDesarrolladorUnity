using UnityEngine;
using System.Collections;
using Assets.Scripts;

[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{
    [SerializeField] private float minVelocity = 0.05f;
    [SerializeField] private float birdCollider = 0.235f;
    [SerializeField] private float birdColliderBig = 0.5f;
    [SerializeField] private Birds birds = Birds.Normal;
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
        StartCoroutine(DissapearAfterTime());
    }
    private void GetReadyBirdToThrow()
    {
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<TrailRenderer>().sortingLayerName = "Foreground";
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private IEnumerator DissapearAfterTime()
    {
        yield return new WaitForSeconds(5F);
        gameObject.SetActive(false);
    }
}
