using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
[System.Serializable]
public class EquipmentData 
{
    [SerializeField] private string name;
    [SerializeField] private int id;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public string Name { get => name; set => name = value; }

}
