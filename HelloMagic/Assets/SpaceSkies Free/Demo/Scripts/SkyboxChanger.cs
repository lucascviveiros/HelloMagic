using UnityEngine;
using UnityEngine.UI;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] Skyboxes;
    private Dropdown _dropdown;

    public void Awake()
    {
        _dropdown = GetComponent<Dropdown>();
		ChangeSkybox();
    }

    public void ChangeSkybox()
    {
		//RenderSettings.skybox = Skyboxes[_dropdown.value];
		RenderSettings.skybox = Skyboxes[0];
		Debug.Log("skybox: " + _dropdown.value.ToString());
    }
}