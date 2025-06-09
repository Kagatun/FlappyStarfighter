using UnityEngine;

public interface IMovement
{
    void Initialize(Transform transform);
    void UpdateMovement();
    void ResetParameters(Vector2 spawnPosition);
}