using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TurnRestriction : MonoBehaviour
{
    public SnapTurnProviderBase snapTurnProvider; // Reference to the Snap Turn Provider
    public float rotationLimit = 90f; // Max rotation limit to each side (90° for 180° total)

    private Quaternion initialRotation; // Initial forward direction when starting
    private Transform playerTransform;

    private void Start()
    {
        // Capture the initial forward-facing rotation and the player transform
        initialRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        playerTransform = transform;

        // If Snap Turn Provider is not assigned, get it automatically
        if (snapTurnProvider == null)
        {
            snapTurnProvider = GetComponent<SnapTurnProviderBase>();
        }
    }

    private void Update()
    {
        // Get the current angle difference relative to the initial direction
        float currentAngleDifference = GetAngleDifference();

        // Calculate the intended turn direction
        float intendedTurnAmount = snapTurnProvider.turnAmount;

        // Check if a snap turn attempt would exceed the rotation limit
        if (intendedTurnAmount != 0)
        {
            float nextAngleDifference = currentAngleDifference + intendedTurnAmount;

            // Allow the turn only if it stays within the 180° range
            if (Mathf.Abs(nextAngleDifference) <= rotationLimit)
            {
                // Apply the turn by rotating the player directly
                playerTransform.Rotate(Vector3.up, intendedTurnAmount);
            }
            // If the turn exceeds the limit, skip the turn by not rotating
        }
    }

    private float GetAngleDifference()
    {
        // Calculate the signed angle difference between the current and initial rotation
        float angleDifference = Vector3.SignedAngle(initialRotation * Vector3.forward, playerTransform.forward, Vector3.up);
        return angleDifference;
    }
}
