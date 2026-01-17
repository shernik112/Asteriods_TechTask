using System.Collections.Generic;

public class ReferenseSetToggle 
{
    private HashSet<object> references = new HashSet<object>();


    private bool previousState = false;

    public bool True
    {
        get
        {
            references.RemoveWhere(x => x == null);

            bool current = references.Count > 0;
            if (current != previousState)
            {
                previousState = current;
            }
            return current;
        }
    }

    public void Add(object obj)
    {
        if (obj == null) return;
        references.Add(obj);
    }

    public void Remove(object obj)
    {
        if (obj == null) return;
        references.Remove(obj);
    }
    
}