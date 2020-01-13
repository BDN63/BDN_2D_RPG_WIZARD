﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Classe de gestion des boutons de la feuille du personnage
/// </summary>
public class CharacterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Item d'équipement
    private Armor equippedArmor;

    // Type d'équipement
    [SerializeField]
    private ArmorType armorType = default;

    // Image de l'item
    [SerializeField]
    private Image icon = default;


    /// <summary>
    /// Entrée du curseur
    /// </summary>
    /// <param name="eventData">Evenement d'entrée</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Si l'emplacement n'est pas vide
        if (equippedArmor != null)
        {
            // Affiche le tooltip
            UIManager.MyInstance.ShowTooltip(new Vector2(0, 0), transform.position, equippedArmor);
        }

    }

    /// <summary>
    /// Sortie du curseur
    /// </summary>
    /// <param name="eventData">Evenement de sortie</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // Masque le tooltip
        UIManager.MyInstance.HideTooltip();
    }

    /// <summary>
    /// Gestion du clic
    /// </summary>
    /// <param name="eventData">Evenement de clic</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // Clic gauche
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // // Si un item Armor est en train d'être déplacé
            if (Hand.MyInstance.MyMoveable is Armor)
            {
                // Item Armor
                Armor stuff = Hand.MyInstance.MyMoveable as Armor;

                if (stuff.MyArmorType == armorType)
                {
                    // Equipement de l'item
                    EquipArmor(stuff);
                }

                // Actualise le tooltip
                UIManager.MyInstance.RefreshTooltip(stuff);
            }
            // Si rien n'est en train d'être déplacé et qu'il y a un clic sur un emplacement qui n'est pas vide
            else if (Hand.MyInstance.MyMoveable == null && equippedArmor != null)
            {
                // Récupération de l'item
                Hand.MyInstance.TakeMoveable(equippedArmor);

                // Référence sur le bouton
                CharacterPanel.MyInstance.MySelectedButton = this;

                // Grise l'image de l'item
                icon.color = Color.grey; 
            }
        }
    }

    /// <summary>
    /// Equipe un item
    /// </summary>
    /// <param name="armor"></param>
    public void EquipArmor(Armor armor)
    {
        // Retire l'item de l'inventaire
        armor.Remove();

        // Si l'emplacement n'est pas vide
        if (equippedArmor != null)
        {
            // Si l'item à équiper est différent de l'item équipé
            if (equippedArmor != armor)
            {
                // Ajoute l'item équipé dans l'emplacement de l'item à equiper
                armor.MySlot.AddItem(equippedArmor);
            }

            // Actualise le tooltip
            UIManager.MyInstance.RefreshTooltip(equippedArmor);
        }
        else
        {
            // Masque le tooltip
            UIManager.MyInstance.HideTooltip();
        }

        // Actualise la couleur de l'image de l'item
        icon.color = Color.white;

        // Actualise l'image de l'item
        icon.sprite = armor.MyIcon;

        // Nouvelle couleur
        Color buttonColor = Color.clear;

        // La couleur varie en fonction de la qualité de l'item
        ColorUtility.TryParseHtmlString(QualityColor.MyColors[armor.MyQuality], out buttonColor);

        // Actualise la couleur de l'image de l'emplacement
        GetComponent<Image>().color = buttonColor;

        // Active l'image de l'item
        icon.enabled = true;

        // Référence sur l'item
        equippedArmor = armor;

        // S'il on déplace un item Armor
        if (Hand.MyInstance.MyMoveable == (armor as IMoveable))
        {
            // Libère l'item
            Hand.MyInstance.Drop();
        }
    }

    /// <summary>
    /// Déséquipe un item
    /// </summary>
    /// <param name="armor"></param>
    public void UnequipArmor()
    {
        // Actualise la couleur de l'image de l'item
        icon.color = Color.white;

        // Actualise l'image de l'item
        icon.sprite = null;

        // Actualise la couleur de l'image de l'emplacement
        GetComponent<Image>().color = Color.white;

        // Active l'image de l'item
        icon.enabled = false;

        // Référence sur l'item
        equippedArmor = null;
    }
}