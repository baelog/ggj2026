
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public int gold;
    public int mask;
    private bool build = false;
    public TMP_Text goldDisplay;
    public TMP_Text maskDisplay;

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

    void Start()
    {
        gold = 100;
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
        maskDisplay.text = "Mask: " + mask.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopPreview();
            deleteTile = false;
        }
        if (buildingToPlace != null)
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;

            if (Input.GetMouseButtonDown(0) && build == false)
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
            if (cells[cellPos].isOccupied == buildingToPlace.placeTerrain && gold >= buildingToPlace.cost)
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

            overlayTilemap.SetTile(
                cellPos,
                cells[cellPos].isOccupied != buildingToPlace.placeTerrain ? occupiedTile : freeTile
            );

            lastHoveredCell = cellPos;
            hasLastCell = true;
        }
    }

    public void UpdateHoverDelete(Vector3 worldPos)
    {
        Vector3Int cellPos = groundTilemap.WorldToCell(worldPos);

        if (!cells.ContainsKey(cellPos))
        {
            ClearOverlay();
            return;
        }

        if (hasLastCell && cellPos == lastHoveredCell) return;

        ClearOverlay();

        if (Input.GetMouseButtonDown(0) && cells[cellPos].isOccupied <= 10)
        {
            Debug.Log(cells[cellPos].isOccupied);
            cells[cellPos].isOccupied -= 10;
            buldingTilemap.SetTile(cellPos, null);
            Debug.Log(buldingTilemap.ToString());
        }
        else
        {
            overlayTilemap.SetTile(cellPos, occupiedTile);
        }

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
        }
    }

    public void IncrementMask(int numberAdd)
    {
        mask += numberAdd;
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
}