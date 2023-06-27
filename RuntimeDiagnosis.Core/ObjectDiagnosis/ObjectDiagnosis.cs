using System.Diagnostics;
using System.Runtime.CompilerServices;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.Kit;
using RuntimeDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis;
 
[DebuggerDisplay("{ToString()}")]
public class ObjectDiagnosis<TOwnerType> : IObjectDiagnosis<TOwnerType>
    where TOwnerType : IDiagnosableObject
{
    private readonly Action<string> _invokeOwnerPropertyChanged;

    IDiagnosableObject IObjectDiagnosis.Owner => Owner;
    
    public TOwnerType Owner { get; }

    public Type OwnerBaseType => Owner.GetType().BaseType ?? Owner.GetType();

    [DebuggerDisplay($"{nameof(MemberDiagnoses)} for {{GetOwnerTypeString()}}")]
    public IEnumerable<IMemberDiagnosis> MemberDiagnoses { get; internal set; } = null!;

    internal ObjectDiagnosis(TOwnerType owner, Action<string> invokePropertyChanged)
    {
        _invokeOwnerPropertyChanged = invokePropertyChanged;
        Owner = owner;
    }

    public override string ToString() =>
        $"{typeof(ObjectDiagnosis<TOwnerType>).GetNameWithoutGenericArity()} for {GetOwnerTypeString()}";

    public string GetOwnerTypeString() =>
        $"{OwnerBaseType.Name} object";

    public IMemberDiagnosis? GetMemberDiagnose([CallerMemberName] string memberName = "") =>
        MemberDiagnoses.FirstOrDefault(md => md.MemberName == memberName);

    IMemberDiagnosis<TMemberValueType?>? IObjectDiagnosis.GetMemberDiagnose<TMemberValueType>(
        string memberName) where TMemberValueType : default =>
        GetMemberDiagnose(memberName) as IMemberDiagnosis<TMemberValueType?>;

    public IMemberDiagnosis<TOwnerType, TMemberValueType?>? GetMemberDiagnose<TMemberValueType>(
        [CallerMemberName] string memberName = "") =>
        GetMemberDiagnose(memberName) as MemberDiagnosis<TOwnerType, TMemberValueType?>;
    
    public TMemberValueType? GetCurrentOutputMemberValue<TMemberValueType>(
        in Func<TMemberValueType?> getMemberValue,
        [CallerMemberName] string memberName = "")
    {
        var memberDiagnose = GetMemberDiagnose<TMemberValueType>(memberName);
        return memberDiagnose == null ? getMemberValue() : memberDiagnose.OutputValue.Value;
    }
    
    public void SetOriginalInputMemberValue<TMemberValueType>(in Action<TMemberValueType?> setMemberValue, 
        in TMemberValueType? value, [CallerMemberName] string memberName = "")
    {
        var memberDiagnose = GetMemberDiagnose<TMemberValueType?>(memberName);
        if (memberDiagnose == null)
        {
            setMemberValue(value);
            return;
        }
        memberDiagnose.InputValue.Value = value;
    }

    public IMemberDiagnosis CreateMemberDiagnosis<TMemberValueType>(
        in string memberName, 
        IEnumerable<DirectionValueDefinition> inputCallerDefinitions, 
        IEnumerable<DirectionValueDefinition> outputCallerDefinitions,
        Func<TMemberValueType?> getMemberValue,
        Action<TMemberValueType?> setMemberValue) =>
        new MemberDiagnosis<TOwnerType, TMemberValueType>(
                this, memberName, inputCallerDefinitions, outputCallerDefinitions,
                InvokeOwnerPropertyChanged, getMemberValue, setMemberValue);

    private void InvokeOwnerPropertyChanged(IMemberDiagnosis memberDiagnosis) =>
        _invokeOwnerPropertyChanged(memberDiagnosis.MemberName);
}