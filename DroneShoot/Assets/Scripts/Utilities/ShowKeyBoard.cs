using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
public class ShowKeyBoard : MonoBehaviour
{
    private TMP_InputField tMP_InputField;
    // Start is called before the first frame update
    void Start()
    {
        tMP_InputField = GetComponent<TMP_InputField>();
        tMP_InputField.onSelect.AddListener(x => OpenKeyBoard());
    }

    public void OpenKeyBoard(){
        NonNativeKeyboard.Instance.InputField = tMP_InputField;
        NonNativeKeyboard.Instance.PresentKeyboard(tMP_InputField.text);
    }
}
