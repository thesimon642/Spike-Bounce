using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InfiniteLevel : MonoBehaviour
{
    private int unloadingLeft;
    private int loadingRight;
    private int lastUnloaded=-1;
    private int lastloaded = 0;
    private readonly int maxTileHeightBackground = 30;
    private readonly int minTileHeightBackground = -30;
    public Transform playerPosition;
    public Tilemap blocks;
    public Tile topBlock;
    public Tile botBlock;
    public Tile topleft;
    public Tile botleft;
    public Tile StandAloneBlock;

    public static int scoreContributedFromDistance;

   // public Tilemap background;
   // public Tile backgroundTile;

    public Tilemap spikes;
    public Tile spikeTile;

    private int rollNewInt;
    private int rollHeight;
    private int lastspikeblocklocation=0;

    public GameObject EnemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        lastUnloaded = -1;
        lastloaded = 0;
        blocks.SetTile(new Vector3Int(0, 0, 0), botleft);
        blocks.SetTile(new Vector3Int(0, 1, 0), topleft);
        scoreContributedFromDistance = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        unloadingLeft = Mathf.FloorToInt(playerPosition.position.x-40);
        loadingRight = Mathf.FloorToInt(playerPosition.position.x + 40);

        //put in floor and background
        for (int i = lastloaded+1; i <= loadingRight; i++)
        {
            blocks.SetTile(new Vector3Int(i, 1, 0), topBlock);
            blocks.SetTile(new Vector3Int(i, 0, 0), botBlock);



            //loads spikes randomly only after first 20 blocks
            if (i > 20)
            {
                rollNewInt = Mathf.FloorToInt(Random.Range(0f, 2.99f));
                if (rollNewInt == 0)
                {
                    spikes.SetTile(new Vector3Int(i, 2, 0), spikeTile);
                }
            }
            //loads blocks/enemies randomly every 5 blocks after block 20
            if (i > 20 && (i%5 == 0))
            {
                //decide the block:
                //0-0.99 spawn enemy
                //1-1.99 spawn spike block
                //2-3.99 spawn basic block
                rollNewInt = Mathf.FloorToInt(Random.Range(0f, 3.99f));
                rollHeight = Mathf.FloorToInt(Random.Range(3f, 9.99f));
                //don't allow 2 spike blocks to spawn next to each other
                if (rollNewInt == 1 && lastspikeblocklocation == i - 5)
                { rollNewInt = 2; }
                switch(rollNewInt)
                {
                    case 0:
                        //enemy
                        Instantiate(EnemyPrefab,new Vector3(i,4f,0f),Quaternion.identity);
                        break;
                    case 1:
                        //spike block
                        blocks.SetTile(new Vector3Int(i, rollHeight, 0), StandAloneBlock);
                        spikes.SetTile(new Vector3Int(i,rollHeight+1,0),spikeTile);
                        lastspikeblocklocation = i;
                        break;
                    default:
                        //basic block
                        blocks.SetTile(new Vector3Int(i, rollHeight, 0), StandAloneBlock);
                        break;


                }
            }
            //load background
            //for (int j = minTileHeightBackground; j <= maxTileHeightBackground; j++)
           // {
          //      background.SetTile(new Vector3Int(i, j, 0), backgroundTile);
          //  }
        }
        lastloaded = loadingRight;
        //unload old floor and background
        for (int i = lastUnloaded - 1; i <= unloadingLeft; i++)
        {
            for (int j = minTileHeightBackground; j <= maxTileHeightBackground; j++)
            {
                //background.SetTile(new Vector3Int(i, j, 0), null);
                blocks.SetTile(new Vector3Int(i, j, 0), null);
                spikes.SetTile(new Vector3Int(i, j, 0), null);
            }
        }
        lastUnloaded = unloadingLeft;

        scoreContributedFromDistance = Mathf.Max(scoreContributedFromDistance,0,loadingRight-49);
    }
}
