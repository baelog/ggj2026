
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

    public GameObject objectPrefab;

    public float rotationStep = 60f;

    private GameObject previewInstance;
    private Camera mainCamera;
    private float currentYRotation = 0f;

    void Start()
    {
        gold = 100;
        InitGrid();
        overlayTilemap.ClearAllTiles();
        Instance = this;
        mainCamera = Camera.main;
    }

    void Update()
    {
        goldDisplay.text = "Gold: " + gold.ToString();
        maskDisplay.text = "Mask: " + mask.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearOverlay();
            buildingToPlace = null;
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
            HandlePreview();
            HandleRotation();
        }
    }

    void StopHandlePreview()
    {
        previewInstance.SetActive(false);
        Destroy(previewInstance);
    }

    void HandlePreview()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (!previewInstance.activeInHierarchy)
                previewInstance.SetActive(true);

            previewInstance.transform.position = hit.point;
            previewInstance.transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        }
    }

    void HandleRotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentYRotation += rotationStep;
        }
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
                Instantiate(factory, buldingTilemap.CellToWorld(cellPos), Quaternion.identity);
                gold -= buildingToPlace.cost;
                StopHandlePreview();
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
            previewInstance = Instantiate(objectPrefab);
            previewInstance.SetActive(false);
        }
    }

    public void IncrementMask(int numberAdd)
    {
        mask += numberAdd;
    }
}