namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.SingleValue;

public sealed class SingleValueEditable<TOwnerType, TMemberValueType, TValueType> : 
    SingleValueAlwaysEditable<TOwnerType, TMemberValueType?, TValueType?>, 
    ISingleValueEditable<TOwnerType, TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{
    public new bool EditingCurrentlyAllowed
    {
        get => base.EditingCurrentlyAllowed;
        internal set => base.EditingCurrentlyAllowed = value;
    }

    public SingleValueEditable(IDirectionValue<TOwnerType, TMemberValueType?> directionValue, 
        string name, bool editingCurrentlyAllowed = false) :
        base(directionValue, name, editingCurrentlyAllowed)
    { }
}