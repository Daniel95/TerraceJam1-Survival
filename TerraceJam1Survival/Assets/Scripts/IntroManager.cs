using Cinemachine;
using System;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public static Action IntroCompleteEvent;

    [SerializeField] private GameObject textField2;
    [SerializeField] private GameObject textField3;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera1;

    int mouseCounter;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(mouseCounter == 0)
            {
                textField2.SetActive(true);
            } 
            else if (mouseCounter == 1)
            {
                textField3.SetActive(true);
            }
            else if(mouseCounter == 2)
            {
                cinemachineVirtualCamera1.Priority = 0;

                if(IntroCompleteEvent != null)
                {
                    IntroCompleteEvent();
                }
                     
                gameObject.SetActive(false);
            }



            mouseCounter++;
        }
    }
}
