using System.Collections;

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
                var targetList = (IList)Activator.CreateInstance(typeList);
                //Enum vire uma string
                foreach (var item in list)
                {
                    var itemTarget = item.FromTo(itemTargetType);
                    targetList.Add(itemTarget);
                }
                return (ListTaget)targetList;
            }
            catch
            {
                throw;
                return default;
            }
        }
        internal static bool SequenceEquals(this IList list1, IList list2)
        {
            if (list1.Count != list2.Count) return false;
            if (list1 == null || list2 == null) return false;
            // Se passar por todos e todos forem iguais, retorno true
            for (int i = 0; i < list1.Count; i++)
            {
                if (Equals(list1[i], list2[i]) == false)
                    return false;
            }
            return true;
        }

    }
}
