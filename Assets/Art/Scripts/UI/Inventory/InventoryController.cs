using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage InventoryUI;

    public int inventorySize = 10;

    private void Start()
    {
        InventoryUI.InitializeInventoryUI(inventorySize);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            if (InventoryUI.isActiveAndEnabled == false)
            {
                InventoryUI.Show();
            }
            else
            {
                InventoryUI.Hide();
            }
        }
    }
}
