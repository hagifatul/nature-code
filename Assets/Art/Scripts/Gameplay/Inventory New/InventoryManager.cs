using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject settingMenuPanel;
    [SerializeField] private GameObject pilihanMenuPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject storePanel;

    void Start()
    {
        
    }

    void Update()
    {
        if (storePanel.activeSelf || pilihanMenuPanel.activeSelf)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
                inventoryPanel.SetActive(false);
            }
            else
            {
                inventoryPanel.SetActive(true);
            }

        }
    }

