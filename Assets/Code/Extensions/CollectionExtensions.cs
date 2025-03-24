using System.Collections.Generic;

public static class CollectionExtensions
{
    public static bool ContainsId<T>(this IList<T> list, int id) where T : IIdEqualable
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].IdEquals(id))
                return true;
        }
        return false;
    }

    public static T GetElementById<T>(this IList<T> list, int id) where T : IIdEqualable
    {
        if (list != null)
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].IdEquals(id))
                    return list[i];
            }
        return default(T);
    }
}
