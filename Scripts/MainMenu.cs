using Godot;
using System;
public partial class MainMenu : ColorRect
{
    public override void _Ready()
    {
        GetNode<Button>("CenterContainer/VBoxContainer/Play").Pressed += OnPlayButtonPressed;
        GetNode<Button>("CenterContainer/VBoxContainer/Quit").Pressed += OnQuitButtonPressed;
    }
    private void OnPlayButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Level_1.tscn");
    }
    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }
}