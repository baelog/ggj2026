
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public int gold;
    //public int mask;
    public int stoneMask;
    public int ironMask;
    public int prismarineMask;
    public int lightPrismarineMask;
    public int solidifiedPrismarineMask;
    private bool build = false;
    public TMP_Text goldDisplay;
    //public TMP_Text maskDisplay;
    public TMP_Text stoneMaskDisplay;
    public TMP_Text ironMaskDisplay;
    public TMP_Text prismarineMaskDisplay;
    public TMP_Text lightPrismarineMaskDisplay;
    public TMP_Text solidifiedPrismarineMaskDisplay;

    private Building buildingToPlace;
    private Tile builddingTile;

    public Tilemap groundTilemap;
    public Tilemap buldingTilemap;
    public Tilemap overlayTilemap;

    public TileBase freeTile;
    public TileBase occupiedTile;

    public GameObject factory;

    private Dictionary<Vector3Int, CellData> cells = new Dictionary<Vector3Int, CellData>();

    private Vector3Int lastHoveredCell;
    private bool hasLastCell = false;

    public static GameManager Instance;

    public GameObject previewInstance;
    private Camera mainCamera;
    public int rotation;
    private bool deleteTile;

    public GameObject targetUIElement;
    public EventSystem eventSystem;
    public GraphicRaycaster graphicRaycaster;

    void Start()
    {
        gold = 1000;
        rotation = 0;
        InitGrid();
        overlayTilemap.ClearAllTiles();
        Instance = this;
        mainCamera = Camera.main;
        previewInstance.SetActive(false);
    }

    void Update()
    {
        goldDisplay.text = "Gold: " + gold.ToString();
        //maskDisplay.text = "Mask: " + mask.ToString();

        stoneMaskDisplay.text = "Stone Mask: " + stoneMask.ToString();
        ironMaskDisplay.text = "Iron Mask: " + ironMask.ToString();
        prismarineMaskDisplay.text = "Prismarine Mask: " + prismarineMask.ToString();
        lightPrismarineMaskDisplay.text = "Light Prismarine Mask: " + lightPrismarineMask.ToString();
        solidifiedPrismarineMaskDisplay.text = "solidified Prismarine Mask: " + solidifiedPrismarineMask.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopPreview();
            deleteTile = false;
        }
        if (buildingToPlace != null)
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement(targetUIElement) && build == false)
            {
                build = true;
            }
            UpdateHover(worldPos);
        }
        if (deleteTile == true)
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;

            UpdateHoverDelete(worldPos);
        }
    }

    void StopPreview()
    {
        ClearOverlay();
        buildingToPlace = null;
        previewInstance.SetActive(false);
        SetMenuActive.Instance.hideElement();
    }

    void InitGrid()
    {
        BoundsInt bounds = groundTilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (groundTilemap.GetTile(pos) == null) continue;

            cells[pos] = new CellData
            {
                isOccupied = GetTypeTerrain(groundTilemap.GetTile(pos).name)
            };
        }
    }

    private int GetTypeTerrain(string nameCell)
    {
        switch (nameCell)
        {
            case "HexagonGreen":
                return 0;
            case "HexagonBrown":
                return 1;
            case "HexagonBlue":
                return 2;
        }
        return -1;
    }

    public void UpdateHover(Vector3 worldPos)
    {
        Vector3Int cellPos = groundTilemap.WorldToCell(worldPos);


        if (build)
        {
            if ((cells[cellPos].isOccupied == buildingToPlace.placeTerrain || buildingToPlace.placeTerrain == 4) && gold >= buildingToPlace.cost)
            {
                builddingTile = ScriptableObject.CreateInstance<Tile>();
                var builddingTileTemp = buildingToPlace.GetComponent<SpriteRenderer>();
                builddingTile.sprite = builddingTileTemp.sprite;
                builddingTile.color = builddingTileTemp.color;
                cells[cellPos].isOccupied = buildingToPlace.type;
                buldingTilemap.SetTile(cellPos, builddingTile);
                buildingToPlace.createBuilding(buldingTilemap.CellToWorld(cellPos));
                gold -= buildingToPlace.cost;
            }
            build = false;
        } 
        else 
        {
            if (!cells.ContainsKey(cellPos))
            {
                ClearOverlay();
                return;
            }

            if (hasLastCell && cellPos == lastHoveredCell) return;

            ClearOverlay();

            if (buildingToPlace.placeTerrain != 4)
                overlayTilemap.SetTile(
                    cellPos,
                    cells[cellPos].isOccupied != buildingToPlace.placeTerrain ? occupiedTile : freeTile
                );
            else
                overlayTilemap.SetTile(cellPos,freeTile);

            lastHoveredCell = cellPos;
            hasLastCell = true;
        }
    }

    public void UpdateHoverDelete(Vector3 worldPos)
    {
        Vector3Int cellPos = groundTilemap.WorldToCell(worldPos);

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement(targetUIElement) && cells[cellPos].isOccupied >= 10)
        {
            Collider2D hit = Physics2D.OverlapPoint(worldPos);

            cells[cellPos].isOccupied -= 10;
            buldingTilemap.SetTile(cellPos, null);

            if (hit != null && (hit.CompareTag("Building") || hit.CompareTag("Belt")))
            {
                Destroy(hit.gameObject);
            }
        }

        if (!cells.ContainsKey(cellPos))
        {
            ClearOverlay();
            return;
        }

        if (hasLastCell && cellPos == lastHoveredCell)
        {
            return;
        }

        ClearOverlay();

        overlayTilemap.SetTile(cellPos, occupiedTile);

        lastHoveredCell = cellPos;
        hasLastCell = true;
    }

    public void ClearOverlay()
    {
        overlayTilemap.ClearAllTiles();
        hasLastCell = false;
    }

    public void BuyBulding(Building building)
    {
        if (gold >= building.cost)
        {
            buildingToPlace = building;
            previewInstance.SetActive(true);
            deleteTile = false;
        }
    }
    public void IncrementStoneMask(int numberAdd)
    {
        stoneMask += numberAdd;
    }
    public void IncrementIronMask(int numberAdd)
    {
        ironMask += numberAdd;
    }
    public void IncrementPrismarineMask(int numberAdd)
    {
        prismarineMask += numberAdd;
    }
    public void IncrementLightPrismarineMask(int numberAdd)
    {
        lightPrismarineMask += numberAdd;
    }
    public void IncrementSolidifiedPrismarineMask(int numberAdd)
    {
        solidifiedPrismarineMask += numberAdd;
    }
    public void IncrementGold(int numberAdd)
    {
        gold += numberAdd;
    }
    public void Rotate()
    {
        rotation++;
    }

    public void Delete()
    {
        buildingToPlace = null;
        deleteTile = true;
        previewInstance.SetActive(false);
    }

    bool IsPointerOverUIElement(GameObject target)
    {
        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = Input.mousePosition;

        var results = new System.Collections.Generic.List<RaycastResult>();
        graphicRaycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == target)
            {
                return true;
            }
        }

        return false;
    }
}