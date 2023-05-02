using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleBird : Bird
{
    [SerializeField] private float copiesScale = 0.8F;
    [SerializeField] private GameObject birdCopy = default;
    [SerializeField] private GameObject birdPositionUp = default;
    [SerializeField] private GameObject birdPositionDown = default;
    private bool isBirdMultiplied = false;
    private void MultiplyBird()
    {
        if (!isBirdMultiplied)
        {
            isBirdMultiplied = true;
            GameObject birdCloneUp = Instantiate(birdCopy, birdPositionUp.transform.position, Quaternion.Euler(0,0,45));
            GetVelocityAndAngularSpeed(birdCloneUp);
            GameObject birdCloneMid = Instantiate(birdCopy, transform.position, Quaternion.identity);
            GetVelocityAndAngularSpeed(birdCloneMid);
            GameObject birdCloneDown = Instantiate(birdCopy, birdPositionDown.transform.position, Quaternion.Euler(0, 0, -45));
            GetVelocityAndAngularSpeed(birdCloneDown);
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && State == BirdState.Thrown)
            MultiplyBird();
    }

    public override void ShootBird()
    {
        base.ShootBird();
    }
    private void GetVelocityAndAngularSpeed(GameObject clone)
    {
        ChangeScaleOfCopies(clone);
        clone.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * 0.7F;
        clone.GetComponent<Rigidbody2D>().angularVelocity = GetComponent<Rigidbody2D>().angularVelocity * 0.7F;
    }

    private void ChangeScaleOfCopies(GameObject clone)
    {
        clone.transform.localScale -= transform.localScale * copiesScale;
    }

    private IEnumerator DissapearBirdAfterTime()
    {
        yield return new WaitForSeconds(2F);
        gameObject.SetActive(false);
    }

}
