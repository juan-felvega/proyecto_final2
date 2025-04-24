using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolectar : MonoBehaviour
{
    public InventoryUI inventoryUI; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pocion") || collision.CompareTag("Pollo") || collision.CompareTag("Cerveza"))
        {
            string itemName = collision.tag;
            //inventoryUI.AgregarItem(itemName);
            Destroy(collision.gameObject);
        }
    }
}