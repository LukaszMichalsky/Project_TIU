using System;

namespace zoo_manager_backend.Exceptions {
    public class DuplicateFoundException : Exception {
        public DuplicateFoundException(string message) : base(message) {}
    }
}
