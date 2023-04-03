using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    private GameObject player;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    [System.Serializable]
    public class SaveData
    {
        public Vector3 position;
        public int sectionIndex;
    }
    
    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneCoroutine(index));
    }

    IEnumerator LoadSceneCoroutine(int index)
    {
        yield return SceneManager.LoadSceneAsync(index);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void LoadData()
    {
        StartCoroutine(LoadDataCoroutine());
    }

    IEnumerator LoadDataCoroutine()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            if (saveData.sectionIndex == SceneManager.GetActiveScene().buildIndex)
            {
                //player.GetComponent<CharacterController>().Move(saveData.position - player.transform.position);
                player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = saveData.position;
                player.GetComponent<CharacterController>().enabled = true;
            }
            else
            {
                yield return LoadSceneCoroutine(saveData.sectionIndex);
                //player.GetComponent<CharacterController>().Move(saveData.position - player.transform.position);
                player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = saveData.position;
                player.GetComponent<CharacterController>().enabled = true;
            }
        }
        yield return null;
    }
}
