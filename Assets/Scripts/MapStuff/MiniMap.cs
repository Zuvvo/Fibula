using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    private Texture2D minimap;
    private GameObject player;

    private int objectInPixels = 10;
    private int playerPositionX;
    private int playerPositionY;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        minimap = new Texture2D(200, 200);
        InvokeRepeating("DrawMap", 0.1f, 0.1f);
    }


    public void DrawMap()
    {
        playerPositionX = (int)player.transform.position.x;
        playerPositionY = (int)player.transform.position.y;

        for (int i = -10; i < 10; i++)
        {
            for (int j = -10; j < 10; j++)
            {
                SetPixelColors(i, j);
            }
        }
        minimap.Apply();
        this.GetComponent<RawImage>().texture = minimap;
    }

    private void SetPixelColors(int i, int j)
    {
        if (MapSystem.Map1Server[i + playerPositionX, j + playerPositionY].isBlocked)
        {
            if (MapSystem.Map1Server[i + playerPositionX, j + playerPositionY].EnemyOnTile != null)
            {
                //   Debug.Log("drawing enemy");
                SetXPixels(objectInPixels, (i + 10) * objectInPixels, (j + 10) * objectInPixels, Color.red);
            }
            else
            {
                SetXPixels(objectInPixels, (i + 10) * objectInPixels, (j + 10) * objectInPixels, Color.black);
            }
        }
        else
        {
            if(MapSystem.Map1Server[i + playerPositionX, j + playerPositionY].tileTypeId==1)
            {
                SetXPixels(objectInPixels, (i + 10) * objectInPixels, (j + 10) * objectInPixels, new Color(69, 44, 33, 255));
            }
            else
            {
                SetXPixels(objectInPixels, (i + 10) * objectInPixels, (j + 10) * objectInPixels, Color.green);
            }
        }
        if(MapSystem.Map1Server[i + playerPositionX, j + playerPositionY].isPlayerOnTile)
        {
            SetXPixels(objectInPixels, (i + 10) * objectInPixels, (j + 10) * objectInPixels, Color.blue);
        }
        
    }
    private void SetXPixels(int size, int positionX, int positionY, Color color)
    {
        for (int i = positionX; i < positionX + size; i++)
        {
            for (int j = positionY; j < positionY + size; j++)
            {
                minimap.SetPixel(i, j, color);
            }
        }
    }
    private void DrawPlayer(int positionX, int positionY)
    {
        for (int i = 0; i < objectInPixels; i++)
        {
            for (int j = 0; j < objectInPixels; j++)
            {
                minimap.SetPixel(positionX + i, positionY + j, Color.blue);
            }
        }
    }
}

