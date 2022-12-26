namespace lab
{
    public class Program3
    {
        private static void Main()
        {
            var list = new List<string>() { "code", "doce", "ecod", "framer", "frame" };

            DeleteAnagrams(list);
        }

        public static void DeleteAnagrams(List<string> list)
        {
            List<string> answer = new List<string>();
            List<string> keys = new List<string>();

            foreach (string s in list)
            {
                List<char> element = s.ToList();
                element.Sort();
                string str = new string(element.ToArray());

                if (!Find(keys, str))
                {
                    keys.Add(str);
                    answer.Add(s);
                }
            }
            answer.Sort();
            Print(answer);
        }

        public static bool Find(List<string> list, string element)
        {
            foreach (string item in list)
            {
                if (item == element)
                    return true;
            }
            return false;
        }

        public static void Print(List<string> answer)
        {
            foreach (string str in answer)
            {
                Console.Write(str + " ");
            }
        }
    }
}
