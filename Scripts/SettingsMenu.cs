using Godot;
using System;

public partial class SettingsMenu : Control
{
    // References to UI elements
    private HSlider sfxSlider;
    private HSlider bgmSlider;
    private CheckBox cooldownCheckBox;

    public override void _Ready()
    {
        // Get references to the sliders and checkbox
        sfxSlider = GetNode<HSlider>("VBoxContainer/HBoxContainer/SFXSlider");
        bgmSlider = GetNode<HSlider>("VBoxContainer/HBoxContainer2/MusicSlider");
        cooldownCheckBox = GetNode<CheckBox>("VBoxContainer/HBoxContainer3/ShowCooldownCheckBox");

        // Access CooldownManager as a singleton instance
        CooldownManager cooldownManager = GetNode<CooldownManager>("/root/CooldownManager");

        // Set the checkbox state based on cooldown visibility
        cooldownCheckBox.ButtonPressed = cooldownManager.AreCooldownsVisible();

        // Connect signals using the new syntax
        sfxSlider.ValueChanged += OnSFXVolumeChanged;
        bgmSlider.ValueChanged += OnBGMVolumeChanged;
        cooldownCheckBox.Toggled += OnCooldownToggle;
    }

    private void OnSFXVolumeChanged(double value)
    {
        // Set the volume for the SFX audio bus
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("SFX"), (float)value);
    }

    private void OnBGMVolumeChanged(double value)
    {
        // Set the volume for the BGM audio bus
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("BGM"), (float)value);
    }

    private void OnCooldownToggle(bool pressed)
    {
        // Access the CooldownManager singleton and update cooldown visibility
        CooldownManager cooldownManager = GetNode<CooldownManager>("/root/CooldownManager");
        cooldownManager.SetCooldownVisibility(pressed);
    }



}
