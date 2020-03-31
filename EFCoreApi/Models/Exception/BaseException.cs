using System;

namespace EFCoreApi.Models.Exception
{
	public abstract class BaseException : System.Exception
	{
		/// <summary>
		/// Свойство исключения.
		/// </summary>
		public override string Message { get; }

		/// <summary>
		/// Конструктор по типу исключения и тексту ошибки.
		/// </summary>
		protected BaseException(string errorMessage, string errorDescription)
		{
			Message = $"{errorMessage}.{Environment.NewLine} Причина : {errorDescription}";
		}
	}
}