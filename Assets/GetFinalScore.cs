using System;
using TMPro;
using UnityEngine;
using Zenject;

public class GetFinalScore : ManagedBehaviour
{
    protected TMP_Text Text;

    private void Awake()
    {
        Text = GetComponent<TMP_Text>();
    }

    public void ShowFinalScore(int count)
    {
        Text.text = count.ToString();
    }
}