using Firebase.Storage;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System;
public static class UserDataManager

{
    private const string PROGRESS_KEY = "Progress";
    public static UserProgressData Progress;

    public static void LoadFromLocal()
    {
        // Cek apakah ada data yang tersimpan sebagai PROGRESS_KEY
        if (!PlayerPrefs.HasKey(PROGRESS_KEY))
        {
            // Jika tidak ada, maka simpan data baru
            // dan upload ke Cloud
            Save(true);
        }
        else
        {
            // Jika ada, maka timpa progress dengan yang sebelumnya
            string json = PlayerPrefs.GetString(PROGRESS_KEY);
            Progress = JsonUtility.FromJson<UserProgressData>(json);
        }
    }
    public static void Save(bool uploadToCloud = false)
    {
        string json = JsonUtility.ToJson(Progress);
        PlayerPrefs.SetString(PROGRESS_KEY, json);

        if (uploadToCloud)
        {
            AnalyticsManager.SetUserProperties("gold", Progress.Gold.ToString());
            byte[] data = Encoding.Default.GetBytes(json);
            StorageReference targetStorage = GetTargetCloudStorage();

            targetStorage.PutBytesAsync(data);
        }
    }

    public static IEnumerator LoadFromCloud (System.Action onComplete)
    {
        StorageReference targetStorage = GetTargetCloudStorage();
        bool isCompleted = false;
        bool isSuccessfull = false;
        const long maxAllowedSize = 1024 * 1024; // Sama dengan 1 MB
        targetStorage.GetBytesAsync (maxAllowedSize).ContinueWith ((Task<byte[]> task) =>
        {
            if (!task.IsFaulted)
            {
                string json = Encoding.Default.GetString (task.Result);
                Progress = JsonUtility.FromJson<UserProgressData> (json);
                isSuccessfull = true;
            }
 
            isCompleted = true;
        });

        while (!isCompleted)
        {
            yield return null;
        }
        // Jika sukses mendownload, maka simpan data hasil download
        if (isSuccessfull)
        {
            Save ();
        }
        else
        {
            // Jika tidak ada data di cloud, maka load data dari local
            LoadFromLocal ();
        }
        onComplete?.Invoke ();
    }
    private static StorageReference GetTargetCloudStorage()
    {
        // Gunakan Device ID sebagai nama file yang akan disimpan di cloud
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        return storage.GetReferenceFromUrl($"{storage.RootReference}/{deviceID}");
    }
    public static bool HasResources(int index)
    {
        return index + 1 <= Progress.ResourcesLevels.Count;
    }
    public static void Load()
    {
        // Cek apakah ada data yang tersimpan sebagai PROGRESS_KEY
        if (!PlayerPrefs.HasKey(PROGRESS_KEY))
        {
            // Jika tidak ada, maka buat data baru
            Progress = new UserProgressData();
            Save();
        }
        else
        {
            // Jika ada, maka timpa progress dengan yang sebelumnya
            string json = PlayerPrefs.GetString(PROGRESS_KEY);
            Progress = JsonUtility.FromJson<UserProgressData>(json);
        }
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(Progress);
        PlayerPrefs.SetString(PROGRESS_KEY, json);
    }
}