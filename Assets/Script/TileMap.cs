using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMap : MonoBehaviour
{
    [SerializeField] private TextAsset textAsset;
    
    public GameObject tilePrefab;

    void Start()
    {
        var mapData = CreateMapFromJson(textAsset);
        CreateGround(mapData.map);
    }

    private void CreateGround(List<MapData.MapItem> sqms)
    {
        foreach (var sqm in sqms)
        {
         
            foreach (var tile in sqm.tiles)
            {
                
                var tileTexture = Resources.Load<Texture2D>("Textures/" + tile.id);
                if (tileTexture == null)
                {
                    Debug.LogError("Failed to load texture for tile ID: " + tile.id);
                    return;
                }
                var position = new Vector3(sqm.x, sqm.y * -1) ;
                var newTile = Instantiate(tilePrefab,  position, Quaternion.identity);
                newTile.name = "[" + sqm.x + "," + sqm.y + "] - ground:" + tile.id;
                var spriteRenderer = newTile.GetComponent<SpriteRenderer>();
                var layer = 1;
                if (tile.type == "ground")
                {
                    layer = 0;
                }
                spriteRenderer.sortingOrder = layer;
            
                //Change the texture of the SpriteRenderer to a new texture
                spriteRenderer.sprite = Sprite.Create(tileTexture, new Rect(0, 0, tile.imageWidth, tile.imageHeight), Vector2.one/2);
                newTile.transform.parent = transform;
            }
        }
    }

    private static MapData CreateMapFromJson(TextAsset textAsset)
    {
        var jsonString = textAsset.text;
        var map = JsonUtility.FromJson<MapData>(jsonString);
        return map;
    }
}