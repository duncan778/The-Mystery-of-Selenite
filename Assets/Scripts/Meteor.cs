using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] float speed = 10;

    [SerializeField] Dialog dialogPr = null;
    private Dialog dialog;
    [SerializeField] string[] sentences;


    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        if (transform.position.y <= -10)
        {
            dialog = Instantiate(dialogPr);
            dialog.SetSentences(sentences);
            Destroy(gameObject);
        }
    }
}
