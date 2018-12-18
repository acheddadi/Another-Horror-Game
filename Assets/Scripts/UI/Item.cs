using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    [SerializeField] private bool canEquip;
    [SerializeField]private Sprite itemSprite;
    [TextArea(3, 10)]
    [SerializeField]private string[] description;
}
