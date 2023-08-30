using Jab;
using RuntimeDiagnosis.Core.ObjectDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.Kit;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.TrackableValue;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.Kit;

namespace RuntimeDiagnosis.Core;

// TODO: Improvement to Jab: Add possibility to register Types with Generic Type Attributes without registering these Generic Types also
[ServiceProvider]
[Transient(typeof(IObjectDiagnosis), Factory = nameof(_objectDiagnosisFactory))]
[Transient(typeof(IObjectRegistry), Factory = nameof(_objectRegistryFactory))]
[Singleton(typeof(IObjectDiagnosesManagerInternal), Factory = nameof(CreateObjectDiagnosesManagerInstance))]
[Transient(typeof(IMemberDiagnosis), Factory = nameof(_memberDiagnosisFactory))]
[Transient(typeof(IMemberAccessor), Factory = nameof(_memberAccessorFactory))]
[Transient(typeof(IInputValueDiagnosis), Factory = nameof(_inputValueDiagnosisFactory))]
[Transient(typeof(IOutputValueDiagnosis), Factory = nameof(_outputValueDiagnosisFactory))]
[Transient(typeof(ITrackableValue), Factory = nameof(_trackableValueFactory))]
[Transient(typeof(ITrackableValueAlwaysEditable), Factory = nameof(_trackableValueAlwaysEditableFactory))]
[Transient(typeof(ITrackableValueEditable), Factory = nameof(_trackableValueEditableFactory))]
[Singleton<IDirectionValueDiagnosesFinder, DirectionValueDiagnosesFinder>]
public sealed partial class ServiceProvider
{
    private readonly object _lockCreateObjectDiagnosisInstance = new();
    private delegate IObjectDiagnosis ObjectDiagnosisFactoryDelegate();
    private ObjectDiagnosisFactoryDelegate? _objectDiagnosisFactory;
    
    private readonly object _lockCreateObjectRegistryInstance = new();
    private delegate IObjectRegistry ObjectRegistryFactoryDelegate();
    private ObjectRegistryFactoryDelegate? _objectRegistryFactory;
    
    private readonly object _lockCreateMemberDiagnosisInstance = new();
    private delegate IMemberDiagnosis MemberDiagnosisFactoryDelegate();
    private MemberDiagnosisFactoryDelegate? _memberDiagnosisFactory;
    
    private readonly object _lockCreateMemberAccessorInstance = new();
    private delegate IMemberAccessor MemberAccessorFactoryDelegate();
    private MemberAccessorFactoryDelegate? _memberAccessorFactory;
    
    private readonly object _lockCreateInputValueDiagnosisInstance = new();
    private delegate IInputValueDiagnosis InputValueDiagnosisFactoryDelegate();
    private InputValueDiagnosisFactoryDelegate? _inputValueDiagnosisFactory;
    
    private readonly object _lockCreateOutputValueDiagnosisInstance = new();
    private delegate IOutputValueDiagnosis OutputValueDiagnosisFactoryDelegate();
    private OutputValueDiagnosisFactoryDelegate? _outputValueDiagnosisFactory;
    
    private readonly object _lockCreateTrackableValueInstance = new();
    private delegate ITrackableValue TrackableValueFactoryDelegate();
    private TrackableValueFactoryDelegate? _trackableValueFactory;
    
    private readonly object _lockCreateTrackableValueAlwaysEditableInstance = new();
    private delegate ITrackableValueAlwaysEditable TrackableValueAlwaysEditableFactoryDelegate();
    private TrackableValueAlwaysEditableFactoryDelegate? _trackableValueAlwaysEditableFactory;
    
    private readonly object _lockCreateTrackableValueEditableInstance = new();
    private delegate ITrackableValueEditable TrackableValueEditableFactoryDelegate();
    private TrackableValueEditableFactoryDelegate? _trackableValueEditableFactory;

    public static readonly ServiceProvider Instance = new();
    
    public IObjectDiagnosisInternal<TOwnerType> GetObjectDiagnosis<TOwnerType>()
        where TOwnerType : IDiagnosableObjectInternal<TOwnerType>
    {
        lock (_lockCreateObjectDiagnosisInstance)
        {
            _objectDiagnosisFactory = () => 
                new ObjectDiagnosis<TOwnerType>(GetService<IObjectDiagnosesManagerInternal>());
            return (IObjectDiagnosisInternal<TOwnerType>)GetService<IObjectDiagnosis>();
        }
    }
    
    public IObjectRegistryInternal<T> GetObjectRegistry<T>()
    {
        lock (_lockCreateObjectRegistryInstance)
        {
            _objectRegistryFactory = () => 
                new ObjectRegistry<T>();
            return (IObjectRegistryInternal<T>)GetService<IObjectRegistry>();
        }
    }

    private IObjectDiagnosesManagerInternal CreateObjectDiagnosesManagerInstance() =>
        new ObjectDiagnosesManager(GetObjectRegistry<IObjectDiagnosis>());
    
    public IMemberDiagnosisInternal<TOwnerType, TMemberValueType> GetMemberDiagnosis<TOwnerType, TMemberValueType>()
        where TOwnerType : IDiagnosableObject
    {
        lock (_lockCreateMemberDiagnosisInstance)
        {
            _memberDiagnosisFactory = () => new MemberDiagnosis<TOwnerType, TMemberValueType>(
                GetMemberAccessor<TMemberValueType>(), 
                GetInputValueDiagnosis<TOwnerType, TMemberValueType>(), 
                GetOutputValueDiagnosis<TOwnerType, TMemberValueType>());
            return (IMemberDiagnosisInternal<TOwnerType, TMemberValueType>)GetService<IMemberDiagnosis>();
        }
    }
    
    public IMemberAccessorInternal<T?> GetMemberAccessor<T>()
    {
        lock (_lockCreateMemberAccessorInstance)
        {
            _memberAccessorFactory = () => new MemberAccessor<T?>();
            return (IMemberAccessorInternal<T?>)GetService<IMemberAccessor>();
        }
    }
    
    public IInputValueDiagnosisInternal<TOwnerType, TMemberValueType?>
        GetInputValueDiagnosis<TOwnerType, TMemberValueType>() where TOwnerType : IDiagnosableObject
    {
        lock (_lockCreateInputValueDiagnosisInstance)
        {
            _inputValueDiagnosisFactory = () => 
                new InputValueDiagnosis<TOwnerType, TMemberValueType>(
                    GetService<IObjectDiagnosesManagerInternal>(),
                    GetService<IDirectionValueDiagnosesFinder>(),
                    GetTrackableValueAlwaysEditable<TOwnerType, TMemberValueType?, bool>(),
                    GetTrackableValueEditable<TOwnerType, TMemberValueType?, TMemberValueType?>(),
                    GetTrackableValue<TOwnerType, TMemberValueType?, TMemberValueType?>(),
                    GetTrackableValue<TOwnerType, TMemberValueType?, TMemberValueType?>());
            return (IInputValueDiagnosisInternal<TOwnerType, TMemberValueType?>)GetService<IInputValueDiagnosis>();
        }
    }
    
    public IOutputValueDiagnosisInternal<TOwnerType, TMemberValueType?>
        GetOutputValueDiagnosis<TOwnerType, TMemberValueType>() where TOwnerType : IDiagnosableObject
    {
        lock (_lockCreateOutputValueDiagnosisInstance)
        {
            _outputValueDiagnosisFactory = () => 
                new OutputValueDiagnosis<TOwnerType, TMemberValueType>(
                    GetService<IObjectDiagnosesManagerInternal>(),
                    GetService<IDirectionValueDiagnosesFinder>(),
                    GetTrackableValueAlwaysEditable<TOwnerType, TMemberValueType?, bool>(),
                    GetTrackableValueEditable<TOwnerType, TMemberValueType?, TMemberValueType?>(),
                    GetTrackableValue<TOwnerType, TMemberValueType?, TMemberValueType?>(),
                    GetTrackableValue<TOwnerType, TMemberValueType?, TMemberValueType?>());
            return (IOutputValueDiagnosisInternal<TOwnerType, TMemberValueType?>)GetService<IOutputValueDiagnosis>();
        }
    }
    
    public ITrackableValueInternal<TOwnerType, TMemberValueType, TValueType>
        GetTrackableValue<TOwnerType, TMemberValueType, TValueType>()
        where TOwnerType : IDiagnosableObject
    {
        lock (_lockCreateTrackableValueInstance)
        {
            _trackableValueFactory = () => new TrackableValue<TOwnerType, TMemberValueType, TValueType>();
            return (ITrackableValueInternal<TOwnerType, TMemberValueType, TValueType>)GetService<ITrackableValue>();
        }
    }
    
    public ITrackableValueAlwaysEditableInternal<TOwnerType, TMemberValueType, TValueType>
        GetTrackableValueAlwaysEditable<TOwnerType, TMemberValueType, TValueType>()
        where TOwnerType : IDiagnosableObject
    {
        lock (_lockCreateTrackableValueAlwaysEditableInstance)
        {
            _trackableValueAlwaysEditableFactory = () => 
                new TrackableValueAlwaysEditable<TOwnerType, TMemberValueType, TValueType>();
            return (ITrackableValueAlwaysEditableInternal<TOwnerType, TMemberValueType, TValueType>)
                GetService<ITrackableValueAlwaysEditable>();
        }
    }
    
    public ITrackableValueEditableInternal<TOwnerType, TMemberValueType, TValueType>
        GetTrackableValueEditable<TOwnerType, TMemberValueType, TValueType>()
        where TOwnerType : IDiagnosableObject
    {
        lock (_lockCreateTrackableValueEditableInstance)
        {
            _trackableValueEditableFactory = () => 
                new TrackableValueEditable<TOwnerType, TMemberValueType, TValueType>();
            return (ITrackableValueEditableInternal<TOwnerType, TMemberValueType, TValueType>)
                GetService<ITrackableValueEditable>();
        }
    }
}