using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.Sqlite;

namespace OM.MFPTracker.Data.Helper
{
	public class DuplicateValueException : Exception
	{
		public DuplicateValueException(string message)
			: base(message)
		{
		}
	}
	public static class DBValidation
	{
		public static bool IsUniqueConstraintViolation(DbUpdateException ex)
		{
			return ex.InnerException is SqliteException sqliteEx
				   && sqliteEx.SqliteErrorCode == 19;
		}
	}
}