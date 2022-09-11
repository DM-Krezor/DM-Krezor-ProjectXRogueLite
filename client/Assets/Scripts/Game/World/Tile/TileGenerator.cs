using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.World.Tile
{
    public class TileGenerator : MonoBehaviour
    {
        [SerializeField]
        private int tileSize = 1;

        public TileController _tileController;
        TileController[] _tiles;
        private Dictionary<Vector3,TileController> _tilesDictionary = new Dictionary<Vector3, TileController>();

        private Color _tileColor;

        void Awake () {
            float tileCount = 0;
            while (tileCount < 5)
            {
                _tileColor = new Color(255f/255f,(255f - tileCount * 20f)/255f, (255f - tileCount * 20f)/255f);
                if (_tilesDictionary.Count == 0)
                { 
                    Vector3 firstPosition = new Vector3(0, 0, 0);
                    GenerateTile(firstPosition);
                }
                
                foreach (KeyValuePair<Vector3, TileController> tile in _tilesDictionary.ToList())
                {
                    Vector3 positionNewTileLeft;
                    positionNewTileLeft.x = tile.Key.x - tileSize;
                    positionNewTileLeft.y = tile.Key.y;
                    positionNewTileLeft.z = tile.Key.z;

                    Vector3 positionNewTileTop;
                    positionNewTileTop.x = tile.Key.x;
                    positionNewTileTop.y = tile.Key.y + tileSize;
                    positionNewTileTop.z = tile.Key.z;

                    Vector3 positionNewTileRight;
                    positionNewTileRight.x = tile.Key.x + tileSize;
                    positionNewTileRight.y = tile.Key.y;
                    positionNewTileRight.z = tile.Key.z;

                    Vector3 positionNewTileBottom;
                    positionNewTileBottom.x = tile.Key.x;
                    positionNewTileBottom.y = tile.Key.y - tileSize;
                    positionNewTileBottom.z = tile.Key.z;
                    
                    if (!_tilesDictionary.ContainsKey(positionNewTileLeft))
                    {
                        GenerateTile(positionNewTileLeft);
                    }
                    if (!_tilesDictionary.ContainsKey(positionNewTileTop))
                    {
                        GenerateTile(positionNewTileTop);
                    }
                    if (!_tilesDictionary.ContainsKey(positionNewTileRight))
                    {
                        GenerateTile(positionNewTileRight);
                    }
                    if (!_tilesDictionary.ContainsKey(positionNewTileBottom))
                    {
                        GenerateTile(positionNewTileBottom);
                    }
                }
                tileCount++;
            }
        }
	
        void GenerateTile(Vector3 positionNewTile)
        {
            TileController tile = Instantiate(_tileController, transform, false);
            _tilesDictionary.Add(positionNewTile, tile);
            tile.gameObject.GetComponent<SpriteRenderer>().color = _tileColor;
            tile.transform.localPosition = positionNewTile;
        }
    }
}
