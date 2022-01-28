using UnityEngine;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Dropdown))]
public class SaveFullscreenDropdownValue : MonoBehaviour
{
    const string PrefName = "fullscreenValue";

    private TMP_Dropdown _dropdown;


    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();

        _dropdown.onValueChanged.AddListener(new UnityAction<int>(index => {

            PlayerPrefs.SetInt(PrefName, _dropdown.value);
            PlayerPrefs.Save();
        }));
    }


    private void Start()
    {
        _dropdown.value = PlayerPrefs.GetInt(PrefName);
    }

}
