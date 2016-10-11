using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EntityFramework.LookupTables
{
    internal static class AssemblyHeplers
    {
        internal static IEnumerable<Assembly> GetCurrentAssemblies()
        {
            var _assemblies = from Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()
                              where !(assembly is System.Reflection.Emit.AssemblyBuilder)
                              && !assembly.GlobalAssemblyCache
                              && !assembly.SecurityRuleSet.ToString().Equals("Level1")
                              && !assembly.GetType().FullName.Equals("System.Reflection.Emit.InternalAssemblyBuilder")
                              select assembly;

            return _assemblies;
        }
    }

    internal static class FuncHeplers
    {
        public static Func<TEnum, TEntity> Converter<TEnum, TEntity>()
        {
            return new Func<TEnum, TEntity>((TEnum args) =>
            (TEntity)Activator
            .CreateInstance(typeof(TEntity),
            BindingFlags.Instance | BindingFlags.NonPublic,
            null, new object[] { args }, null));
        }
    }

    public static class TypeHelpers
    {
        public static bool DoesTypeSupportInterface(this Type type, Type inter)
        {
            if (inter.IsAssignableFrom(type))
                return true;
            if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == inter))
                return true;
            return false;
        }

        public static IEnumerable<Type> GetLookupTableTypes()
        {
            var assemblies = AssemblyHeplers.GetCurrentAssemblies();
            var desTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                var type = assembly.GetTypes()
                          ?.Where(x => x.IsClass &&
                          x.DoesTypeSupportInterface(typeof(ILookupTable<>)
                          ));
                desTypes.AddRange(type);
            }
            return desTypes;
        }
    }
}