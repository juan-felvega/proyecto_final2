using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text amountText;

    private Item item;

    public void AddItem(ItemInstance itemInstance)
    {
        item = itemInstance.item;
        icon.sprite = item.icon;
        icon.enabled = true;

        if (item.isStackable && itemInstance.amount > 1)
            amountText.text = itemInstance.amount.ToString();
        else
            amountText.text = "";
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        amountText.text = "";
    }
}
