using Core.Beans;
using Repositories;

namespace Core.Mappers
{
    /// <summary>
    /// Provides methods to map between ErrorLog and ErrorLogDTO.
    /// </summary>
    public static class ErrorLogMapper
    {
        public static ErrorLogDTO ToDTO(ErrorLog errorLog)
        {
            return new ErrorLogDTO
            {
                ErrorMessage = errorLog.ErrorMessage,
                StackTrace = errorLog.StackTrace,
                ExceptionType = errorLog.ExceptionType,
                StatusCode = errorLog.StatusCode,
                ControllerName = errorLog.ControllerName,
                ActionName = errorLog.ActionName
            };
        }

        public static ErrorLog ToEntity(ErrorLogDTO errorLogDTO)
        {
            return new ErrorLog
            {
                ErrorMessage = errorLogDTO.ErrorMessage,
                StackTrace = errorLogDTO.StackTrace,
                ExceptionType = errorLogDTO.ExceptionType,
                StatusCode = errorLogDTO.StatusCode,
                ControllerName = errorLogDTO.ControllerName,
                ActionName = errorLogDTO.ActionName,
                ErrorOccurAt = DateTime.UtcNow, // Default to current time for new entities
                IsSolved = false, // Default to false for new entities
                ErrorOccurCount = 1 // Default to 1 for new entities
            };
        }
    }

}