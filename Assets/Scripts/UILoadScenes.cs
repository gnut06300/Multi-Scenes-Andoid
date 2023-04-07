using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] GameObject joystickMove;
    [SerializeField] GameObject joystickCamera;
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
        audioSource.volume = data.soundVolume;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            FindObjectOfType<Scrollbar>().value = data.soundVolume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        data.position = player.transform.position;
        data.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        //data.soundVolume = audioSource.volume;
    }

    public void ShowMenu()
    {
        Debug.Log("Boutton appuyé");
        if (!active)
        {
            active = true;
            menu.SetActive(active);
            FindObjectOfType<Scrollbar>().value = data.soundVolume;
            joystickMove.SetActive(false);
            joystickCamera.SetActive(false);
            //Cursor.lockState = CursorLockMode.None;
            camera1.GetComponent<SojaExiles.MouseLook>().enabled = false;

        }
        else
        {
            active = false;
            menu.SetActive(active);
            joystickMove.SetActive(true);
            joystickCamera.SetActive(true);
            //Cursor.lockState = CursorLockMode.Locked;
            camera1.GetComponent<SojaExiles.MouseLook>().enabled = true;
        }
    }
    public void UpdateVolume(float volume)
    {
        audioSource.volume = volume;
        data.soundVolume = volume;
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
        public float soundVolume;
    }

    public void SaveDataFile()
    {
        SaveData saveData = new SaveData();
        saveData.position = data.position;
        saveData.sectionIndex = data.sceneIndex;
        saveData.soundVolume = data.soundVolume;
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