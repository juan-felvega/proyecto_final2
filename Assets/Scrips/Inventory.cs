using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public int space = 20;
    public List<ItemInstance> items = new List<ItemInstance>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    void Awake()
    {
        // Si ya existe una instancia distinta, la eliminamos
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Asignamos la instancia y la hacemos persistente
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

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
