using System;

namespace EFCoreApi.Models.Exception
{
	public class DataBaseException : System.Exception
	{
		/// <summary>
		/// Текст исключения.
		/// </summary>
		private readonly string _errorMessage = @"Ошибка при взаимодействии с БД.";

		/// <summary>
		/// Свойство исключения.
		/// </summary>
		public override string Message { get; }
		
		/// <summary>
		/// Конструктор по типу исключения и тексту ошибки.
		/// </summary>
		public DataBaseException(string errorDescription)
		{
			Message = $"{_errorMessage}.{Environment.NewLine} Причина : {errorDescription}";
		}
	}
}