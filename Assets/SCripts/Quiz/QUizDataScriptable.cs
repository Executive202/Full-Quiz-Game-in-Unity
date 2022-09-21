using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData",menuName = "QuestionData")]
public class QUizDataScriptable : ScriptableObject
{
    public List<Question> questions;
}
