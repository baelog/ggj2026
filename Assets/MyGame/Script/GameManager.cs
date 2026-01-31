
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

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
    }

    void StopPreview()
    {
        ClearOverlay();
        buildingToPlace = null;
        previewInstance.SetActive(false);
    }

    void InitGrid()
    {
        BoundsInt bounds = groundTilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (groundTilemap.GetTile(pos) == null) continue;

            cells[pos] = new CellData
            {
                isOccupied = groundTilemap.GetTile(pos).name == "HexagonGreen" ? 0 : 2
            };
        }
    }

    public void UpdateHover(Vector3 worldPos)
    {
        Vector3Int cellPos = groundTilemap.WorldToCell(worldPos);

        if (build)
        {
            if (cells[cellPos].isOccupied == 0 && gold >= buildingToPlace.cost)
            {
                builddingTile = ScriptableObject.CreateInstance<Tile>();
                var builddingTileTemp = buildingToPlace.GetComponent<SpriteRenderer>();
                builddingTile.sprite = builddingTileTemp.sprite;
                builddingTile.color = builddingTileTemp.color;
                cells[cellPos].isOccupied = 1;
                buldingTilemap.SetTile(cellPos, builddingTile);
                GameObject newfactory = Instantiate(factory, buldingTilemap.CellToWorld(cellPos), Quaternion.identity);
                newfactory.transform.Find("Out").Rotate(0, 0, 60 * rotation);
                gold -= buildingToPlace.cost;
                //StopPreview();
                //return;
            }
            build = false;
        }

        if (!cells.ContainsKey(cellPos))
        {
            ClearOverlay();
            return;
        }

        if (hasLastCell && cellPos == lastHoveredCell) return;

        ClearOverlay();

        overlayTilemap.SetTile(
            cellPos,
            cells[cellPos].isOccupied != 0 ? occupiedTile : freeTile
        );

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
    public void Rotate()
    {
        rotation++;
    }
}