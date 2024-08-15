using System.ComponentModel;
using UnityEngine;

public static class RaycastExtensions
{
    /// <summary>
    /// Cast a raycast from under cursor, and obtain a component in parent, if anything was hit
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static T GetComponentUnderCursor<T>(Vector3 screenPos, LayerMask layerMask, QueryTriggerInteraction triggerIntraction = QueryTriggerInteraction.UseGlobal) where T : UnityEngine.Component
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(screenPos), out var hit, Mathf.Infinity, layerMask))
        {
            return hit.collider.GetComponentInParent<T>();
        }
        return default(T);
    }
}