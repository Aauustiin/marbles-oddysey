using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class LevelController : MonoBehaviour
{
    [SerializeField] float MAX_TILT_ANGLE = 45f;

    public void OnTilt(InputAction.CallbackContext value)
    {
        Vector2 rawMousePos = value.ReadValue<Vector2>();
        Vector2 relativeMousePos = new Vector2(rawMousePos.x - Screen.width / 2, rawMousePos.y - Screen.height / 2); // Relative to the center of the screen.
        Vector2 normalizedMousePos = new Vector2(relativeMousePos.x / (Screen.width / 2), relativeMousePos.y / (Screen.height / 2)); // Bottom left corresponds to (-1, -1), top right would be (1, 1).
        Vector2 boundedMousePos = new Vector2(Mathf.Abs(normalizedMousePos.x) > 1 ? (normalizedMousePos.x / Mathf.Abs(normalizedMousePos.x)) : normalizedMousePos.x,
            Mathf.Abs(normalizedMousePos.y) > 1 ? (normalizedMousePos.y / Mathf.Abs(normalizedMousePos.y)) : normalizedMousePos.y); // Bounded to the edges of the window.
        Vector3 rotation = new Vector3(MAX_TILT_ANGLE * boundedMousePos.y, 0, -(MAX_TILT_ANGLE * boundedMousePos.x));
        transform.rotation = Quaternion.Euler(rotation);
    }
}
