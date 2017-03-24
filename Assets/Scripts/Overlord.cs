using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Overlord : MonoBehaviour 
{
    public static Overlord instance;

    public List<GameObject> Bodies = new List<GameObject>();
    public List<AudioClip> Notes;

    [HideInInspector] public CelestialBodyOverlord CBO;
    [HideInInspector] public InputOverlord IO;
    [HideInInspector] public GameObject Sun;
    [HideInInspector] public System.Random Random;

    #region prefabs
    public GameObject SunPrefab;
    public GameObject PlanetPrefab;
    public GameObject LinePrefab;
    public GameObject SpacePrefab;

    public AudioClip A0;
    public AudioClip A1;
    public AudioClip A2;
    public AudioClip A3;
    public AudioClip A4;
    public AudioClip A5;
    public AudioClip A6;
    public AudioClip A7;
    public AudioClip A8;
    public AudioClip A9;

    public AudioClip B0;
    public AudioClip B1;
    public AudioClip B2;
    public AudioClip B3;
    public AudioClip B4;
    public AudioClip B5;
    public AudioClip B6;
    public AudioClip B7;
    public AudioClip B8;
    public AudioClip B9;
    
    #endregion

    void Start () 
    {
        instance = this;

        AudioListener.volume = 0;

        Notes = new List<AudioClip>() {A0, A1, A2, A3, A4, A5, A6, A7, A8, A9, B0, B1, B2, B3, B4, B5, B6, B7, B8, B9};

        Random = new System.Random();

        CBO = new CelestialBodyOverlord();
        IO = new InputOverlord();
        IO.Init();
        StartCoroutine(IO.Delay());
        
        Sun = GameObject.Instantiate(SunPrefab) as GameObject;
        Sun.transform.position = new Vector3(0, 0, 0);

        GetComponent<AudioSource>().volume = 0f;
        GetComponent<AudioSource>().Play();



        //SetupSpace();

        //CBO.AddBody(new Vector2(3, -3), new Vector2(.4f, 1f), PlanetPrefab);
        //CBO.AddBody(new Vector2(-3, -3), new Vector2(-.4f, 1f), PlanetPrefab);
        //CBO.AddBody(new Vector2(-2, 4), new Vector2(-.6f, -1f), PlanetPrefab);
	}
	
	void Update () 
    {
        IO.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            System.Action quit = Application.Quit;
            CameraFade.StartAlphaFade(Color.black, false, 3f, 0f, quit);
        }
	}

    void FixedUpdate()
    {
        if (GetComponent<AudioSource>().isPlaying && AudioListener.volume < .8f)
        {
            AudioListener.volume += .001f;
        }

        if (GetComponent<AudioSource>().isPlaying && GetComponent<AudioSource>().volume < .5f)
        {
            GetComponent<AudioSource>().volume += .001f;
        }

        CBO.Update();
    }

    void SetupSpace()
    {
        float posX = -9.9f;
        float posY = 4.9f;

        for (int k = 0; k < 20; k++)
        {
            for (int i = 0; i < 35; i++)
            {
                posX += .64f;

                GameObject temp = GameObject.Instantiate(SpacePrefab) as GameObject;
                temp.transform.Rotate(0, 0, 90 * Random.Next(0, 4));
                temp.transform.position = new Vector3(posX, posY, .5f);
            }

            posY -= .64f;
            posX = -9.9f;
        }
    }
}
