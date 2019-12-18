﻿using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Classe de gestion de l'inventaire
/// </summary>
public class InventoryScript : MonoBehaviour
{
    // Instance de classe (singleton)
    private static InventoryScript instance;

    // Propriété d'accès à l'instance
    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                // Retourne l'object de type Inventory (doit être unique)
                instance = FindObjectOfType<InventoryScript>();
            }

            return instance;
        }
    }


    // Liste des sacs de l'inventaire
    private List<Bag> bags = new List<Bag>();

    // Tabelau des boutons des sacs
    [SerializeField]
    private BagButton[] bagButtons;

    // [DEBUG] : Tableau des items de l'inventaire
    [SerializeField]
    private Item[] items;

    // Propriété d'ajout des sacs
    public bool CanAddBag { get => bags.Count < bagButtons.Length; }



    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        // Création d'un sac
        Bag bag = (Bag)Instantiate(items[0]);

        // Initialisation du sac
        bag.Initialize(20);

        // Ajoute un sac à l'inventaire
        bag.Use();
    }


    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        // [DEBUG] : Ajoute un sac
        if (Input.GetKeyDown(KeyCode.J))
        {
            // Création d'un sac
            Bag bag = (Bag)Instantiate(items[0]);

            // Initialisation du sac
            bag.Initialize(20);

            // Ajoute un sac à l'inventaire
            bag.Use();
        }
    }


    /// <summary>
    /// Ajoute un sac à l'inventaire
    /// </summary>
    public void AddBag(Bag bag)
    {
        foreach (BagButton bagButton in bagButtons)
        {
            // S'il n'y a pas de sacs sur le bouton
            if (bagButton.MyBag == null)
            {
                // Assignation du sac
                bagButton.MyBag = bag;

                // Ajoute le sac dans la liste
                bags.Add(bag);

                break;
            }
        }
    }
}