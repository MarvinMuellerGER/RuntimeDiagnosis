using System.Diagnostics;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;
using RuntimeDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

[DebuggerDisplay("{ToString()} ({ToCurrentValueString()})")]
public class MemberDiagnosis<TOwnerType, TMemberValueType> : IMemberDiagnosis<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    private readonly Action<IMemberDiagnosis> _invokeOwnerPropertyChanged;
    IObjectDiagnosis IMemberDiagnosis.ObjectDiagnosis => ObjectDiagnosis;

    public IObjectDiagnosis<TOwnerType> ObjectDiagnosis { get; }
    
    public string MemberName { get; }

    IInputValueDiagnosis IMemberDiagnosis.InputValueDiagnosis => InputValueDiagnosis;

    IInputValueDiagnosis<TMemberValueType?> IMemberDiagnosis<TMemberValueType?>.InputValueDiagnosis => InputValueDiagnosis;

    public IInputValueDiagnosis<TOwnerType, TMemberValueType?> InputValueDiagnosis { get; }

    IOutputValueDiagnosis IMemberDiagnosis.OutputValueDiagnosis => OutputValueDiagnosis;

    IOutputValueDiagnosis<TMemberValueType?> IMemberDiagnosis<TMemberValueType?>.OutputValueDiagnosis => OutputValueDiagnosis;

    public IOutputValueDiagnosis<TOwnerType, TMemberValueType?> OutputValueDiagnosis { get; }

    IEnumerable<IDirectionValueDiagnosis> IMemberDiagnosis.DirectionValues => DirectionValues;
    
    IEnumerable<IDirectionValueDiagnosis<TMemberValueType?>> IMemberDiagnosis<TMemberValueType?>.DirectionValues => 
        DirectionValues;

    public IEnumerable<IDirectionValueDiagnosis<TOwnerType, TMemberValueType?>> DirectionValues { get; }

    public MemberDiagnosis(in IObjectDiagnosis<TOwnerType> objectDiagnosis,
        in string memberName,
        IEnumerable<DirectionValueDefinition> inputCallerDefinitions,
        IEnumerable<DirectionValueDefinition> outputCallerDefinitions,
        Action<IMemberDiagnosis> invokeOwnerPropertyChanged,
        Func<TMemberValueType?> getOriginalOutputValue,
        Action<TMemberValueType?> setCurrentInputValue)
    {
        _invokeOwnerPropertyChanged = invokeOwnerPropertyChanged;
        ObjectDiagnosis = objectDiagnosis;
        MemberName = memberName;
        InputValueDiagnosis = new InputValueDiagnosis<TOwnerType, TMemberValueType?>(
            this, inputCallerDefinitions, setCurrentInputValue);
        OutputValueDiagnosis = new OutputValueDiagnosis<TOwnerType, TMemberValueType?>(
            this, outputCallerDefinitions, InvokeOwnerPropertyChanged,
            getOriginalOutputValue,
            inputValueChanged => 
                InputValueDiagnosis.CurrentValue.ValueChangedUnified += inputValueChanged);
        DirectionValues = new IDirectionValueDiagnosis<TOwnerType, TMemberValueType?>[] { InputValueDiagnosis, OutputValueDiagnosis };
    }
    
    public override string ToString() =>
        $"{GetType().GetNameWithoutGenericArity()} for {MemberName} of " +
        $"{ObjectDiagnosis.GetOwnerTypeString()}";

    public string ToCurrentValueString() =>
        $"{InputValueDiagnosis.ToCurrentValueString()}, {OutputValueDiagnosis.ToCurrentValueString()}";
    
    private void InvokeOwnerPropertyChanged() =>
        _invokeOwnerPropertyChanged(this);
}