using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public class ChestUI : MonoBehaviour
{
    private bool chestOpen = false;
    public bool ChestOpen => chestOpen;
    public GameObject chestParent;
    public GameObject chestTab;
    
    //other tabs
    public GameObject playerTab;
    public GameObject relationTab;
    public GameObject mapTab;
    public GameObject craftingTab;
    public GameObject achievementTab;
    public GameObject settingsTab;
    public GameObject exitTab;

    private List<ItemSlot> itemSlotList = new List<ItemSlot>();
    public GameObject inventorySlotPrefab;
    public Transform itemTransform;

    private void Start()
    {
        Inventory.instance.onItemChange += UpdateInventoryUI;
        UpdateInventoryUI();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (chestOpen)
            {
                CloseUI();
            }

            else
            {
                OpenUI();
            }
        }
    }

    private void UpdateInventoryUI()
    {
        int currentItemCount = Inventory.instance.inventoryItemList.Count;

        if (currentItemCount > itemSlotList.Count)
        {
            AddItemSlots(currentItemCount);
        }

        for (int i = 0; i < itemSlotList.Count; i++)
        {
            if (i <= currentItemCount)
            {
                itemSlotList[i].AddItem(Inventory.instance.inventoryItemList[i]);
            }

            else
            {
                itemSlotList[i].DestroySlot();
                itemSlotList.RemoveAt(i);
            }
        }
    }

    private void AddItemSlots(int currentItemCount)
    {
        int amount = currentItemCount - itemSlotList.Count;

        for (int i = 0; i < amount; i++)
        {
            GameObject GO = Instantiate(inventorySlotPrefab, itemTransform);
            ItemSlot newSlot = GO.GetComponent<ItemSlot>();
            itemSlotList.Add(newSlot);
        }
    }

    private void OpenUI()
    {
        chestOpen = true;
        chestParent.SetActive(true);
    }

    private void CloseUI()
    {
        chestOpen = false;
        chestParent.SetActive(false);
    }

    public void OnChestTabClicked()
    {
        playerTab.SetActive(false);
        chestTab.SetActive(true); //
        relationTab.SetActive(false);
        mapTab.SetActive(false);
        craftingTab.SetActive(false);
        achievementTab.SetActive(false);
        settingsTab.SetActive(false);
        exitTab.SetActive(false);
    }
    public void OnPlayerTabClicked()
    {
        playerTab.SetActive(true); //
        chestTab.SetActive(false);
        relationTab.SetActive(false);
        mapTab.SetActive(false);
        craftingTab.SetActive(false);
        achievementTab.SetActive(false);
        settingsTab.SetActive(false);
        exitTab.SetActive(false);
    }

    public void OnRelationTabClicked()
    {
        playerTab.SetActive(false);
        chestTab.SetActive(false);
        relationTab.SetActive(true); //
        mapTab.SetActive(false);
        craftingTab.SetActive(false);
        achievementTab.SetActive(false);
        settingsTab.SetActive(false);
        exitTab.SetActive(false);
    }

    public void OnMapTabClicked()
    {
        playerTab.SetActive(false);
        chestTab.SetActive(false);
        relationTab.SetActive(false);
        mapTab.SetActive(true); //
        craftingTab.SetActive(false);
        achievementTab.SetActive(false);
        settingsTab.SetActive(false);
        exitTab.SetActive(false);
    }

    public void OnCraftingTabClicked()
    {
        playerTab.SetActive(false);
        chestTab.SetActive(false);
        relationTab.SetActive(false);
        mapTab.SetActive(false);
        craftingTab.SetActive(true); //
        achievementTab.SetActive(false);
        settingsTab.SetActive(false);
        exitTab.SetActive(false);
    }

    public void OnAchievementTabClicked()
    {
        playerTab.SetActive(false);
        chestTab.SetActive(false);
        relationTab.SetActive(false);
        mapTab.SetActive(false);
        craftingTab.SetActive(false);
        achievementTab.SetActive(true); //
        settingsTab.SetActive(false);
        exitTab.SetActive(false);
    }

    public void OnSettingsTabClicked()
    {
        playerTab.SetActive(false);
        chestTab.SetActive(false);
        relationTab.SetActive(false);
        mapTab.SetActive(false);
        craftingTab.SetActive(false);
        achievementTab.SetActive(false);
        settingsTab.SetActive(true); //
        exitTab.SetActive(false);
    }

    public void OnExitTabClicked()
    {
        playerTab.SetActive(false);
        chestTab.SetActive(false);
        relationTab.SetActive(false);
        mapTab.SetActive(false);
        craftingTab.SetActive(false);
        achievementTab.SetActive(false);
        settingsTab.SetActive(false); 
        exitTab.SetActive(true); //
    }


}
