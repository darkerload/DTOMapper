using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTOMapper
{
    public static class Mapper
    {
        /// <summary>
        /// Data transfer between two object.
        /// </summary>
        /// <typeparam name="TSource">Source Object Type</typeparam>
        /// <typeparam name="TTarget">Target Object Type</typeparam>
        /// <param name="data">Must be same type with source type</param>
        /// <returns></returns>
        public static TTarget Bind<TSource, TTarget>(TSource data)
        {
            if(data == null)
            {
                throw new ArgumentNullException("Source Data Is Null");
            }

            var SourceType = typeof(TSource);
            var TargetType = typeof(TTarget);


            if (TargetType.IsGenericType)
            {
                object newTarget = Activator.CreateInstance(TargetType);


                Type targetGenericName = newTarget.GetType().GetGenericTypeDefinition();
                Type targetGenericArgName = newTarget.GetType().GetGenericArguments()[0];

                var newCollection = (IList)data;
                foreach (var item in newCollection)
                {
                    object concreteType = Activator.CreateInstance(targetGenericArgName);
                    foreach (var prop in item.GetType().GetProperties())
                    {
                        var name = prop.Name;
                        var val = prop.GetValue(item, null);

                        var existField = concreteType.GetType().GetProperty(name);
                        if (existField != null)
                        {
                            existField.SetValue(concreteType, val);
                        }
                        
                    }

                    newTarget.GetType().GetMethod("Add").Invoke(newTarget, new[] { concreteType });
                   
                }

                return (TTarget)newTarget;

             }
            else
            {

                object targetObj = Activator.CreateInstance(TargetType);
                PropertyInfo[] props = data.GetType().GetProperties();

                foreach (PropertyInfo prop in props)
                {
                    var name = prop.Name;
                    var value = prop.GetValue(data, null);

                    var existField = targetObj.GetType().GetProperty(name);
                    if(existField != null)
                    {
                        existField.SetValue(targetObj, value);
                    }

                }
                return (TTarget)targetObj;
            }

        }
    }
}
