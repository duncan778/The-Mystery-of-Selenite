using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour

{
    private string[] sentences = {"привет", "Привет 2"};
    private int currentIndex = 0;
    [SerializeField] GameObject dialogPanelPr;

    private Text dialogText;
    private GameObject dialogPanel;

    public void SetSentences(string[] sentences)
    {
        int sentencesCount = sentences.Length;
        this.sentences = new string[sentencesCount];
        for (int i = 0; i < sentencesCount; i++)
        {
            this.sentences[i] = sentences[i];
        }
    }

    private void Start()
    {
        dialogPanel = Instantiate(dialogPanelPr, GameObject.Find("Canvas").transform);
        dialogText = dialogPanel.transform.GetChild(0).GetComponent<Text>();
        dialogText.text = sentences[currentIndex];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            currentIndex++;
            if (currentIndex < sentences.Length)
            {
                dialogPanel.transform.Translate(Random.Range(-20, 20), Random.Range(-20, 20), 0);
                dialogText.text = sentences[currentIndex];
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        Destroy(dialogPanel);
    }

    
}
