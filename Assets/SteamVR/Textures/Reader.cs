using System.IO;
using UnityEngine;

public class Reader : MonoBehaviour {

    public MainControl mc;

    private void Start()
    {
        Load("input.txt");
    }
    public void Load(string path)
    {
        if (File.Exists(Application.dataPath + "/" + path))
        {
            var lines = ReadFile(Application.dataPath + "/" + path).Split('\n');
            int c;
            if (!int.TryParse(lines[0], out c))
            {
                Debug.LogError("Incorrect file format!");
                return;
            }
            for (int i = 0; i < c; ++i)
            {
                if (i + 1 >= lines.Length)
                {
                    Debug.LogWarning("Not enough lines in file! Loaded only that was");
                    break;
                }
                var nums = lines[i + 1].Split(' ');
                if (nums.Length < 4)
                {
                    Debug.LogWarning("Not enough parametres for charge! Skipping");
                    continue;
                }
                if (nums.Length > 4) Debug.LogWarning("Too many parametres for charge. Potential error in format");
                float q, x, y, z;
                if (!(
                    float.TryParse(nums[0], out q) &&
                    float.TryParse(nums[1], out x) &&
                    float.TryParse(nums[2], out y) &&
                    float.TryParse(nums[3], out z)
                  ))
                {
                    Debug.LogWarning("Can't parse one of parametres of charge. Skipping");
                    continue;
                }
                mc.Spawn((int)q, x, y, z);

            }
        }
    }
    private string ReadFile(string path)
    {
        StreamReader reader = new StreamReader(path);
        string res = reader.ReadToEnd();
        reader.Close();
        return res;
    }
}
