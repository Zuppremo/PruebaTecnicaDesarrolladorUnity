using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth 
{
    public int MaxHealth { get;}
    public int CurrentHealth { get;}

    void ReceiveDamage(int amount);
}
