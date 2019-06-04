using System.IO;
using System.Collections;
using UnityEngine;

public class ObbExtractor : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ExtractObbDatasets());
    }

    private IEnumerator ExtractObbDatasets()
    {
        string[] filesInOBB = 
        {
            "AlchemistRunes.dat",
            "AlchemistRunes.xml",
            "ArcanistRunes.dat",
            "ArcanistRunes.xml",
            "ChronomancerRunes.dat",
            "ChronomancerRunes.xml",
            "ElementalistRunes.dat",
            "ElementalistRunes.xml",
            "SummonerRunes.dat",
            "SummonerRunes.xml",
            "TricksterRunes.dat",
            "TricksterRunes.xml",
            "BoardImages1.dat",
            "BoardImages1.xml"
        };
        foreach (var filename in filesInOBB)
        {
            string uri = Application.streamingAssetsPath + "/QCAR/" + filename;

            string outputFilePath = Application.persistentDataPath + "/QCAR/" + filename;
            if (!Directory.Exists(Path.GetDirectoryName(outputFilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));

            var www = new WWW(uri);
            yield return www;

            Save(www, outputFilePath);
            yield return new WaitForEndOfFrame();
        }
    }

    private void Save(WWW www, string outputPath)
    {
        File.WriteAllBytes(outputPath, www.bytes);

        // Verify that the File has been actually stored
        if (File.Exists(outputPath))
        {
            Debug.Log("File successfully saved at: " + outputPath);
        }
        else
        {
            Debug.Log("Failure!! - File does not exist at: " + outputPath);
        }
    }
}