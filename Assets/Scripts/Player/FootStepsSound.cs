using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsSound : MonoBehaviour
{
    private AudioSource footStepAS;
    [SerializeField]
    private AudioClip[] footClip;

    [HideInInspector]
    public float Accumulated_Distance, StepDistance;
    [HideInInspector]
    public float MinVolume, MaxVolume ;
    private CharacterController charControl;

    void Awake()
    {
        charControl = GetComponentInParent<CharacterController>();
        footStepAS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckFootSteps();
    }

    void CheckFootSteps()
    {
        if (!charControl.isGrounded)
            return;
        if (charControl.velocity.sqrMagnitude > 0f)
        {
            Accumulated_Distance += Time.deltaTime;
            if (Accumulated_Distance > StepDistance)
            {
                footStepAS.volume = Random.Range(MinVolume, MaxVolume);
                footStepAS.PlayOneShot(footClip[Random.Range(0, footClip.Length)]);
                Accumulated_Distance = 0f;
            }
        }
        else { Accumulated_Distance = 0f; }
    }
}
