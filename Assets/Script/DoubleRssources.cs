using UnityEngine;
using System.Collections;

public class DoubleRssources : MonoBehaviour
{
    public GameObject obj;
    public GameObject iron;
    public GameObject prisma;
    public GameObject lightprisma;
    public GameObject strongPrisma;
    private GameObject[] buildings;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void test()
    {
    }

    public void findBuildingsWithTag()
    {
        buildings = GameObject.FindGameObjectsWithTag(obj.tag);
        Forge forge = obj.GetComponent<Forge>();
        forge.numberRessource++;

        foreach (GameObject building in buildings)
        {
            Forge itemBehaviour = building.GetComponent<Forge>();
            if (itemBehaviour != null)
            {
                itemBehaviour.numberRessource++;
            }
        }
    }

    public void oneIron()
    {
        buildings = GameObject.FindGameObjectsWithTag(iron.tag);
        ForgeIronStone forge = iron.GetComponent<ForgeIronStone>();
        forge.numberRessource++;

        foreach (GameObject building in buildings)
        {
            ForgeIronStone itemBehaviour = building.GetComponent<ForgeIronStone>();
            if (itemBehaviour != null)
            {
                itemBehaviour.numberRessource++;
            }
        }
    }

    public void onePrisma()
    {
        buildings = GameObject.FindGameObjectsWithTag(prisma.tag);
        ForgeIronPrimarine forge = prisma.GetComponent<ForgeIronPrimarine>();
        forge.numberRessource++;

        foreach (GameObject building in buildings)
        {
            ForgeIronPrimarine itemBehaviour = building.GetComponent<ForgeIronPrimarine>();
            if (itemBehaviour != null)
            {
                itemBehaviour.numberRessource++;
            }
        }
    }
    public void oneLightPrisma()
    {
        buildings = GameObject.FindGameObjectsWithTag(lightprisma.tag);
        ForgeStonePrismarine forge = lightprisma.GetComponent<ForgeStonePrismarine>();
        forge.numberRessource++;

        foreach (GameObject building in buildings)
        {
            ForgeStonePrismarine itemBehaviour = building.GetComponent<ForgeStonePrismarine>();
            if (itemBehaviour != null)
            {
                itemBehaviour.numberRessource++;
            }
        }
    }
    public void oneStrongPrisma()
    {
        buildings = GameObject.FindGameObjectsWithTag(strongPrisma.tag);
        ForgeStoneIronPrismarine forge = strongPrisma.GetComponent<ForgeStoneIronPrismarine>();
        forge.numberRessource++;

        foreach (GameObject building in buildings)
        {
            ForgeStoneIronPrismarine itemBehaviour = building.GetComponent<ForgeStoneIronPrismarine>();
            if (itemBehaviour != null)
            {
                itemBehaviour.numberRessource++;
            }
        }
    }
}
