using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExplosionBird : Bird
{
    [SerializeField] private float explosionForce = 0.4F;
    [SerializeField] private float explosionRadius = 0.5F;
    [SerializeField] private GameObject explosionParticlesGO = default;
    [SerializeField] private LayerMask layerToExplode;
    private bool hasBirdExploded = false;
    private Rigidbody2D rb;

    public event Action BirdExplodedAction;

    private void Start()
    {
        explosionParticlesGO.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
    }
    private void ExplodeBird()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layerToExplode);

        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            if (obj.GetComponent<Rigidbody2D>() != null)
            {
                obj.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce);
                explosionParticlesGO.SetActive(true);
                BirdExplodedAction?.Invoke();

                if (obj.GetComponent<Pig>() != null)
                    obj.GetComponent<Pig>().ReceiveDamage(50);

                StartCoroutine(DissapearBirdAfterTime());
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && State == BirdState.Thrown)
        {
            if (!hasBirdExploded)
            {
                hasBirdExploded = true;
                ExplodeBird();
            }
        }
    }
    public override void ShootBird()
    {
        base.ShootBird();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private IEnumerator DissapearBirdAfterTime()
    {
        yield return new WaitForSeconds(3F);
        gameObject.SetActive(false);
    }
}
