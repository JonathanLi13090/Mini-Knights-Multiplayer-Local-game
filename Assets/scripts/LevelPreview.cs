using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPreview : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite dungeonPreview;
    public Sprite castlePreview;
    public Sprite swampPreview;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSprite(int levelNum)
    {
        if(levelNum == 1) { spriteRenderer.sprite = castlePreview; }
        else if(levelNum == 2) { spriteRenderer.sprite = dungeonPreview; }
        else if(levelNum == 3) { spriteRenderer.sprite = swampPreview; }
    }
}
