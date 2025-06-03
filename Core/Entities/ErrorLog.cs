using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// This table logs application errors and exception events. Each row stores the error details, including the message, stack trace, type, time of occurrence, status code, controller/action context, resolution status, and number of occurrences. Deduplication and counting of repeated errors is handled by triggers.
/// </summary>
[Table("ErrorLog")]
public partial class ErrorLog
{
    [Key]
    [Column("error_id")]
    public Guid ErrorId { get; set; }

    [Column("error_message")]
    public string ErrorMessage { get; set; } = null!;

    [Column("stack_trace")]
    public string? StackTrace { get; set; }

    [Column("exception_type")]
    [StringLength(255)]
    public string ExceptionType { get; set; } = null!;

    [Column("error_occur_at", TypeName = "timestamp without time zone")]
    public DateTime ErrorOccurAt { get; set; }

    [Column("status_code")]
    [StringLength(20)]
    public string StatusCode { get; set; } = null!;

    [Column("controller_name")]
    [StringLength(255)]
    public string? ControllerName { get; set; }

    [Column("action_name")]
    [StringLength(255)]
    public string? ActionName { get; set; }

    [Column("is_solved")]
    public bool IsSolved { get; set; }

    [Column("solved_at", TypeName = "timestamp without time zone")]
    public DateTime? SolvedAt { get; set; }

    [Column("solved_by")]
    public Guid? SolvedBy { get; set; }

    /// <summary>
    /// Counts how many times this specific error has occurred (duplicates are incremented by trigger).
    /// </summary>
    [Column("error_occur_count")]
    public int ErrorOccurCount { get; set; }
}
