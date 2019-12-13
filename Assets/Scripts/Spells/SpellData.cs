using System;
using UnityEngine;



/// <summary>
/// Classe des sorts
/// </summary>
[Serializable]
public class SpellData : IUseable
{
    // Nom du sort
    [SerializeField]
    private string name = default;

    // D�g�ts du sort
    [SerializeField]
    private int damage = default;

    // Ic�ne du sort
    [SerializeField]
    private Sprite icon = default;

    // Vitesse du sort
    [SerializeField]
    private float speed = default;

    // Temps d'incantation du sort
    [SerializeField]
    private float castTime = default;

    // Prefab du sort
    [SerializeField]
    private GameObject prefab = default;

    // Couleur de la barre de sort
    [SerializeField]
    private Color barColor = default;


    /// <summary>
    /// Accesseurs sur les propri�t�s du sort
    /// </summary>

    // Propri�t� d'acc�s au nom du sort 
    public string MyName { get => name; }

    // Propri�t� d'acc�s aux d�g�ts du sort
    public int MyDamage { get => damage; }

    // Propri�t� d'acc�s � l'ic�ne du sort 
    public Sprite MyIcon { get => icon; }

    // Propri�t� d'acc�s � la vitesse du sort 
    public float MySpeed { get => speed; }

    // Propri�t� d'acc�s au temps d'incantation du sort
    public float MyCastTime { get => castTime; }

    // Propri�t� d'acc�s � la prefab du sort 
    public GameObject MyPrefab { get => prefab; }

    // Propri�t� d'acc�s � la couleur de la barre 
    public Color MyBarColor { get => barColor; }


    // Utilisation du sort
    public void Use()
    {
        Player.MyInstance.CastSpell(MyName);
    }
}