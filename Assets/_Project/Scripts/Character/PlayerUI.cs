using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
  //  public UIBarScript HP, Stamina, Armor;
    public TextMeshProUGUI Ammo;
    public GameObject ShopMenu;
    public GameObject Weapon;
    public PlayerWallet Wallet;
    public Animator Animator;
  // public Shop Shop;
    public GameObject ui;
    public void OpenShop()
    {
        Weapon.SetActive(false);
        Animator.enabled = false;
        Wallet.isShop = true;
        ShopMenu.SetActive(true);
      //  Shop.CloseCost();
     //   Shop.UpdateResources(Wallet.ResourcesToArray());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void CloseShop()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Wallet.isShop = false;
        ShopMenu.SetActive(false);  
        Animator.enabled = true;
        Weapon.SetActive(true);
    }

}
