using System;

public static class InputManager
{
    public static InputType currentInputType;

    public static Action<InputType> ChangeInputTypeEvent;
    
    public static void ChangeInputType()
    {
        currentInputType = currentInputType == InputType.Rotate ? InputType.Move : InputType.Rotate;

        ChangeInputTypeEvent?.Invoke(currentInputType);
    }
}

public enum InputType
{
    Rotate,
    Move
}
