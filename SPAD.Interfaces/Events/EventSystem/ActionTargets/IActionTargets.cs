﻿using SPAD.neXt.Interfaces;
using SPAD.neXt.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPAD.neXt.Interfaces.Events
{
    public interface IEventActionRangedAxis : IEventAction
    {
        bool InvertAxis { get; set; }
        bool SendRaw { get; set; }
        bool Option1Selected { get; set; }
        bool Option2Selected { get; set; }
        bool Option3Selected { get; set; }
        bool Option4Selected { get; set; }
    }

    public interface IEventActionCustomAxis : IEventActionChangeValue
    {
        bool AlwaysActive { get; set; }
        SPADEventValueComparator Comparator { get; }
        bool InvertAxis { get; set; }
        bool OneTime { get; set; }
        int RangeFrom { get; set; }
        int RangeTo { get; set; }
        float MapFrom { get; set; }
        float MapTo { get; set; }
        bool UseMapping { get; set; }
        bool SendRaw { get; set; }
    }

    public interface IEventActionMonitor
    {
        IDataDefinition TargetDataDefinition { get; set; }
        IDataDefinition SourceDataDefinition { get; set; }
        string TargetDataDefinitionID { get; set; }
        string SourceDataDefinitionID { get; set; }
        IReadOnlyList<IDataDefinition> MonitoredDataDefinitions { get; }
    }

    public interface IEventActionChangeValue : IEventAction, IEventActionMonitor
    {
        object Value { get; set; }
        SPADValueOperation ValueOperation { get; set; }

        bool UseTriggerValue { get; set; }
        bool HasMinMax { get; set; }
        Double ValueMin { get; set; }
        Double ValueMax { get; set; }
        bool EnableRollOver { get; set; }
    }

    public interface IEventActionSendEvent : IEventActionChangeValue
    {

    }

    public interface IEventActionObserve : IEventAction
    {
        IDataDefinition TargetDataDefinition { get; set; }
        string TargetDataDefinitionID { get; set; }
        IReadOnlyList<IDataDefinition> ObservedDataDefinitions { get; }
    }

    public interface IEventActionSingleton : IEventAction
    {
        Guid SingletonID { get; set; }
    }

    public interface IEventActionCommand : IEventAction
    {
        string CommandParameter { get; set; }
        string CommandName { get; set; }
    }

    public interface IEventActionPlaySound : IEventAction
    {
        SPADSoundOperation SoundOperation { get; set; }
        int Volume { get; set; }
        string SoundName { get; set; }
    }

    public interface IEventActionDisplayValue : IEventActionObserve
    {
        string TargetDisplay { get; set; }
        string DisplayFormat { get; set; }
    }

    public interface IEventActionKeyboard : IEventAction
    {
        IKeyMacro KeyboardMacro { get; }
        SPADKeyboardOption MacroType { get; set; }
        void ConfigureKeyboardAction(IEnumerable<int> keys, int duration, int pause, int repeat);
    }

    public interface IEventActionSwitchWindow : IEventAction
    {
    }

    public interface IEventActionPlateImage : IEventAction
    {
        FLASHMODE FlashMode { get; set; }
        string Image { get; set; }
    }

    public interface IEventActionPlateLabel : IEventActionObserve
    {
        string Text { get; set; }
        string Color { get; set; }
    }

    public interface IEventImageData : IXmlAnyObject
    {
        Guid ID { get; }
        string Name { get; set; }

        byte[] GetImageData();
        void UpdateImage(byte[] data);
    }

    public interface IEventActionVirtualJoyStick : IEventActionChangeValue
    {
        VirtualJoystickAction JoystickAction { get; set; }
        uint JoystickNumber { get; set; }
        uint JoystickTarget { get; set; }
        VirtualJoystickButtonMode JoystickMode { get; set; }
    }

    public interface IKeyMacro
    {
        IList<int> Keys { get; }
        bool IsPause { get; }
        int Duration { get; }
        int Pause { get; }
        int Repeat { get; }
    }

    public interface IKeyboardPlayer
    {
        void Playback(IKeyMacro playback, SPADKeyboardOption option);
    }

    public interface IEventActionJoystick : IEventAction
    {
        int Button { get; set; }
        int Joystick { get; set; }
    }

    public interface IEventActionExternal  : IEventAction, IEventActionMonitor 
    {
        Guid ProviderID { get; set; }
        IActionProvider ActionProvider { get; }

        object PrivateData { get; set; }
        T GetPrivateData<T>() where T : class;
        void SetPrivateData<T>(T data) where T : class;
    }

    public interface IEventActionDelay : IEventAction
    {
        uint Delay { get; set; }
    }

    public interface IEventActionScript : IEventAction
    {
        string ScriptName { get; set; }
        int ScriptArgument { get; set; }
    }
    public interface IEventActionRestCall : IEventAction
    {
        string Text { get; set; }
        string CallMethod { get; set; }
        bool WaitForFinish { get; set; }
    }

    public interface IEventActionText2Speech : IEventAction
    {
        string Text { get; set; }
        string Voice { get; set; }
        bool StopAll { get; set; }
        bool WaitForFinish { get; set; }
        bool UseSSML { get; set; }
        int Volume { get; set; }
        int Rate { get; set; }
    }

    public interface IEventActionDummy : IEventActionSingleton
    {
        IXmlAnyObject Data { get; set; }
    }
}
