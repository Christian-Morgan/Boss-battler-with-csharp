using Godot;
using System;

public partial class SceneLoader : Node
{
    // Optional
  //  [Export] private string _sceneFolder;
    public void ChangeToScene(string sceneName)
    {
        // Optional
        // string f = _sceneFolder == "" ? "" : $"{_sceneFolder}/";
        // Non-optional
        GetTree().ChangeSceneToFile($"res://Scenes/{sceneName}");
    }
}
