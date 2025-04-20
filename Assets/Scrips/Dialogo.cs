using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    [SerializeField] private GameObject DialogoUI;
    [SerializeField] private GameObject DialogoPanel;
    [SerializeField] private TMP_Text DialogoText;
    [SerializeField, TextArea(4, 6)] private string[] DialogoLines;
    private float typingTime = 0.05f;
   private bool isPlayerInRange;
   private bool diadDialogoStart;
   private int LineIndex;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!diadDialogoStart)
            {
                StartDialogo();
            }
            else if (DialogoText.text == DialogoLines[LineIndex])
            {
                NextDialogoLine();
            }
            else
            {
                StopAllCoroutines();
                DialogoText.text = DialogoLines[LineIndex];
            }
        }
    }
    private void StartDialogo()
    {
       diadDialogoStart = true;
        DialogoUI.SetActive(true);
        DialogoPanel.SetActive(true);
        LineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowDialogoLine());
    }

    private void NextDialogoLine()
    {
        LineIndex++;
        if (LineIndex < DialogoLines.Length)
        {
            StartCoroutine(ShowDialogoLine());
        }
        else
        {
            diadDialogoStart = false;
            DialogoUI.SetActive(true);
            DialogoPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    private IEnumerator ShowDialogoLine()
    {
        DialogoText.text = string.Empty;
        foreach(char ch in DialogoLines[LineIndex])
        {
            DialogoText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            DialogoUI.SetActive(true);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            DialogoUI.SetActive(false);
            
        }
        
    }
    
}
