using System.Collections.Generic;

[System.Serializable]
public class MapData
{
    public Map map;

    [System.Serializable]
    public class Map
    {
        public string version;
        public int identifier;
        public Data data;

        [System.Serializable]
        public class Data
        {
            public int type;
            public int version;
            public int mapWidth;
            public int mapHeight;
            public int itemsMajorVersion;
            public int itemsMinorVersion;
            public List<Node> nodes;

            [System.Serializable]
            public class Node
            {
                public int type;
                public string description;
                public string spawnfile;
                public string housefile;
                public List<Feature> features;

                [System.Serializable]
                public class Feature
                {
                    public int type;
                    public int x;
                    public int y;
                    public int z;
                    public List<Tile> tiles;

                    [System.Serializable]
                    public class Tile
                    {
                        public int type;
                        public int x;
                        public int y;
                        public int tileid;
                        public List<Item> items;

                        [System.Serializable]
                        public class Item
                        {
                            public int type;
                            public int id;
                        }
                    }
                }
            }
        }
    }
}