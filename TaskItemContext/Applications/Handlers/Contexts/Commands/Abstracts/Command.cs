namespace WorkItem.Application.Handlers.Contexts.Commands.Abstracts
{
    public abstract record Command
    {
        public Guid CorrelationId { get; init; }

        protected Command()
        {
            CorrelationId = Guid.NewGuid();
        }

        protected Command(Guid correlationId)
        {
            CorrelationId = correlationId == Guid.Empty ? Guid.NewGuid() : correlationId;
        }
    }
}
