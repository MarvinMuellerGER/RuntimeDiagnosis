using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;
using RuntimeDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis;

// TODO: Implement Unit Tests
[DebuggerDisplay("{ToString()}")]
public sealed class ObjectDiagnosis<TOwnerType> : IObjectDiagnosisInternal<TOwnerType>
    where TOwnerType : IDiagnosableObjectInternal<TOwnerType>
{
    private readonly IObjectDiagnosesManagerInternal _objectDiagnosesManager;
    private Action<string> _invokeOwnerPropertyChanged = null!;

    Action<string> IObjectDiagnosis.InvokeOwnerPropertyChanged => _invokeOwnerPropertyChanged;

    IDiagnosableObject IObjectDiagnosis.Owner => Owner;
    
    public TOwnerType Owner { get; private set; } = default!;

    public Type OwnerBaseType { get; private set; } = typeof(TOwnerType);

    [DebuggerDisplay($"{nameof(MemberDiagnoses)} for {{GetOwnerTypeString()}}")]
    public IEnumerable<IMemberDiagnosis> MemberDiagnoses { get; private set; } = null!;
    
    public ObjectDiagnosis(IObjectDiagnosesManagerInternal objectDiagnosesManager) => 
        _objectDiagnosesManager = objectDiagnosesManager;


    public void Initialize(TOwnerType owner)
    {
        Owner = owner;
        SetOwnerBaseType();
        MemberDiagnoses = owner.CreateMemberDiagnosisActions
            .Select(createMemberDiagnose => createMemberDiagnose(this)).ToList();
        _invokeOwnerPropertyChanged = owner.InvokePropertyChanged;
        _objectDiagnosesManager.AddNewObjectDiagnose(this);
    }

    public override string ToString() =>
        $"{this.GetTypeNameWithoutGenericArity()} for {GetOwnerTypeString()}";

    public string GetOwnerTypeString() =>
        $"{OwnerBaseType.Name} object";

    public IMemberDiagnosis? GetMemberDiagnose([CallerMemberName] string memberName = "") =>
        MemberDiagnoses.FirstOrDefault(md => md.MemberName == memberName);

    IMemberDiagnosis<TMemberValueType?>? IObjectDiagnosis.GetMemberDiagnose<TMemberValueType>(
        string memberName) where TMemberValueType : default =>
        GetMemberDiagnose(memberName) as IMemberDiagnosis<TMemberValueType?>;

    public IMemberDiagnosis<TOwnerType, TMemberValueType?>? GetMemberDiagnose<TMemberValueType>(
        [CallerMemberName] string memberName = "") =>
        GetMemberDiagnose(memberName) as IMemberDiagnosis<TOwnerType, TMemberValueType?>;

    IMemberDiagnosis IObjectDiagnosisInternal.CreateMemberDiagnosis<TMemberValueType>(
        in string memberName,
        Expression<Func<TMemberValueType?>> memberExpression, 
        IEnumerable<IDirectionValueDefinition> inputCallerDefinitions, 
        IEnumerable<IDirectionValueDefinition> outputCallerDefinitions) where TMemberValueType : default
    {
        var memberDiagnosis = ServiceProvider.Instance.GetMemberDiagnosis<TOwnerType, TMemberValueType>();
        memberDiagnosis.Initialize(
            this, memberName, memberExpression, inputCallerDefinitions, outputCallerDefinitions);
        return memberDiagnosis;
    }

    TMemberValueType? IObjectDiagnosisInternal.GetMemberValue<TMemberValueType>(string memberName)
        where TMemberValueType : default =>
        GetMemberDiagnose<TMemberValueType>(memberName)!.OutputValueDiagnosis.Value;
    
    void IObjectDiagnosisInternal.SetMemberValue<TMemberValueType>(in TMemberValueType? value, string memberName)
        where TMemberValueType : default =>
        GetMemberDiagnose<TMemberValueType?>(memberName)!.InputValueDiagnosis.Value = value;

    private void SetOwnerBaseType() =>
        OwnerBaseType = typeof(TOwnerType).BaseType == typeof(object)
            ? typeof(TOwnerType).GetInterfaces()[1]
            : typeof(TOwnerType).BaseType ?? typeof(TOwnerType);
}