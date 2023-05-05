using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector2 = System.Numerics.Vector2;

public class TileMap : MonoBehaviour
{
    [SerializeField] private TextAsset textAsset;
    
    public GameObject tilePrefab;

    void Start()
    {
        var mapData = CreateMapFromJson(textAsset);
        CreateGround(mapData.map);
    }

    private void CreateGround(List<MapData.MapItem> squares)
    {
        foreach (var square in squares)
        {
         
            foreach (var tile in square.tiles)
            {
                
                var tileTexture = Resources.Load<Texture2D>("Textures/" + tile.id);
                if (tileTexture == null)
                {
                    Debug.LogError("Failed to load texture for tile ID: " + tile.id);
                    return;
                }
                
                var position = new Vector3(square.x, square.y * -1);
                var newTile = Instantiate(tilePrefab, position, Quaternion.identity);
                newTile.name = $"[{square.x},{square.y}] - {tile.type}:{tile.id}";
                var spriteRenderer = newTile.GetComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = tile.type == "ground" ? 0 : 1;
                var pivot = new Vector3(1f, 0f);
                spriteRenderer.sprite = Sprite.Create(tileTexture, new Rect(0, 0, tile.imageWidth, tile.imageHeight), pivot);
                newTile.transform.SetParent(transform);
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