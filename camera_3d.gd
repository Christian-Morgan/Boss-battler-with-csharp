extends Node3D

@export var camera_target : Node3D
@export var pitch_max = 50
@export var pitch_min = -50
var yaw = float()
var pitch = float()
var yaw_sensitivity = .002
var pitch_sensitivity = .002
