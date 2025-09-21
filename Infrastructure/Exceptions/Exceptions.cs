using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message, Exception? innerException = null)
            : base(message, innerException) { }
    }

    public class EntityNotFoundException : RepositoryException
    {
        public EntityNotFoundException(string entityName, object id)
            : base($"{entityName} with id '{id}' was not found.") { }
        public EntityNotFoundException(string entityName, object id, object date)
    : base($"{entityName} with id '{id}' and date '{date}' was not found.") { }
    }

    public class EntityAddException : RepositoryException
    {
        public EntityAddException(string entityName, Exception innerException)
            : base($"Failed to add {entityName}.", innerException) { }
    }

    public class EntityUpdateException : RepositoryException
    {
        public EntityUpdateException(string entityName, Exception innerException)
            : base($"Failed to update {entityName}.", innerException) { }
    }

    public class EntityRemoveException : RepositoryException
    {
        public EntityRemoveException(string entityName, Exception innerException)
            : base($"Failed to remove {entityName}.", innerException) { }
    }
}
