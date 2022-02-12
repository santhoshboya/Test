using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Student student;
    Student studentRef;
    void Start()
    {
        studentRef = Instantiate<Student>(student);
        studentRef.studentElements.studentDetailsAndOptions.SetStudentName("Test Player 1");
    }
}
