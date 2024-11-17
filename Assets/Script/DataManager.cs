using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace Data
{
    public class DataManager : MonoBehaviour
    {
        private static DataManager _instance;
        public static DataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<DataManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("DataManager");
                        _instance = go.AddComponent<DataManager>();
                        DontDestroyOnLoad(go);
                    }
                }
                return _instance;
            }
        }
        private string SaveDataPath
        {
            get
            {
#if UNITY_EDITOR
                return Path.Combine(Application.dataPath, "SaveData");
#else
                return Path.Combine(Application.persistentDataPath, "SaveData");
#endif
            }
        }
        void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void SaveData()
        {
        
        }

        private void LoadData()
        {
        
        }
    }   
}