
namespace AonWeb.FluentHttp.Mocks 
{
    public class QueuedMockHalCallBuilder : MockHalCallBuilder {
        public QueuedMockHalCallBuilder()
            : base(new QueuedMockTypedHttpCallBuilder()) { }
    }
}