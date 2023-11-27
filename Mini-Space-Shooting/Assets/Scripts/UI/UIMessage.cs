using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UIMessage : MonoBehaviour
{
    private Text m_UI_Message_Text;
    private Animator m_UI_Message_Animator;
    private Coroutine m_Coroutine;
   
    private void OnEnable() => StartRoutine();

    private void Awake()
    {
        m_UI_Message_Animator = GetComponent<Animator>();
        m_UI_Message_Text = GetComponent<Text>();
    }
    private void StartRoutine()
    {
        if (m_Coroutine != null)
            StopCoroutine(m_Coroutine);
        m_Coroutine = StartCoroutine(DisableAfterMessage());
        m_UI_Message_Animator.Play("ZoomIn", -1, 0f);
    }
    public void SetMessage(string message, Color color)
    {
        this.gameObject.SetActive(true);
        m_UI_Message_Text.text = message;
        m_UI_Message_Text.color = color;
        if (isActiveAndEnabled)
            StartRoutine();
    }

    private IEnumerator DisableAfterMessage()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
