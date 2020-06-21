using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputDictionary
{
    #region MouseAndKeyboard
    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";
    public const string Jump = "Jump";
    public const KeyCode Zoom = KeyCode.F;

    public const int MouseLeftClick = 0;
    public const int MouseRightClick = 1;
    #endregion

    #region Xbox
    public const string XboxLeftJoystickHorizontal = "XboxLeftJoystickHorizontal";
    public const string XboxLeftJoystickVertical = "XboxLeftJoystickVertical";

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
    public static string XboxRightJoystickHorizontal = "XboxRightJoystickHorizontal";
    public static string XboxRightJoystickVertical = "XboxRightJoystickVertical";
    public static string XboxLeftTrigger = "XboxLeftTrigger";
    public static string XboxRightTrigger = "XboxRightTrigger";
    public static KeyCode XboxAButton = KeyCode.Joystick1Button0;
    public static KeyCode XboxBButton = KeyCode.Joystick1Button1;
    public static KeyCode XboxXButton = KeyCode.Joystick1Button2;
    public static KeyCode XboxYButton = KeyCode.Joystick1Button3;
    public static KeyCode XboxLeftBumper = KeyCode.Joystick1Button4;
    public static KeyCode XboxRightBumper = KeyCode.Joystick1Button5;
    public static KeyCode XboxBackButton = KeyCode.Joystick1Button6;
    public static KeyCode XboxStartButton = KeyCode.Joystick1Button7;
    public static KeyCode XboxLeftStickClick = KeyCode.Joystick1Button8;
    public static KeyCode XboxRightStickClick = KeyCode.Joystick1Button9;
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        public static string XboxRightJoystickHorizontal = "XboxRightJoystickHorizontalMAC";
        public static string XboxRightJoystickVertical = "XboxRightJoystickVerticalMAC";
        public static string XboxLeftTrigger = "XboxLeftTriggerMAC";
        public static string XboxRightTrigger = "XboxRightTriggerMAC";
        public static KeyCode XboxAButton = KeyCode.Joystick1Button16;
        public static KeyCode XboxBButton = KeyCode.Joystick1Button17;
        public static KeyCode XboxXButton = KeyCode.Joystick1Button18;
        public static KeyCode XboxYButton = KeyCode.Joystick1Button19;
        public static KeyCode XboxLeftBumper = KeyCode.Joystick1Button13;
        public static KeyCode XboxRightBumper = KeyCode.Joystick1Button14;
        public static KeyCode XboxBackButton = KeyCode.Joystick1Button10;
        public static KeyCode XboxStartButton = KeyCode.Joystick1Button9;
        public static KeyCode XboxLeftStickClick = KeyCode.Joystick1Button11;
        public static KeyCode XboxRightStickClick = KeyCode.Joystick1Button12;
#endif
    #endregion
}
