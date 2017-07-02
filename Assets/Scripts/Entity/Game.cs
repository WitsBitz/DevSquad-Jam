using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	public static Game instance;
	public GameObject playerPrefab;
    public GameObject Player { get; set; }
    bool inEncounter;

    public List<GameObject> Enemies {get; set;}
    public List<Door> Doors {get; set;}

	void Awake()
	{
        Enemies = new List<GameObject>();
        Doors = new List<Door>();

        if(Player == null)
            Player = GameObject.Find("Player");

		        //Check if instance already exists
            if (instance == null)
                
                //if not, set instance to this
                instance = this;
            
            //If instance already exists and it's not this:
            else if (instance != this)
                
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);    
            
            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(Player);

            SceneManager.sceneLoaded += OnSceneLoaded;
	}

    void Update()
    {
        if(inEncounter)
        {
            if(Enemies.Count <= 0)
            {
                inEncounter = false;
                foreach(Door door in Doors)
                {
                    door.Locked = false;
                }
                Doors.Clear();
            }
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(Player == null)
        {
            Player = GameObject.Find("Player");
            if(Player == null)
            {
                Player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            }
        }

        Player.transform.position = Vector3.zero;
        Player.transform.rotation = Quaternion.identity;

        StartEncounter();
    }

    void StartEncounter()
    {
        Enemies .Clear();
        Doors.Clear();

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemies.Add(enemy);
        }

        foreach(GameObject doorGO in GameObject.FindGameObjectsWithTag("Door"))
        {
            Door door = doorGO.GetComponent<Door>();
            Doors.Add(door);
        }

        if(Enemies.Count > 0)
        {
            inEncounter = true;
            foreach(Door door in Doors)
            {
                door.Locked = true;
            }
        }

        Debug.Log("Encounter Started!");
        Debug.Log("There are " + Enemies.Count + " Enemies");
        Debug.Log("There are " + Doors.Count + " Doors");
        if(Enemies.Count > 0)
            Debug.Log("Doors are locked!");
        else
            Debug.Log("Doors are NOT locked!");
    }
}
