using Common.Constantans;

namespace Common.Models
{
    public readonly struct ResultModel
    {
        internal ResultModel(ResultType status, Exception? exception = null)
        {
            Status = status;
            Exception = exception;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess => Status == ResultType.Success;

        /// <summary>
        /// Gets error Message (if any).
        /// </summary>
        public Exception? Exception { get; }

        /// <summary>
        /// Gets the type of the status result.
        /// </summary>
        /// <value>
        /// The type of the status result.
        /// </value>
        public ResultType Status { get; }

        /// <summary>
        /// Creates result when data found.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>GetEntityResult.</returns>
        public static ResultModel FromSuccess()
        {
            return new ResultModel(ResultType.Success);
        }

        /// <summary>
        /// Creates result when DB error occurred.
        /// </summary>
        /// <param name="exception">The error message.</param>
        /// <exception cref="ArgumentNullException">errorMessage.</exception>
        /// <returns>GetEntityResult.</returns>
        public static ResultModel FromDbError(Exception? exception)
        {
            ArgumentNullException.ThrowIfNull(exception);

            return new ResultModel(ResultType.DatabaseError, exception: exception);
        }

        /// <summary>
        /// Creates result when unexpected error occurred.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <exception cref="ArgumentNullException">errorMessage.</exception>
        /// <returns>GetEntityResult.</returns>
        public static ResultModel FromUnexpectedError(Exception exception)
        {
            ArgumentNullException.ThrowIfNull(exception);

            return new ResultModel(ResultType.UnexpectedError, exception: exception);
        }
    }

    /// <summary>
    /// Generic result for getting entity from database.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public readonly struct ResultModel<T>
    {
        private ResultModel(ResultType status, T? entity = default, Exception? exception = null)
        {
            Status = status;
            Entity = entity;
            Exception = exception;
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public T? Entity { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess => Status == ResultType.Success;

        /// <summary>
        /// Gets error Message (if any).
        /// </summary>
        public Exception? Exception { get; }

        /// <summary>
        /// Gets the type of the status result.
        /// </summary>
        /// <value>
        /// The type of the status result.
        /// </value>
        public ResultType Status { get; }

        /// <summary>
        /// Creates result when data not found.
        /// </summary>
        /// <returns>GetEntityResult.</returns>
        public static ResultModel<T> FromNotFound()
        {
            return new ResultModel<T>(ResultType.NotFound, default);
        }

        /// <summary>
        /// Creates result when data found.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>GetEntityResult.</returns>
        public static ResultModel<T> FromSuccess(T entity)
        {
            return new ResultModel<T>(ResultType.Success, entity);
        }

        /// <summary>
        /// Creates result when DB error occurred.
        /// </summary>
        /// <param name="exception">The error message.</param>
        /// <exception cref="ArgumentNullException">errorMessage.</exception>
        /// <returns>GetEntityResult.</returns>
        public static ResultModel<T> FromDbError(Exception? exception)
        {
            ArgumentNullException.ThrowIfNull(exception);

            return new ResultModel<T>(ResultType.DatabaseError, exception: exception);
        }

        /// <summary>
        /// Creates result when unexpected error occurred.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <exception cref="ArgumentNullException">errorMessage.</exception>
        /// <returns>GetEntityResult.</returns>
        public static ResultModel<T> FromUnexpectedError(Exception exception)
        {
            ArgumentNullException.ThrowIfNull(exception);

            return new ResultModel<T>(ResultType.UnexpectedError, exception: exception);
        }

        public static implicit operator ResultModel(ResultModel<T> resultModelT)
        {
            if (resultModelT.IsSuccess)
            {
                throw new InvalidCastException("Couldn't cast success result with entity");
            }

            return new ResultModel(resultModelT.Status, resultModelT.Exception);
        }
    }
}