using System;

namespace SharedTools
{
    public static class TypeExtensions
    {
        const int indentLevel = 4;

        public static string GetFriendlyName(this Type t)
        {
            string name = t.Name;

            if (t.IsGenericType)
            {
                name = name.Remove(name.IndexOf('`')) + "<";
                Type[] gArguments = t.GetGenericArguments();
                string[] args = new string[gArguments.Length];

                for (int x = 0; x < gArguments.Length; x++)
                {
                    args[x] = gArguments[x].GetFriendlyName();
                }
                name += string.Join(", ", args) + ">";
            }
            return name;
        }
    }
}
