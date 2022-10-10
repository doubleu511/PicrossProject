using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHub : Singleton<SpriteHub>
{
    public Sprite sprite_box = null;
    public Sprite sprite_boxX = null;
    public Sprite sprite_boxFill = null;

    public Sprite[] puzzleSprites = null;
    public Sprite puzzleSpriteUnknown = null;

    private SpriteHub[] spriteHubs;

    private void Awake()
    {
        spriteHubs = FindObjectsOfType<SpriteHub>();

        if (spriteHubs.Length >= 2)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
