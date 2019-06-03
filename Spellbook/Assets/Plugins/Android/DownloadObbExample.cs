using UnityEngine;
using System.Collections;

public class DownloadObbExample : MonoBehaviour
{
    private IGooglePlayObbDownloader m_obbDownloader;
    void Start()
    {
        m_obbDownloader = GooglePlayObbDownloadManager.GetGooglePlayObbDownloader();
        // YOUR PUBLIC KEY HERE (this is spellbook's public key)
        m_obbDownloader.PublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmbXD+JdeFn2dp41yRJHOXPIztz9/9oftnX0Se1rWOXqGtEW5uxXXX6ERHb9F8k8MetgtmIndwj4hl9nCKb6O0soBTY09DdUrwNmPemU16SgiCCxR3l0TavcgdBGyrMrRsqCbG+D4Utl8cbFA+nQ42B0ZV2Lof1JI6XB1AfvMwGZHGBu6Rz5D+2QzbEukWxlw+Rgu+Ei5uVeSY98FYxy0Ki8odoImAOigfgzm1y+/Tw/MivXjXatWsr5XWxiozAYGBjC6Bj14NbBzp7I51zetlfYdUBIc2uX5hAK/6vQ0ic7KgGXwPHOayolZRZ6fZc3mw347qxIqOzEQrFHfBo98mQIDAQAB"; 
    }	

    void OnGUI()
    {
        if (!GooglePlayObbDownloadManager.IsDownloaderAvailable())
        {
            GUI.Label(new Rect(10, 10, Screen.width-10, 20), "Use GooglePlayDownloader only on Android device!");
            return;
        }
        
        string expPath = m_obbDownloader.GetExpansionFilePath();
        if (expPath == null)
        {
                GUI.Label(new Rect(10, 10, Screen.width-10, 20), "External storage is not available!");
        }
        else
        {
            var mainPath = m_obbDownloader.GetMainOBBPath();
            var patchPath = m_obbDownloader.GetPatchOBBPath();
            
            GUI.Label(new Rect(10, 10, Screen.width-10, 20), "Main = ..."  + ( mainPath == null ? " NOT AVAILABLE" :  mainPath.Substring(expPath.Length)));
            GUI.Label(new Rect(10, 25, Screen.width-10, 20), "Patch = ..." + (patchPath == null ? " NOT AVAILABLE" : patchPath.Substring(expPath.Length)));
            if (mainPath == null || patchPath == null)
                if (GUI.Button(new Rect(10, 100, 100, 100), "Fetch OBBs"))
                    m_obbDownloader.FetchOBB();
        }

    }
}
