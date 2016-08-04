﻿using SPAD.neXt.Interfaces.Base;
using SPAD.neXt.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPAD.neXt.Interfaces.Events
{
    public delegate void SPADEventHandler(object sender, ISPADEventArgs e);
   

    public interface IEventManager
    {

        void SubscribeToSystemEvent(string eventName, SPADEventHandler newDelegate);
        //void Subscribe(string eventName, IEventHandler eventDelegate, ISPADComparator comparator);
        //void Unsubscribe(IMonitorableValue monitorableValue, string eventName, IEventHandler eventDelegate);
        event PropertyChangedEventHandler AircraftChanged;

        void Raise(object sender, string eventname, ISPADEventArgs e);

        void RegisterTag(string tag, IMonitorableValue monitorableValue);
        void UnregisterTag(string tag);
        IMonitorableValue GetValueByTag(string tag);
        void ForceUpdate(string tag);

        bool IsRegisteredVariable(string tag);
        IMonitorableValue GetRegisteredVariable(string tag);
        IMonitorableValue CreateMonitorableValue(string Name, VARIABLE_SCOPE scope = VARIABLE_SCOPE.SCOPE_SESSION, double defaultValue = 0);
    }

    public interface IEventHandler
    {
        void HandleEvent(ISPADEventArgs e);
    }

    public interface IObserverTicket
    {
        void Dispose();
    }

    public interface IValueProvider
    {
        object GetValue(IMonitorableValue value);
        void SetValue(IMonitorableValue value, Guid sender, int delay = 0);
        void ForceUpdate(IMonitorableValue value);

        void StartMonitoring(IMonitorableValue value);
        void StopMonitoring(IMonitorableValue value);
        bool IsMonitoring(IMonitorableValue value);

        bool IsInitialized { get; }
        bool IsPaused { get; }
        
        void Initialize();
        void Pause();
        void Continue();

        void SendControl(UInt32 control, UInt32 parameter);
        void SendControl(IDataDefinition control, UInt32 parameter);
        void EventCallback(object callbackvalue);
        IDataDefinition CreateDynamic(string name, string normalizer = null, VARIABLE_SCOPE scope = VARIABLE_SCOPE.SCOPE_SESSION, double defaultValue = 0);


    }

    public interface ISimulationController
    {
        void InitController();
        void StartProcessing();
        void StopProcessing();
        void PauseProcessing();
        void ContinueProcessing();

        bool IsConnected { get; }
        bool HasConnectionStatusChanged { get; }
    }

    public interface IMonitorableValue : IDisposable
    {
        Guid ID { get; }
        Guid Owner { get; set; }
        string Name { get; }
        string InternalName { get; set; }
        object CurrentValue { get; }
        object PreviousValue { get; }
        double MinimumChange { get; }
        bool IsDisposed { get; }
        VARIABLE_SCOPE Scope { get; set; }
        ValueDataTypes ValueDataType { get; }
        IValueProvider ValueProvider { get; }
        IValueNormalizer ValueNormalizer { get; }
        IDataDefinition DataDefinition { get; }
        event EventHandler<BooleanEventArgs> MonitoredChanged;

        void SetRawValue(object newValue);
        object GetRawValue(bool secondary = false);
        string GetValueString();

        bool AsBool { get; set; }
        byte AsByte { get; set; }
        double AsDouble { get; set; }
        short AsInt16 { get; set; }
        int AsInt32 { get; set; }
        long AsInt64 { get; set; }
        sbyte AsSByte { get; set; }
        float AsSingle { get; set; }
        string AsString { get; set; }
        ushort AsUInt16 { get; set; }
        uint AsUInt32 { get; set; }
        ulong AsUInt64 { get; set; }


        bool HasChanged();

        void SetValue(object newValue, int delay = 0);
        Double ChangeValue(Double valChange);

        void StartMonitoring();
        void StopMonitoring();
        void ForceUpdate();

        bool IsActive { get; }
        bool IsUndefined();

        IObserverTicket Subscribe(string subscriptionID,string eventName, ISPADEventDelegate eventDelegate, int priority = 0);

        void Raise(string eventName, object sender, ISPADEventArgs eventArgs);

        void SetPassive(); // This Monitorable will neever raise an event
        bool IsPassive { get; }
        bool NeedEvent { get; }
    }

    public enum ValueDataTypes
    {
        Unkown,
        Byte,
        Int16,
        Int32,
        Int64,
        Double,
        Single,
        ByteArray,
        UInt16,
        UInt32,
        UInt64,
        String,
        BitArray,
        SByte,
        Bit
    }

}