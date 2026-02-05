using System;
using TMPro;
using UnityEngine;
using Zenject;

public class GetFinalScore : ManagedBehaviour
{
    protected TMP_Text Text;

    public override void ManagedInintialize()
    {
        Text = GetComponent<TMP_Text>();
    }

    public void ShowFinalScore(int count)
    {
        Text.text = count.ToString();
    }
}