using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInfoManager : MonoBehaviour
{
    public List<string> infos;
    public Text infoText;
    string UIText;
    // Update is called once per frame
    private void Start()
    {
        infos = new List<string>();
        StartCoroutine(DeleteInfo());
    }
    void Update()
    {
        infoText.text = UIText;
    }

    public void AddInfo(string _string)
    {
        infos.Add(_string);
        WatchInfo();
    }

    public void WatchInfo()
    {
        UIText = "";
        for (int i = 0; i < infos.Count; i++)
        {
            UIText += (infos[i] + "\n");
        }
    }
    IEnumerator DeleteInfo()
    {
        while (true)
        {
            if (infos.Count > 0)
            {
                yield return new WaitForSeconds(2f);
                infos.RemoveAt(0);
                WatchInfo();
            }
            else
            {
                UIText = "";
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
