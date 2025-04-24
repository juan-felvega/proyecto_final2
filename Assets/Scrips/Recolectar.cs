using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolectar : MonoBehaviour
{
    public InventoryUI inventoryUI;  // Necesitamos el UI del inventario

    // Variables para referirse a los �tems
    public Item pocionItem;
    public Item polloItem;
    public Item cervezaItem;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificamos el tipo de objeto con el que el jugador colisiona
        if (collision.CompareTag("Pocion"))
        {
            // Si es una poci�n, agregarla al inventario
            AddItemToInventory(pocionItem);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Pollo"))
        {
            // Si es un pollo, agregarlo al inventario
            AddItemToInventory(polloItem);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Cerveza"))
        {
            // Si es cerveza, agregarla al inventario
            AddItemToInventory(cervezaItem);
            Destroy(collision.gameObject);
        }
    }

    // M�todo para agregar el �tem al inventario
    void AddItemToInventory(Item item)
    {
        bool added = Inventory.instance.Add(item); // Usamos el m�todo Add de Inventory para agregar el �tem

        if (added)
        {
            Debug.Log("�tem agregado al inventario: " + item.itemName);
        }
        else
        {
            Debug.Log("No se pudo agregar el �tem: Inventario lleno.");
        }
    }
}
