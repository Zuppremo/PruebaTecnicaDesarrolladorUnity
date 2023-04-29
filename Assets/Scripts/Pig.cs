using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour, IHealth
{
    [SerializeField] private int maxHealth = 150;
    [SerializeField] private int currentHealth;
    public Sprite SpriteShownWhenHurt;
    private int hurtPigHealth = 130;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        hurtPigHealth = CurrentHealth - 30;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) 
            return;

        if (col.gameObject.CompareTag("Bird"))
        {
            GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        else
        {
            int damage = (int) col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            ReceiveDamage(damage);
            GetComponent<AudioSource>().Play();
            switch (currentHealth)
            {
                case 120:
                    GetComponent<SpriteRenderer>().sprite = SpriteShownWhenHurt;
                    break;
                case 0:
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
    public void ReceiveDamage(int amount)
    {
        currentHealth -= amount;
    }
}
