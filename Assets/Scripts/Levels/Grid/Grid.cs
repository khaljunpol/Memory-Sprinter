using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform TileParent; 

    private AdjustGridLayoutCellSize _gridSizer;
    private GridConfig _gridConfig;
    private List<Tile> _tiles = new List<Tile>();


    public void InitializeGrid(GridConfig gridConfig)
    {
        TileParent = TileParent ?? this.transform;
        _gridConfig = gridConfig;
        _gridSizer = this.GetComponent<AdjustGridLayoutCellSize>();
        InstantiateLevelObjects();
    }

    private void InstantiateLevelObjects()
    {
        for(int col = 0; col < this._gridConfig.column; col++)
        {
            for(int row = 0; row < this._gridConfig.row; row++)
            {
                Tile tile = Instantiate(this._gridConfig.tilePrefab, TileParent);
                this._tiles.Add(tile);
            }
        }

        _gridSizer.UpdateSize(this._gridConfig.column, this._gridConfig.row);
    }
}