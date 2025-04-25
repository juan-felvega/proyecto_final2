using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public GameObject inventoryPanel;
    public GameObject player;

    public Transform itemsParent;
    public InventorySlot[] slots;

    private Inventory inventory;
    private PlayerMovimiento playerMovement;
    private bool isOpen = false;

    void Awake()
    {
        // Prevención de duplicados
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persiste entre escenas
    }

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        SetupPlayerReference();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        UpdateUI();
    }

    void Update()
    {
       if (inventoryPanel == null || inventoryPanel.Equals(null))
    {
        SetupInventoryPanel();
        return; // Evitamos llamar SetActive si aún no existe
    }

    if (Input.GetKeyDown(KeyCode.I))
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);

        if (playerMovement != null)
            playerMovement.canMove = !isOpen;
    }

    if ((player == null || player.Equals(null) || playerMovement == null) && !isOpen)
    {
        SetupPlayerReference();
    }

    // Si el jugador fue destruido, o su referencia está "fantasma", lo buscamos otra vez
    if ((player == null || player.Equals(null) || playerMovement == null) && !isOpen)
    {
        SetupPlayerReference();
    }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
                slots[i].AddItem(inventory.items[i]);
            else
                slots[i].ClearSlot();
        }
    }

    void SetupPlayerReference()
    {
         player = GameObject.FindGameObjectWithTag("Player");

    if (player != null)
    {
        playerMovement = player.GetComponent<PlayerMovimiento>();

        if (playerMovement == null)
            Debug.LogWarning("El objeto con tag 'Player' no tiene el componente PlayerMovimiento.");
    }
    }
    void SetupInventoryPanel()
    {
    inventoryPanel = GameObject.Find("InventoryPanel");

    if (inventoryPanel == null)
    {
        Debug.LogWarning("No se encontró el InventoryPanel en la nueva escena.");
    }
    }

}
