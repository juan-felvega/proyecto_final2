using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public Item item;
    public int amount;

    public ItemInstance(Item item, int amount = 1)
    {
        this.item = item;
        this.amount = amount;
    }
}
