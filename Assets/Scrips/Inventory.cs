using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    void Awake() { instance = this; }

    public int space = 20;

    public List<ItemInstance> items = new List<ItemInstance>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public bool Add(Item item)
    {
        // Si es stackeable y ya lo tenemos, sumamos cantidad
        if (item.isStackable)
        {
            foreach (ItemInstance i in items)
            {
                if (i.item == item)
                {
                    i.amount++;
                    onItemChangedCallback?.Invoke();
                    return true;
                }
            }
        }

        // Si hay espacio, añadimos nuevo
        if (items.Count >= space)
        {
            Debug.Log("Inventario lleno");
            return false;
        }

        items.Add(new ItemInstance(item));
        onItemChangedCallback?.Invoke();
        return true;
    }

    public void Remove(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
            {
                if (items[i].amount > 1)
                {
                    items[i].amount--;
                }
                else
                {
                    items.RemoveAt(i);
                }

                onItemChangedCallback?.Invoke();
                return;
            }
        }
    }
}
