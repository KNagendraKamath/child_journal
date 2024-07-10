using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphEngine.Tests.Unit.ChildJournalColumns;
using Xunit;

namespace GraphMediator.Tests.Unit
{
    public class GraphFactoryTest
    {
        [Fact]
        public void CreateFactory()
        {
            var factory = new GraphFactory(Age, Weight, TestReferenceSources);
            Assert.True(true);
        }
    }
}
