using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace EntityFramework.LookupTables
{
    public static class TypeExtensions
    {
        internal static IEnumerable<object> GetEnumValuesFor(this Type type) => Enum.GetValues(type).Cast<object>();

        public static Type GetAssemblyEnumsFor(this Type type) => type.Assembly.GetTypes()
                ?.Single(x => x.IsEnum
                && x.Name.EndsWith(type.Name, StringComparison.InvariantCultureIgnoreCase)
                | x.Name.StartsWith(type.Name, StringComparison.InvariantCultureIgnoreCase)
                );

        internal static void EnumTypeCheck<TEnum>()
        {
            var enumType = typeof(TEnum);
            enumType.EnumTypeCheck();
        }

        internal static void EnumTypeCheck(this Type enumType)
        {
            if (!enumType.IsSubclassOf(typeof(Enum)))
                throw new ArgumentException($"Type argument {nameof(enumType)} is not an enum: { enumType.Name}");
        }
    }

    public static class DbContextExtensions
    {
        public static void SeedAllEnumValues(this DbContext context)
        {
            foreach (var type in TypeHelpers.GetLookupTableTypes())
            {
                var enumType = type.GetAssemblyEnumsFor();
                context.SeedEnumValues(type, enumType);
            }
        }

        public static void SeedEnumValues(this DbContext context, Type classType, Type enumType)
        {
            var method = typeof(DbContextExtensions).GetMethod(nameof(DbContextExtensions.SeedEnumValues),
               BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(DbContext) }, null);
            var genericMethod = method.MakeGenericMethod(new Type[] { classType, enumType });
            genericMethod.Invoke(null, new object[] { context });
        }

        public static void SeedEnumValues<TEntity, TEnum>(this DbContext context)
            where TEntity : class
        {
            TypeExtensions.EnumTypeCheck<TEnum>();
            var entities = context.Set<TEntity>();
            entities.SeedEnumValues<TEntity, TEnum>();
            context.SaveChanges();
            var toDelete = entities.ToList().Skip(typeof(TEnum).GetEnumValuesFor().ToList().Count).ToList();
            while (toDelete.Count > 0)
            {
                var current = toDelete.Last();
                var objectContext = ((IObjectContextAdapter)context).ObjectContext;
                objectContext.DeleteObject(current);
                objectContext.SaveChanges();
                toDelete.Remove(current);
            }
        }
    }

    internal static class DbSetExtensions
    {
        internal static void SeedEnumValues<TEntity, TEnum>(this IDbSet<TEntity> dbSet)
            where TEntity : class
        {
            TypeExtensions.EnumTypeCheck<TEnum>();
            var converter = FuncHeplers.Converter<TEnum, TEntity>();

            var enumList = typeof(TEnum).GetEnumValuesFor()
                                    .Select(value => converter((TEnum)value))
                                    .ToList();

            enumList.ForEach(instance => dbSet.AddOrUpdate(instance));
        }
    }

    internal static class ConventionExtensions
    {
        internal static void LookupTable<TId>(this Convention convention)
            where TId : struct

        {
            convention.Types<ILookupTable<TId>>().Configure(c =>
            {
                c.Property(p => p.Id)
                    .IsKey()
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                c.Property(x => x.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();
            });
        }
    }
}