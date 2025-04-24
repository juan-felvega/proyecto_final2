using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item item;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool pickedUp = Inventory.instance.Add(item);
            if (pickedUp)
                Destroy(gameObject);
        }
    }
}
