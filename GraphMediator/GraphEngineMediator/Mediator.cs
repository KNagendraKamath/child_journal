
using System.Text.Json;
using GraphMediator.Stubs;

namespace GraphMediator.GraphEngineMediator;

internal class Mediator
{
    private readonly OneCarePatient _oneCarePatient;

    public Mediator(OneCarePatient oneCarePatient,DataSource dataAPI)
    {
        _oneCarePatient = oneCarePatient;
    }

    internal void CollectExaminationData(string jsonString)
    {
        _patientInfo = JsonSerializer.Deserialize<PatientInformation>(jsonString)!;
    }
}
