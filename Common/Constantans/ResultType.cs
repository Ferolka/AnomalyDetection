namespace Common.Constantans
{
    /// <summary>
    /// Result of entity reading operation.
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// Entity was found
        /// </summary>
        Success,

        /// <summary>
        /// Entity was not found
        /// </summary>
        NotFound,

        /// <summary>
        /// Database error appears.
        /// </summary>
        DatabaseError,

        /// <summary>
        /// The unexpected error.
        /// </summary>
        UnexpectedError,
    }
}