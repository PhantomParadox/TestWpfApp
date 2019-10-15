using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LogService
{
    public static class LogManager
    {
        public static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem, Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        public static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem)
            where TSource : class
        {
            return FromHierarchy(source, nextItem, s => s != null);
        }

        public static string GetAllMessages(this Exception exception)
        {
            var messages = exception
                .FromHierarchy(ex => ex.InnerException)
                .Select(ex => ex.Message);

            return string.Join(Environment.NewLine, messages);
        }
        public static void Write(this Exception ex)
        {
            var method = new StackTrace().GetFrame(1).GetMethod();
            string CallerInformation = string.Format("I was called from '{0}' of class '{1}'", method.Name, method.DeclaringType);
            string exc = ex.GetAllMessages();
            string date = $"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}";

            string result = $"\n\t {date} \n\t{CallerInformation} \n\t {exc}";

            Debug.WriteLine(result);

        }
        public static string Show(this Exception ex)
        {
            var method = new StackTrace().GetFrame(1).GetMethod();
            string CallerInformation = string.Format("I was called from '{0}' of class '{1}'", method.Name, method.DeclaringType);
            string exc = ex.GetAllMessages();
            string date = $"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}";

            string result = $"\n\t {date} \n\t{CallerInformation} \n\t {exc}";

            Debug.WriteLine(result);
            return result;

        }
    }
}
