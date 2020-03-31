using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace EFCoreApi
{
	/// <summary>
	/// Модуль инъекции полей и свойств, и разрешения зависимостей.
	/// </summary>
	public class EFCoreApiAutofacModule : Module
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			var assemblyNames = new[] { Assembly.GetAssembly(typeof(EFCoreApiAutofacModule)) };

			builder.RegisterAssemblyTypes(assemblyNames)
				.Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Helper")).AsImplementedInterfaces().AsSelf().SingleInstance();

			base.Load(builder);
		}
	}
}
