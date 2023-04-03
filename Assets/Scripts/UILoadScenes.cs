using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;
using System.IO;


public class UILoadScenes : MonoBehaviour
{
    public Animator transition;
    public GameObject menu;
    public float transitionTime = 1.5f;
    private bool active = false;
    [SerializeField] GameObject player;
    [SerializeField] Data data;
    [SerializeField] Camera camera1;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            menu.SetActive(active);
        }
        audioSource = camera1.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(SceneManager.GetActiveScene().buildIndex != 0)
            {
                if (!active)
                {
                    active = true;
                    menu.SetActive(active);
                    Cursor.lockState = CursorLockMode.None;
                    camera1.GetComponent<SojaExiles.MouseLook>().enabled = false;

                }
                else
                {
                    active = false;
                    menu.SetActive(active);
                    Cursor.lockState = CursorLockMode.Locked;
                    camera1.GetComponent<SojaExiles.MouseLook>().enabled = true;
                }

            }
        }
        data.position = player.transform.position;
        data.sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void UpdateVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void ChangeScene(int index)
    {
        StartCoroutine(LoadScene(index));
    }
    
    public IEnumerator LoadScene(int index)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        MainManager.Instance.LoadScene(index);
    }

    [System.Serializable]
    public class SaveData
    {
        public Vector3 position;
        public int sectionIndex;
    }

    public void SaveDataFile()
    {
        SaveData saveData = new SaveData();
        saveData.position = data.position;
        saveData.sectionIndex = data.sceneIndex;
        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        MainManager.Instance.LoadData();
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

}
