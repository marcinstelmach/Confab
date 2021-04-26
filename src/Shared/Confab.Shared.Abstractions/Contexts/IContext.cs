namespace Confab.Shared.Abstractions.Contexts
{
    public interface IContext
    {
        public string RequestId { get; }

        public string  TraceId { get; }

        public IIdentityContext Identity { get; }
    }
}