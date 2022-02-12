using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentDetailsAndOptions : MonoBehaviour
{

    [Serializable]
    public struct StudentDetailsElements
    {
        public TMPro.TextMeshProUGUI StudentName;
        public GameObject StudentOptions;
    }

    public StudentDetailsElements studentDetailsElementsRef;


    public struct StudentData
    {
        public string studentName;
    }

    public StudentData studentData;

    
    void Update()
    {
        SetStudentElementsData();
    }

    public void SetStudentElementsData()
    {
        studentDetailsElementsRef.StudentName.text = studentData.studentName;
    }

    public void SetStudentName(string name)
    {
        studentData.studentName = name;
    }

    public void OnClickVoiceButton()
    {
        Debug.Log("We Can Talk Now");
    }

    public void OnClickChatButton()
    {
        Debug.Log("We Can Chat Now");
    }
}
