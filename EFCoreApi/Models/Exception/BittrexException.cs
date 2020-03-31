namespace EFCoreApi.Models.Exception
{
	/// <summary>
	/// Исключение при обращении к API.
	/// </summary>
	public class BittrexException : BaseException
	{
		/// <summary>
		/// Текст исключения.
		/// </summary>
		private const string ErrorMessage = "Ошибка при получении данных с Bittrex API.";

		/// <summary>
		/// Конструктор по типу исключения и тексту ошибки.
		/// </summary>
		public BittrexException(string errorDescription) : base(ErrorMessage, errorDescription)
		{
		}
	}
}