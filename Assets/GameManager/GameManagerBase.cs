using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class GameManagerBase : MonoBehaviour
{
    protected GameManagerBase gameManagerBase;

    protected GameObject[] tiles;
    protected TileInitializer[] tileInitializer;
    protected TilePathFinder[] tilePathFinder;

    protected void setDataInBase()
    {
        gameManagerBase = GetComponent<GameManagerBase>();

        setTiles();
        setTileInitializer();
        setTilePathFinder();
    }

    protected void getDataFromBase()
    {
        gameManagerBase = GetComponent<GameManagerBase>();

        tiles = gameManagerBase.getTiles();
        tileInitializer = gameManagerBase.getTileInitializer();
        tilePathFinder = gameManagerBase.getTilePathFinder();
    }

    protected void setTiles() { gameManagerBase.tiles = tiles; }
    protected void setTileInitializer() { gameManagerBase.tileInitializer = tileInitializer; }
    protected void setTilePathFinder() { gameManagerBase.tilePathFinder = tilePathFinder; }

    protected GameObject[] getTiles() { return tiles; }
    protected TileInitializer[] getTileInitializer() { return tileInitializer; }
    protected TilePathFinder[] getTilePathFinder() { return tilePathFinder; }
}
