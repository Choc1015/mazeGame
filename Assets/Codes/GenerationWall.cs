using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum tileType
{
    빈공간,
    벽
}
public class GenerationWall : MonoBehaviour
{
    static public GenerationWall Instance;

    public int grid = GameManager.grid;
    public float speed = 1;

    public GameObject wall;
    public GameObject wallBox;
    public GameObject spike;
    public GameObject point;
    public GameObject endPoint;
    public GameObject time;
    public GameObject path;
    public GameObject pathItem;

    Vector3[,] startPos;
    GameObject[,] tiles;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Debug.Log($"1 .{GameManager.grid} , \n 2 .{GameManager.spikePercent},\n 3 .{GameManager.pointPercent},\n 4 .{GameManager.timePercent}");
        grid = GameManager.grid;

        if (grid % 2 == 0)
        {
            grid += 1;
        }

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
        rndSpawnPath();

        StartCoroutine(destroyTimePath(tiles[1,1].transform.position));
        
    }

    void GenerateBySideWinder()
    {
        for (int y = 0; y < grid; y++)
        {
            for (int x = 0; x < grid; x++)
            {
                if (x % 2 == 0 || y % 2 == 0)
                {
                    tiles[y, x].SetActive(true);
                    tiles[y, x].name = tileType.벽.ToString();
                }
                else
                {
                    tiles[y, x].SetActive(false);
                    tiles[y, x].name = tileType.빈공간.ToString();
                }
            }
        }

        for (int y = 0; y < grid; y++)
        {
            int count = 1;
            for (int x = 0; x < grid; x++)
            {
                if (x % 2 == 0 || y % 2 == 0)
                {
                    continue;
                }
                if (y == grid - 2 && x == grid - 2)
                    continue;
                if (y == grid - 2)
                {
                    tiles[y, x + 1].SetActive(false);
                    tiles[y, x + 1].name = tileType.빈공간.ToString();
                    continue;
                }
                if (x == grid - 2)
                {
                    tiles[y + 1, x].SetActive(false);
                    tiles[y + 1, x].name = tileType.빈공간.ToString();
                    continue;
                }
                if (Random.Range(0, 2) == 0)
                {
                    tiles[y, x + 1].SetActive(false);
                    tiles[y, x + 1].name = tileType.빈공간.ToString();
                    count++;
                }
                else
                {
                    int randomIndex = Random.Range(0, count);
                    tiles[y + 1, x - randomIndex * 2].SetActive(false);
                    tiles[y + 1, x - randomIndex * 2].name = tileType.빈공간.ToString();
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
                if (Random.Range(0, GameManager.movingPercent) == 0)
                {
                    if (tiles[y, x].name == tileType.벽.ToString())
                    {
                        tiles[y, x].name = "움직인 벽";
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
                if (Random.Range(0, GameManager.spikePercent) == 0)
                {
                    if (tiles[y, x].name == tileType.빈공간.ToString())
                    {
                        tiles[y, x].name = "함정";
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
                if (Random.Range(0, GameManager.pointPercent) == 0)
                {
                    if (tiles[y, x].name == tileType.빈공간.ToString())
                    {
                        tiles[y, x].name = "점수";
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
                if (Random.Range(0, GameManager.timePercent) == 0)
                {
                    if (tiles[y, x].name == tileType.빈공간.ToString())
                    {
                        tiles[y, x].name = "시간";
                        StartCoroutine(spawnTime(y, x));
                    }
                }
            }
        }
    }
    void rndSpawnPath()
    {
        for (int y = 0; y < grid; y++)
        {
            for (int x = 0; x < grid; x++)
            {
                if (x == 0 || y == 0 || x == grid - 1 || y == grid - 1)
                {
                    continue;
                }
                if (Random.Range(0, GameManager.grid) == 0)
                {
                    if (tiles[y, x].name == tileType.빈공간.ToString())
                    {
                        Vector3 spawnPos = tiles[y, x].transform.position - new Vector3(0, 0.5f, 0);
                        GameObject[,] spawnedPath = new GameObject[grid, grid];
                        spawnedPath[y, x] = Instantiate(pathItem, spawnPos, Quaternion.identity);
                        
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
            tiles[y, x].name = "벽";
            yield return null;
        }

        yield return new WaitForSeconds(delay);
    }

    IEnumerator spawnSpike(int y, int x)
    {
        int delay = 5;

        Vector3 spawnPos = tiles[y, x].transform.position - new Vector3(0, 0.5f, 0);
        GameObject[,] spawnedSpike = new GameObject[grid, grid];
        spawnedSpike[y, x] = Instantiate(spike, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(delay);

        Destroy(spawnedSpike[y, x]);
        tiles[y, x].name = "빈공간";
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

  

    public IEnumerator destroyTimePath(Vector3 pathItemPos)
    {
        foreach (Vector3 position in FindShortestPath(pathItemPos, tiles[grid - 2, grid - 2].transform.position))
        {
            GameObject par = Instantiate(path, position - new Vector3(0, 0.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(.5f);
            Destroy(par);
        }
    }

    public List<Vector3> FindShortestPath(Vector3 start, Vector3 end) // 시작 지점 도착 지점 가져오고
    {
        Queue<Vector3> queue = new Queue<Vector3>();
        Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();
        queue.Enqueue(start);
        cameFrom[start] = start;

        while (queue.Count > 0)
        {
            Vector3 current = queue.Dequeue(); // 현재 위치 꺼내서 
            if (current == end) // 현재 위치가 끝이면 나오고 
            {
                break;
            }

            foreach (Vector3 next in GetNeighbors(current)) // 현재 위치에서 사방의 위치를 가져와서 체크 
            {
                if (!cameFrom.ContainsKey(next) && tiles[(int)next.z, (int)next.x].name != tileType.벽.ToString()) 
                {   // next없고 ~ tile이 다음 좌표 값의 이름이 벽이 아니어야지 다음 큐를 저장 
                    queue.Enqueue(next);
                    cameFrom[next] = current; // 넥스트 현재로 저장
                }
            }
        }

        List<Vector3> path = new List<Vector3>();
        if (!cameFrom.ContainsKey(end))
        {
            return path; // 길이 없으면 반환
        }

        Vector3 step = end; // 스텦에 엔드 넣고 
        while (step != start) // 시작점이 아니어야지만 계속 돌리고 
        {
            path.Add(step);
            step = cameFrom[step];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }

    List<Vector3> GetNeighbors(Vector3 position) //옆 타일 정보 얻기
    {
        List<Vector3> neighbors = new List<Vector3>(); // 이웃 타일

        Vector3[] directions = new Vector3[] // 상하좌우 체크 
        {
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1)
        };

        foreach (Vector3 direction in directions)
        {
            Vector3 neighbor = position + direction; // 가운데 기준에 방향 더해
            if (neighbor.x >= 0 && neighbor.x < grid && neighbor.z >= 0 && neighbor.z < grid) // 미로 안으로 제한 
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }
}
