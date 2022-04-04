using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GamemanagerScript : MonoBehaviour
{
    public static GamemanagerScript saveGame;
    public List<ScriptableObject> objects = new List<ScriptableObject>();


    private void Awake()
    {
        if(saveGame == null)
        {
            saveGame = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        LoadScriptables();
    }

    private void OnDisable()
    {
        SaveScriptables();
    }

    public void SaveScriptables()
    {
        for(int i = 0; i<objects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath +
                string.Format("/{0}Object.dat", i)); //saves as {I}Object.dat
            //create a binary formater to read
            BinaryFormatter formatter = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            //save the data in the file
            formatter.Serialize(file, json);
            //close dataStream
            file.Close();
        }
    }

    public void LoadScriptables()
    {
        for(int i = 0; i< objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}Object.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}Object.dat", i), FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(file),objects[i]); // formater take data in a json string and convert it  
                file.Close();
            }
        }
    }

    //debug buttons
    public void Reset()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}Object.dat", i)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}Object.dat", i));
            }
        }
    }
}
