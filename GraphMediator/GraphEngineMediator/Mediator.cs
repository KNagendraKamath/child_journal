using GraphMediator.Stubs;
using System;

namespace GraphMediator.GraphEngineMediator
{
    internal class Mediator
    {
        private readonly OneCarePatient _oneCarePatient;

        public Mediator(OneCarePatient oneCarePatient,DataSource dataAPI)
        {
            _oneCarePatient = oneCarePatient;
        }

        internal void CollectExaminationData(string jsonString)
        {
            throw new NotImplementedException();
        }
    }
}
