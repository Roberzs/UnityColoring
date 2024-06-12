using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject BlockPrefab, CBlockPrefab, RBlockPrefab;
    public Texture2D DataTexture2D;
    // Start is called before the first frame update
    private SpriteRenderer[,] Sprites;

    public Sprite blockSp, block_errSp;

    void Start()
    {
        int cell = 15;

        Sprites = new SpriteRenderer[cell, cell];

        var startPos = -new Vector3(2 * cell / 2 - 2, 2 * cell / 2);
        for (int i = 0; i < cell; i++)
        {
            for (int j = 0; j < cell; j++)
            {
                var item = Instantiate(BlockPrefab);
                item.transform.localPosition = startPos + new Vector3(i * 2, j * 2);
                var sp = item.GetComponent<SpriteRenderer>();
                Sprites[i,j] = sp;
            }
        }

        bool[,] datas = new bool[cell, cell];
        int width = DataTexture2D.width, height = DataTexture2D.height;
        // 读取 上色
        for (int i = 0; i < cell; i++)
        {
            for (int j = 0; j < cell; j++)
            {
                Color color = DataTexture2D.GetPixel(i, j);
                if (color.a < 0.3f)
                {
                    Sprites[i, j].sprite = block_errSp;
                    //Sprites[i, j].color = Color.red;
                }
                else
                {
                    datas[i, j] = true;
                    Sprites[i, j].sprite = blockSp;
                    color.a = 1;
                    Sprites[i, j].color = color;
                }
                
            }
        }

        int cnt = 0;
        for (int i = 0; i < cell;i++)
        {
            List<int> lst = new List<int>();
            for (int j = 0; j < cell;j++) 
            {
                if (datas[i, j] == true)
                {
                    cnt++;
                }
                else
                {
                    if (cnt > 0)
                    {
                        lst.Add(cnt);
                    }
                    cnt = 0;
                }
            }
            if (cnt > 0)
            {
                lst.Add(cnt);
            }
            cnt = 0;
            lst.Reverse();
            var item = Instantiate(CBlockPrefab);
            item.transform.localPosition = startPos + new Vector3(i * 2, cell * 2 + 3);
            var str = string.Join(" ", lst);
            item.GetComponentInChildren<TMP_Text>().text = str;
        }

        for (int j = 0; j < cell; j++)
        {
            List<int> lst = new List<int>();
            for (int i = 0; i < cell; i++)
            {
                if (datas[i, j] == true)
                {
                    cnt++;
                }
                else
                {
                    if (cnt > 0)
                    {
                        lst.Add(cnt);
                    }
                    cnt = 0;
                }
            }
            if (cnt > 0)
            {
                lst.Add(cnt);
            }
            cnt = 0;
            var item = Instantiate(RBlockPrefab);
            item.transform.localPosition = startPos + new Vector3(-5, j * 2);
            var str = string.Join(" ", lst);
            item.GetComponentInChildren<TMP_Text>().text = str;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            for (int i = 0; i < Sprites.GetLength(0);i++)
            {
                for (int j = 0; j < Sprites.GetLength(1); j++)
                {
                    Sprites[i, j].sprite = blockSp;
                    Sprites[i, j].color = new Color(212f / 255, 148f / 255, 39f / 255, 1);
                }
            }
        }
    }
}
