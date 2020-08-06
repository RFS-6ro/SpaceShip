using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveLoadSystem
    {
        public static void Save(Save save)
        {
            string json = JsonUtility.ToJson(save);
            File.WriteAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + "save.txt", json);
        }

        public static Save Load()
        {
            Save data = null;
            if (File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + "save.txt"))
            {
                data = ScriptableObject.CreateInstance<Save>();
                string json = File.ReadAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + "save.txt");
                JsonUtility.FromJsonOverwrite(json, data);
            }
            else
            {
                data = Resources.Load<Save>("save");
            }
            return data;
        }
    }
}
