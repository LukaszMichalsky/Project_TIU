using System;

namespace zoo_manager_backend.Exceptions {
    public class AssociationExistsException : Exception {
        public AssociationExistsException(string message) : base(message) {}
    }
}
