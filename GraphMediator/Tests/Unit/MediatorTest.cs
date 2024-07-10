using GraphMediator.GraphEngineMediator;
using GraphMediator.Stubs;
using System.Text.Json;
using Xunit;

namespace GraphMediator.Tests.Unit;

public class MediatorTest
{
    [Fact]
    public void CollectChildData()
    {
        var mediator = new Mediator(new OneCarePatient(1, new DateTime(2005, 04, 22)), new TestExaminationData());
        mediator.CollectExaminationData(ExaminationJsonString(1));
        Assert.True(true);
    }

    private string ExaminationJsonString(int patientId) =>
         JsonSerializer.Serialize(patient);

    private record PatientInformation(int patientId, DateOnly birthDate);



}
