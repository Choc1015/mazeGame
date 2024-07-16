using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum tileType
{
    �����,
    ��
}

public class GenerationWall : MonoBehaviour
{
    public int grid = 10;
    public float speed = 1;

    public GameObject wall;
    public GameObject wallBox;
    public GameObject spike;
    public GameObject point;
    public GameObject endPoint;
    public GameObject time;

    Vector3[,] startPos;
    GameObject[,] tiles;

    private void Start()
    {
        startPos = new Vector3[grid, grid];
        tiles = new GameObject[grid, grid];
        for (int y = 0; y < grid; y++)
        {
            for (int x = 0; x < grid; x++)
            {
                wall.transform.position = new Vector3(x, 0.5f, y);
                GameObject par = Instantiate(wall);
                par.transform.parent = wallBox.transform;
                tiles[y, x] = par;
            }
        }
        GenerateBySideWinder();
        InvokeRepeating("rndMoveWall", 3, 3); 
        InvokeRepeating("rndSpawnSpike", 3, 3);
        rndSpawnPoint();
        rndSpawnTime();
    }

    void GenerateBySideWinder()
    {
        // �ϴ� ���� �� ���ƹ����� �۾�
        for (int y = 0; y < grid; y++)
        {
            for (int x = 0; x < grid; x++)
            {
                if (x % 2 == 0 || y % 2 == 0)
                {
                    tiles[y, x].SetActive(true);
                    tiles[y, x].name = tileType.��.ToString();
                }
                else
                {
                    tiles[y, x].SetActive(false);
                    tiles[y, x].name = tileType.�����.ToString();
                }
            }
        }

        // �������� ���� Ȥ�� �Ʒ��� ���� �վ������.
        for (int y = 0; y < grid; y++)
        {
            int count = 1;
            for (int x = 0; x < grid; x++)
            {
                if (x % 2 == 0 || y % 2 == 0)
                {
                    continue; // ��Ƽ�� ������ �Ʒ��� �Ȱ��� ������ ���ư���.
                }
                if (y == grid - 2 && x == grid - 2)
                    continue;
                if (y == grid - 2)
                {
                    tiles[y, x + 1].SetActive(false);
                    tiles[y, x + 1].name = tileType.�����.ToString();
                    continue;
                }
                if (x == grid - 2)
                {
                    tiles[y + 1, x].SetActive(false);
                    tiles[y + 1, x].name = tileType.�����.ToString();
                    continue;
                }
                if (Random.Range(0, 2) == 0)
                {
                    tiles[y, x + 1].SetActive(false);
                    tiles[y, x + 1].name = tileType.�����.ToString();
                    count++;
                }
                else
                {
                    int randomIndex = Random.Range(0, count);
                    tiles[y + 1, x - randomIndex * 2].SetActive(false);
                    tiles[y + 1, x - randomIndex * 2].name = tileType.�����.ToString();
                    count = 1;
                }
            }
        }
        Instantiate(endPoint, tiles[grid - 2, grid - 2].transform.position, Quaternion.identity);
    }

    void rndMoveWall()
    {
        for (int y = 0; y < grid; y++)
        {
            for (int x = 0; x < grid; x++)
            {
                if (x == 0 || y == 0 || x == grid - 1 || y == grid - 1 || x == 1 || y == 1)
                {
                    continue;
                }
                if (Random.Range(0, grid) == 0)
                {
                    if (tiles[y, x].name == tileType.��.ToString())
                    {
                        tiles[y, x].name = "������ ��";
                        StartCoroutine(moveAndReset(y, x));
                    }
                }
            }
        }
    }

    void rndSpawnSpike()
    {
        for (int y = 0; y < grid; y++)
        {
            for (int x = 0; x < grid; x++)
            {
                if (x == 0 || y == 0 || x == grid - 1 || y == grid - 1)
                {
                    continue;
                }
                if (Random.Range(0, 2000 / grid) == 0)
                {
                    if (tiles[y, x].name == tileType.�����.ToString())
                    {
                        tiles[y, x].name = "����";
                        StartCoroutine(spawnSpike(y, x));
                    }
                }
            }
        }
    }

    void rndSpawnPoint()
    {
        for (int y = 0; y < grid; y++)
        {
            for (int x = 0; x < grid; x++)
            {
                if (x == 0 || y == 0 || x == grid - 1 || y == grid - 1)
                {
                    continue;
                }
                if (Random.Range(0, grid / 2) == 0)
                {
                    if (tiles[y, x].name == tileType.�����.ToString())
                    {
                        tiles[y, x].name = "����";
                        StartCoroutine(spawnPoint(y, x));
                    }
                }
            }
        }
    }
    void rndSpawnTime()
    {
        for (int y = 0; y < grid; y++)
        {
            for (int x = 0; x < grid; x++)
            {
                if (x == 0 || y == 0 || x == grid - 1 || y == grid - 1)
                {
                    continue;
                }
                if (Random.Range(0, grid / 2) == 0)
                {
                    if (tiles[y, x].name == tileType.�����.ToString())
                    {
                        tiles[y, x].name = "�ð�";
                        StartCoroutine(spawnTime(y, x));
                    }
                }
            }
        }
    }

    IEnumerator moveAndReset(int y, int x)
    {
        int value = Random.Range(-1, 2);
        int delay = 5;

        startPos[y, x] = tiles[y, x].transform.position;

        Vector3 offset = new Vector3(value, 0, value);
        Vector3 targetPos = startPos[y, x] + offset;

        while (Vector3.Distance(tiles[y, x].transform.position, targetPos) > 0)
        {
            tiles[y, x].transform.position = Vector3.MoveTowards(tiles[y, x].transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(delay);

        while (Vector3.Distance(tiles[y, x].transform.position, startPos[y, x]) > 0)
        {
            tiles[y, x].transform.position = Vector3.MoveTowards(tiles[y, x].transform.position, startPos[y, x], speed * Time.deltaTime);
            tiles[y, x].name = "��";
            yield return null;
        }

        yield return new WaitForSeconds(delay);
    }

    IEnumerator spawnSpike(int y, int x)
    {
        int delay = 5;

        Vector3 spawnPos = tiles[y, x].transform.position - new Vector3(0, 0.5f, 0);   
        GameObject[,] spawnedSpike =  new GameObject[grid,grid];
        spawnedSpike[y,x] = Instantiate(spike, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(delay);

        Destroy(spawnedSpike[y, x]);
            tiles[y, x].name = "�����";
        yield return new WaitForSeconds(delay);
    }
    IEnumerator spawnPoint(int y, int x)
    {
        int delay = 2;
        yield return new WaitForSeconds(delay);

        Vector3 spawnPos = tiles[y, x].transform.position - new Vector3(0, 0.5f, 0);
        GameObject[,] spawnedPoint = new GameObject[grid, grid];
        spawnedPoint[y, x] = Instantiate(point, spawnPos, Quaternion.identity);
       
    }

    IEnumerator spawnTime(int y, int x)
    {
        int delay = 2;
        yield return new WaitForSeconds(delay);

        Vector3 spawnPos = tiles[y, x].transform.position - new Vector3(0, 0.5f, 0);
        GameObject[,] spawnedTime = new GameObject[grid, grid];
        spawnedTime[y, x] = Instantiate(time, spawnPos, Quaternion.identity);

    }
}
