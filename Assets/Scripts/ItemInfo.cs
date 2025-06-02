using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Text itemName;
    public Text itemDesc;

    public void SetUp(string name, string desc)
    {
        itemName.text = name;
        itemDesc.text = desc;
    }
}
