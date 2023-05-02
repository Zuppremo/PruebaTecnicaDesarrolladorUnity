using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour, IHealth
{
    [SerializeField] private int maxHealth = 70;
    private int currentHealth;

    public int MaxHealth => maxHealth;

    public int CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) 
            return;

        if (col.gameObject.GetComponent<Bird>())
        {
            GetComponent<AudioSource>().Play();
            ReceiveDamage(CalculateDamage(col));
        }

        if (CurrentHealth <= 0) 
            Destroy(this.gameObject);

    }

    public void ReceiveDamage(int amount)
    {
        currentHealth -= amount;
    }

    private int CalculateDamage(Collision2D col)
    {
        int damage = (int)col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
        return damage;
    }
}
