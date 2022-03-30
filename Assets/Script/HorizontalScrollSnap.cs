using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalScrollSnap : MonoBehaviour
{
    [SerializeField] private GameObject scrollbar;
    private float scrollPos = 0;
    private float[] pos;
    private bool runIt = false;
    private float time;
    private Button takeTheBtn;
    private int btnNumber;

    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);

        if (runIt)
        {
            SetScreen(distance, pos, takeTheBtn);
            time += Time.deltaTime;

            if (time > 1f)
            {
                time = 0;
                runIt = false;
            }
        }
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }
    }
    private void SetScreen(float distance, float[] pos, Button btn)       //fucntion responsible for showing the correct slide on pressing menu buttons
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[btnNumber], 1f * Time.deltaTime);
            }
        }
        for (int i = 0; i < btn.transform.parent.transform.childCount; i++)
        {
            btn.transform.name = ".";
        }
    }
    public void WhichBtnClicked(Button btn)                //getting the correct button reference and setting the scroll position based on the given no. to button
    {
        btn.transform.name = "clicked";
        for (int i = 0; i < btn.transform.parent.transform.childCount; i++)
        {
            if (btn.transform.parent.transform.GetChild(i).transform.name == "clicked")
            {
                btnNumber = i;
                takeTheBtn = btn;
                time = 0;
                scrollPos = (pos[btnNumber]);
                runIt = true;
            }
        }
    }
}