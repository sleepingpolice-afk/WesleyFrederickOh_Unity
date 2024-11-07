using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;

    void Awake()
    {
        //weapon = Instantiate(weaponHolder);
        if(weaponHolder != null)
        {
            weapon = Instantiate(weaponHolder, transform.position, Quaternion.identity);
            weapon.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Weapon currentWeapon = other.gameObject.GetComponentInChildren<Weapon>();

            if (currentWeapon != null)
            {
                currentWeapon.gameObject.SetActive(false);
            }

            weapon.transform.SetParent(other.gameObject.transform);
            weapon.transform.localPosition = Vector2.zero;
            TurnVisual(true, weapon);

            weapon.gameObject.SetActive(true);
        }
    }

    void TurnVisual(bool on)
    {
        weapon.GetComponentInChildren<SpriteRenderer>().enabled = on;
        weapon.GetComponentInChildren<Weapon>().enabled = on;
        weapon.GetComponentInChildren<Animator>().enabled = on;
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        weapon.GetComponentInChildren<SpriteRenderer>().enabled = on;
        weapon.GetComponentInChildren<Weapon>().enabled = on;
        weapon.GetComponentInChildren<Animator>().enabled = on;
    }
}
