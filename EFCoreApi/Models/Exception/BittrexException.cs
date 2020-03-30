using System;

namespace EFCoreApi.Models.Exception
{
	/// <summary>
	/// Исключение при обращении к API.
	/// </summary>
	public class BittrexException : System.Exception
	{
		/// <summary>
		/// Текст исключения.
		/// </summary>
		private readonly string _errorMessage = @"Ошибка при получении данных с Bittrex API.";

		/// <summary>
		/// Свойство исключения.
		/// </summary>
		public override string Message { get; }

		/// <summary>
		/// Конструктор по типу исключения и тексту ошибки.
		/// </summary>
		public BittrexException(string errorDescription)
		{
			Message = $"{_errorMessage}.{Environment.NewLine} Причина : {errorDescription}";
		}
	}
}