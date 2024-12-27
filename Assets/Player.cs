using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Player : MonoBehaviour
{
    public int ErosionRange;
    public int ErosionSpeed;
    public float ErosionFrequency;
    public float ErosionTime;
    public int CloseErosionRange;
    public int CloseErosionSpeed;
    public Tilemap[] Tilemaps;
    public Tilemap[] AllTilemaps;

    public Tile Bridge;

    public float Speed;
    public float JumpPower;
    public float JumpPlataformPower;

    public Rigidbody2D r;

    public bool Grounded;

    public GameObject EnergyCircle;

    public GameObject Camera;

    public GameObject DeathParticles;
    public GameObject DeathSplat;
    public GameObject BoingSplat;
    public Animator CameraAnimator;
    public RipplePostProsessor Rippler;

    public LevelManager SceneTransisionDevice;

    public GameObject VictoryEffect;

    public AudioSource source;
    public AudioClip burstSound;
    public AudioClip DeathSound;
    // Update is called once per frame

    private void Start()
    {
        source = FindObjectOfType<AudioSource>();
    }
    void Update()
    {
        if(this.transform.position.y <= -5)
        {
            Die();
        }
        Vector2 Spot = Camera.transform.position;
        //Camera.transform.position = new Vector3((Spot.x + transform.position.x) / 2,0,0);
        Camera.transform.position = Vector2.Lerp(Spot, this.transform.position,Time.deltaTime * 2f);
        Camera.transform.position = new Vector3(Camera.transform.position.x, 0, 0);
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                r.AddForce(new Vector3(-Speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                r.AddForce(new Vector3(Speed * Time.deltaTime, 0, 0));
            }
            if((Input.GetKeyDown(KeyCode.UpArrow) ||Input.GetKeyDown(KeyCode.W)) && Grounded)
            {
                r.velocity = new Vector3( r.velocity.x,JumpPower, 0);
            }
            ErosionTime = 0;
        }
        else
        {
            //Rippler.RippleEffect(1, transform.position, 0.5f);
            ErosionTime += Time.deltaTime;
            if (ErosionTime >= ErosionFrequency)
            {
                source.PlayOneShot(burstSound);
                Instantiate(EnergyCircle).transform.position = this.transform.position;
                Rippler.RippleEffect(17, transform.position, 0.95f);
                ErosionTime -= ErosionFrequency;
                Vector3Int PlayerPos = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
                for(int Y = ErosionRange; Y >= -ErosionRange; Y--)
                {
                    for (int X = ErosionRange; X >= -ErosionRange; X--)
                    {
                        if (Mathf.Pow(X, 2) + Mathf.Pow(Y, 2) < Mathf.Pow(ErosionRange, 2))
                        {
                            if (UnityEngine.Random.Range(0, ErosionSpeed) == 0)
                            {
                                foreach (Tilemap t in Tilemaps)
                                {
                                    t.SetTile(new Vector3Int(X + PlayerPos.x, Y + PlayerPos.y, 0), null);
                                }
                            }
                        }
                    }
                }
                for (int Y = CloseErosionRange; Y >= -CloseErosionRange; Y--)
                {
                    for (int X = CloseErosionRange; X >= -CloseErosionRange; X--)
                    {
                        if (Mathf.Pow(X, 2) + Mathf.Pow(Y, 2) < Mathf.Pow(CloseErosionRange, 2))
                        {
                            if (UnityEngine.Random.Range(0, CloseErosionSpeed) == 0)
                            {
                                foreach (Tilemap t in Tilemaps)
                                {
                                    t.SetTile(new Vector3Int(X + PlayerPos.x, Y + PlayerPos.y, 0), null);
                                }
                            }
                        }
                    }
                }

            }

        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Death")
        {
            Die();
        }
        if (collision.gameObject.tag == "Goal")
        {
            NextLevle();
        }
        if (collision.gameObject.tag == "Jump")
        {
            Bounce();
        }
        /*
        if (collision.gameObject.tag == "SpreadTile")
        {
            SpreadTile(collision.gameObject.GetComponent<Tilemap>());
        }
        */
    }

    public void Die()
    {
        Instantiate(DeathParticles).transform.position = this.transform.position;
        Instantiate(DeathSplat).transform.position = this.transform.position;
        CameraAnimator.SetTrigger("ShakeSkreen");
        Rippler.RippleEffect(60, transform.position,0.99f);
        SceneTransisionDevice.ChooseScene(-2);
        source.PlayOneShot(DeathSound);
        Destroy(this.gameObject);
    }
    public void NextLevle()
    {
        Instantiate(VictoryEffect).transform.position = this.transform.position;
        //CameraAnimator.SetTrigger("ShakeSkreen");
        Rippler.RippleEffect(60, transform.position, 0.99f);
        SceneTransisionDevice.ChooseScene(-1);
        Destroy(this.gameObject);
    }
    public void Bounce()
    {
        Instantiate(BoingSplat).transform.position = this.transform.position;
        //CameraAnimator.SetTrigger("ShakeSkreen");
        Rippler.RippleEffect(30, transform.position, 0.9f);
        r.velocity = new Vector3(r.velocity.x, JumpPlataformPower, 0);
    }
    public void SpreadTile(Tilemap Target)
    {
        bool Left = true;
        bool Right = true;
        foreach(Tilemap t in AllTilemaps)
        {
            if(t.GetTile(new Vector3Int(Mathf.RoundToInt(transform.position.x + 0.3f), Mathf.RoundToInt(transform.position.y -1),0)) != null)
            {
                Right = false;
            }
            if (t.GetTile(new Vector3Int(Mathf.RoundToInt(transform.position.x - 1.3f), Mathf.RoundToInt(transform.position.y - 1), 0)) != null)
            {
                Left = false;
            }
        }
        if(Left == true)
        {
            Target.SetTile(new Vector3Int(Mathf.RoundToInt(transform.position.x - 1.3f), Mathf.RoundToInt(transform.position.y - 1), 0),Bridge);
            Rippler.RippleEffect(20, transform.position, 0.9f);
        }
        if(Right == true)
        {
            Target.SetTile(new Vector3Int(Mathf.RoundToInt(transform.position.x + 0.3f), Mathf.RoundToInt(transform.position.y - 1), 0), Bridge);
            Rippler.RippleEffect(20, transform.position, 0.9f);
        }
        
        //r.velocity = new Vector3(r.velocity.x, JumpPlataformPower, 0);
    }
}
