using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemBar : MonoBehaviour
{

    private GameObject itemSlots;
    private List<GameObject> slots = new List<GameObject>();
    void Awake() {
        itemSlots = GameObject.Find("Item_slot");
        slots.Add(itemSlots.transform.GetChild(0).gameObject);
        slots.Add(itemSlots.transform.GetChild(1).gameObject);
        slots.Add(itemSlots.transform.GetChild(2).gameObject);



    }

    public void Updateslots(int slotNumber, Sprite newImage)
    {
        slots[slotNumber].GetComponent<Image>().sprite=newImage;
    }
}