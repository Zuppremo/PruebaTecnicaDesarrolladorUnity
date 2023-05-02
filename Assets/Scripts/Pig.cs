using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour, IHealth
{
    [SerializeField] private int maxHealth = 150;
    private int currentHealth;
    public Sprite SpriteShownWhenHurt;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (currentHealth <= 0)
            Destroy(gameObject);
        else if (currentHealth <= 120)
            GetComponent<SpriteRenderer>().sprite = SpriteShownWhenHurt;

        if (col.gameObject.GetComponent<Rigidbody2D>() == null) 
            return;

        if (col.gameObject.GetComponent<Bird>())
            ReceiveDamage(25);
        else
            ReceiveDamage(CalculateDamage(col));
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
