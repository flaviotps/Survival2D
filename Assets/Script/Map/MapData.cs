using System.Collections.Generic;

[System.Serializable]
public class MapData
{
    public int width;
    public int height;
    public List<MapItem> map;

    [System.Serializable]
    public class MapItem
    {
        public int x;
        public int y;
        public List<Tile> tiles;

        [System.Serializable]
        public class Tile
        {
            public int id;
            public string type;
            public float imageWidth;
            public float imageHeight;
        }
    }
}