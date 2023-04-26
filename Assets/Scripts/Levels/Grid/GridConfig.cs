using UnityEngine;

[CreateAssetMenu(fileName = "GridConfig", menuName = "JaBum/Level/GridConfig", order = 0)]
public class GridConfig : ScriptableObject {
    public Tile tilePrefab;
    public TileStyle style;
    public int column;
    public int row;
}