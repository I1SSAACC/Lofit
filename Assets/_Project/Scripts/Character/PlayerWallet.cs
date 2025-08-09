using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerWallet : NetworkBehaviour
{
    public PlayerHero Player;
    public int ruby;
    public int saphir;
    public int topaz;
    public Dictionary<OreType, int> Resources;
    

    public bool isShop;

    private void Update()
    {
        ruby = Resources[OreType.Ruby];
        saphir = Resources[OreType.Saphire];
        topaz = Resources[OreType.Topaz];
    }
    public int[] ResourcesToArray()
    {
        return new int[3] { Resources[OreType.Ruby], Resources[OreType.Saphire], Resources[OreType.Topaz] };
    }

    public void Awake()
    {
        Init();
    }
    public void Init()
    {
   
        Resources = new Dictionary<OreType, int>();
        Resources.Add(OreType.None, 0);
        Resources.Add(OreType.Ruby, 50);  
        Resources.Add(OreType.Saphire, 50);
        Resources.Add(OreType.Topaz, 50);
    }
    public void PickUpResource(OreType type,int amount)
    {
        // Debug.Log($"You pickup {amount} {type}");
        Resources[type] += amount;
    }
    /*
    public bool UseResources(List<Resources> resources)
    {
        foreach(Resources res in resources)
        {
            
            if (Resources[res.Resource] < res.Amount)
            {
                return false;
            }
        }
        foreach (Resources res in resources)
        {
            Resources[res.Resource] -= res.Amount;
        }
        return true;
        
    }
*/
    public void TryShop()
    {
        if (Input.GetKeyDown(KeyCode.E) && Player.canShop && !Player.UI.ShopMenu.activeSelf)
        {
            Player.UI.OpenShop();
        }
        else if(Input.GetKeyDown(KeyCode.E) && Player.UI.ShopMenu.activeSelf)
        {
            Player.UI.CloseShop();
        }

    }
}
