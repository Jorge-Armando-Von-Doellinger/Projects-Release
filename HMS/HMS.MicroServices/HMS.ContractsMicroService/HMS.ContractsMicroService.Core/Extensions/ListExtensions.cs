﻿using System.Collections;

namespace HMS.ContractsMicroService.Core.Extensions
{
    public static class ListExtensions
    {
        public static ListTaget FromTo<ListTaget>(this IList list)
        {
            try
            {
                var itemTargetType = typeof(ListTaget).GetGenericArguments()[0];
                var typeList = typeof(List<>).MakeGenericType(itemTargetType);
                var targetList = (IList) Activator.CreateInstance(typeList);
                Console.WriteLine(targetList.GetType());
                //Enum vire uma string
                foreach ( var item in list)
                {
                    var itemTarget = item.FromTo(itemTargetType);
                    Console.WriteLine(itemTarget.GetType());
                    targetList.Add(itemTarget);
                }
                return (ListTaget) targetList;
            }
            catch
            {
                throw;
                return default;
            }
        }



    }
}
