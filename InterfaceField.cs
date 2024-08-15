using UnityEngine;

[System.Serializable]
public class InterfaceField<T> where T : class
{
    [SerializeField]
    private Object assignedObject;

    private T backingField;

    public T Value
    {
        get
        {
            if (backingField == null)
            {
                backingField = assignedObject as T;
            }
            return backingField;
        }
    }
}
