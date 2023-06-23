using System.ComponentModel;
using RuntimeDiagnosis.Core.ObjectDiagnose;

namespace RuntimeDiagnosis.Core;

public interface IDiagnosableObject : INotifyPropertyChanged
{
    public ObjectDiagnose<IDiagnosableObject> ObjectDiagnose { get; }
}