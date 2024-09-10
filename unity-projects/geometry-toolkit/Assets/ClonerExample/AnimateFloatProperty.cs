using System;
using System.Reflection;
using UnityEngine;

public class AnimateFloatProperty : MonoBehaviour
{
    public Component Component;
    public string FieldName;

    public float StartValue = 0;
    public float EndValue = 1;
    public float Duration = 10;
    
    
    private float _Started;
    private FieldInfo _FieldInfo;
    private PropertyInfo _PropertyInfo;
    private Type _Type => Component != null ? Component.GetType() : null;

    public void Start()
    {
    }

    public void OnEnable()
    {
        _FieldInfo = _Type.GetField(FieldName);
        if (_FieldInfo == null)
        {
            _PropertyInfo = _Type.GetProperty(FieldName);
            if (_PropertyInfo == null)
            {
                Debug.LogWarning($"No field found by name of {FieldName}");
            }
        }
        _Started = Time.time;
    }

    public void Reset()
    {
        SetValue(StartValue);
    }

    public void SetValue(object value)
    {
        if (_FieldInfo != null)
            _FieldInfo.SetValue(Component, value);
        else if (_PropertyInfo != null)
            _PropertyInfo.SetValue(Component, value);
    }

    public void Update()
    {
        var amount = (Time.time - _Started) % Duration / Duration;
        var value = Mathf.Lerp(StartValue, EndValue, amount);
        SetValue(value);
    }
}