using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Sprite sprite;
    public float[,] Grid;
    int vertical, horizontal, cols, rows;
    float zoff = 0.0f;
    void Start()
    {
        vertical = (int)Camera.main.orthographicSize;
        horizontal = (int)(vertical * (float)Screen.width / Screen.height);
        cols = horizontal * 2;
        rows = vertical * 2;
        Grid = new float[cols, rows];
        for(int i =0; i < cols; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                Grid[i, j] = Random.Range(0.0f, 1.0f);
                SpawnTile(i, j, Grid[i, j]);
            }
        }
    }

    void SpawnTile(int x, int y, float value)
    {
        GameObject g = new GameObject("X: " + x + "Y: " + y);
        g.transform.position = new Vector3(x - (horizontal - 0.5f),y - (vertical - 0.5f),0);
        var s = g.AddComponent<SpriteRenderer>();
        s.color = new Color(value, value, value);
        s.sprite = sprite;
    }

    private void FixedUpdate()
    {
        float xoff = 0.0f;
        for(int i = 0; i < cols; i++)
        {
            float yoff = 0.0f;
            for(int j = 0; j < rows; j++)
            {
                GameObject h = GameObject.Find("X: " + i + "Y: " + j);
                var s = h.GetComponent<SpriteRenderer>();
                s.color = new Color(PerlinNoise3D(xoff, yoff, zoff), PerlinNoise3D(yoff, xoff, zoff), PerlinNoise3D(xoff + yoff, yoff - xoff, zoff));
                yoff += 0.01f;
                Debug.Log(s.color);
            }
            xoff += 0.01f;
        }
        zoff += 0.02f;

    }

    public static float PerlinNoise3D(float x, float y, float z)
    {
        float xy = Mathf.PerlinNoise(x, y);
        float xz = Mathf.PerlinNoise(x, z);
        float yz = Mathf.PerlinNoise(y, z);
        float yx = Mathf.PerlinNoise(y, x);
        float zx = Mathf.PerlinNoise(z, x);
        float zy = Mathf.PerlinNoise(z, y);

        return (xy + xz + yz + yx + zx + zy) / 6;
    }
}
