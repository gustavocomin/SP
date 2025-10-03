namespace SP.Infraestrutura.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string EntityName { get; }
        public object EntityId { get; }

        public EntityNotFoundException(string entityName, object entityId) 
            : base($"'{entityName}' com id '{entityId}' não foi encontrado")
        {
            EntityName = entityName;
            EntityId = entityId;
        }

        public EntityNotFoundException(string entityName, object entityId, string message) 
            : base(message)
        {
            EntityName = entityName;
            EntityId = entityId;
        }

        public EntityNotFoundException(string entityName, object entityId, string message, Exception innerException) 
            : base(message, innerException)
        {
            EntityName = entityName;
            EntityId = entityId;
        }
    }
}
