using TMPro;


public class FinalScore : ManagedBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void ShowFinalScore(int count)
    {
        _text.text = count.ToString();
    }
}