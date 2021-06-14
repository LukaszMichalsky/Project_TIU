using System;

namespace zoo_manager_backend.Exceptions {
    public class RequiredItemNotExistsException : Exception {
        public RequiredItemNotExistsException(string message) : base(message) {}
    }
}
