using UnityEngine;
using UnityEngine.UI;
 
[ExecuteInEditMode]
[RequireComponent(typeof(GridLayoutGroup))]
public class AdjustGridLayoutCellSize : MonoBehaviour
{
    [SerializeField] public int columns;
    [SerializeField] public int rows;
    [SerializeField] public bool fitToParent;
    [SerializeField] public Vector2 cellSize;
 
    new RectTransform rectTransform;
    GridLayoutGroup gridLayout;
 
    void Awake()
    {
        rectTransform = (RectTransform)base.transform;
        gridLayout = GetComponent<GridLayoutGroup>();
    }
 
    // Start is called before the first frame update
    void Start()
    {
        UpdateCellSize();
    }

    public void UpdateSize(int column, int row)
    {
        columns = column;
        rows = row;
        UpdateCellSize();
    }
 
    void OnRectTransformDimensionsChange()
    {
        UpdateCellSize();
    }
 
#if UNITY_EDITOR
    [ExecuteAlways]
    void Update()
    {
        UpdateCellSize();
    }
#endif
 
    void OnValidate()
    {
        rectTransform = (RectTransform)base.transform;
        gridLayout = GetComponent<GridLayoutGroup>();
        UpdateCellSize();
    }
 
    void UpdateCellSize()
    {
        if (gridLayout == null || rectTransform == null)
        {
            Debug.LogError("FixedSizeGridLayout: GridLayoutGroup or RectTransform component is missing.");
            return;
        }

        if (fitToParent)
        {
            cellSize.x = (rectTransform.rect.width - (gridLayout.padding.left + gridLayout.padding.right) - (gridLayout.spacing.x * (columns - 1))) / columns;
        }

        cellSize.y = cellSize.x;
        rectTransform.sizeDelta = new Vector2(columns * cellSize.x + (gridLayout.spacing.x * (columns - 1)) + gridLayout.padding.left + gridLayout.padding.right, rows * cellSize.y + (gridLayout.spacing.y * (rows - 1)) + gridLayout.padding.top + gridLayout.padding.bottom);

        gridLayout.cellSize = cellSize;
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = columns;
    }
}
 