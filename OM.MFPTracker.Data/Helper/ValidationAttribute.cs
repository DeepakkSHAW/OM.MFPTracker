using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.Sqlite;

namespace OM.MFPTracker.Data.Helper
{
	public static class Helper
	{
		public static string GetDisplayName(Enum enumValue)
		{
			var display = enumValue.GetType()
								   .GetMember(enumValue.ToString())
								   .First()
								   .GetCustomAttributes(false)
								   .OfType<DisplayAttribute>()
								   .FirstOrDefault();

			return display?.Name ?? enumValue.ToString();
		}
	}
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