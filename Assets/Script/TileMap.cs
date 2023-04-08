using System;
using System.Linq;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    [SerializeField] private TextAsset textAsset;
    [SerializeField] private Vector2 groundPivot = new(0f, 0f);
    [SerializeField] private Vector2 itemPivot = new(0f, 0f);

    // Define the prefab for each tile
    public GameObject tilePrefab;
    
    private const int InvertValue = -1;
    private const float TileSize = 32f;
    

    // Start is called before the first frame update
    void Start()
    {
        var mapData = CreateMapFromJson(textAsset);
        // Loop through each tile position and create a new tile game object
        foreach (var dataNode in mapData.map.data.nodes)
        {
            foreach (var feature in dataNode.features)
            {
                CreateGround(feature);
                CreateItems(feature);
            }
        }
    }

    private void CreateGround(MapData.Map.Data.Node.Feature feature)
    {
        foreach (var tile in feature.tiles)
        {
            var newTexture = Resources.Load<Texture2D>("Textures/" + tile.tileid);
            
            var finalX = feature.x + tile.x;
            var finalY = (feature.y + tile.y) * InvertValue;
            var finalZ = feature.z;
            var mapLayer = feature.z;
            var layer = mapLayer + tile.x + tile.y;

            //Adds offset for textures with width greater than 32 
            var tileOffSet = Vector3.zero;
            if (newTexture.width > TileSize)
            {
                var xOffset = (newTexture.width - TileSize) / TileSize;
                tileOffSet = new Vector3(-xOffset, 0, 0);
            }
            
            //Instantiate the tile prefab
            var position = new Vector3(finalX, finalY, finalZ) + tileOffSet;
            var newTile = Instantiate(tilePrefab,  position, Quaternion.identity);
            newTile.name = "[" + tile.x + "," + tile.y + "," + tile.y + "] - ground:" + tile.tileid;
            //Get the SpriteRenderer component of the new tile GameObject
            var spriteRenderer = newTile.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = layer;
            
            //Change the texture of the SpriteRenderer to a new texture
            spriteRenderer.sprite = Sprite.Create(newTexture,
                new Rect(0, 0, newTexture.width, newTexture.height), groundPivot);

            // Set the tile game object as a child of the TileMap game object
            var layerObject = getOrCreateLayerObject(mapLayer.ToString());
            newTile.transform.parent = layerObject;
        }
    }

    private void CreateItems(MapData.Map.Data.Node.Feature feature)
    {
        foreach (var tile in feature.tiles)
        {
            foreach (var item in tile.items.Select((value, index) => new { value, index }))
            {
                var newItemTexture = Resources.Load<Texture2D>("Textures/" + item.value.id);

                var itemX = feature.x + tile.x;
                var itemY = (feature.y + tile.y) * InvertValue;
                var itemZ = feature.z;
                var mapLayer = feature.z;
                var layer = mapLayer + tile.x + tile.y + item.index;

                //Adds offset for textures with width greater than 32 
                var tileOffSet = Vector3.zero;
                if (newItemTexture.width > TileSize)
                {
                    var xOffset = (newItemTexture.width - TileSize) / TileSize;
                    tileOffSet = new Vector3(-xOffset, 0, 0);
                }
                
                //Instantiate the item prefab
                var position = new Vector3(itemX, itemY, -itemZ) + tileOffSet;
                var newItem = Instantiate(tilePrefab, position, Quaternion.identity);
                newItem.name = "[" + tile.x + "," + tile.y + "," + tile.y + "] - item:" + item.value.id;

                //Get the SpriteRenderer component of the new item GameObject
                var itemSpriteRenderer = newItem.GetComponent<SpriteRenderer>();
                itemSpriteRenderer.sortingOrder = layer;
                
                //Change the texture of the SpriteRenderer to a new texture
                itemSpriteRenderer.sprite = Sprite.Create(newItemTexture,
                    new Rect(0, 0, newItemTexture.width, newItemTexture.height), itemPivot);

                // Set the item game object as a child of the TileMap game object
                var layerObject = getOrCreateLayerObject(mapLayer.ToString());
                newItem.transform.parent = layerObject;
            }
        }
    }

    private Transform getOrCreateLayerObject(string layerName)
    {
    
        var childTransform = transform.Find(layerName);
        
        if (childTransform != null)
        {
            return childTransform;
        }

        var emptyObject = new GameObject
        {
            name = layerName,
            transform = { parent = transform}
        };
        return emptyObject.transform;
    }


    private static MapData CreateMapFromJson(TextAsset textAsset)
    {
        var jsonString = textAsset.text;
        var map = JsonUtility.FromJson<MapData>(jsonString);
        return map;
    }
}