using System.Globalization;

namespace laba
{
    public class Command
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Position { get; set; }
        public string Color { get; set; }
        public bool IsFull { get; set; }
        public string Words { get; set; }

        public Command(string str)
        {
            var strArr = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(str);
            SetTime(strArr[0], strArr[1]);
            SetPositionColor(strArr.ToList());
            SetWords(strArr);
        }
        public void SetTime(string start, string end)
        {
            Start = DateTime.ParseExact(start, "mm:ss", CultureInfo.InvariantCulture);
            End = DateTime.ParseExact(end, "mm:ss", CultureInfo.InvariantCulture);
        }

        public void SetPositionColor(List<string> strList)
        {
            var checkList = new List<string>() { "Top", "Left", "Bottom", "Right" };
            if (checkList.Contains(strList[2]))
            {
                Position = strList[2];
                Color = strList[3];
                IsFull = true;
            }
            else
            {
                Position = "Bottom";
                Color = "White";
                IsFull = false;
            }
        }

        public void SetWords(string[] strArr)
        {
            if (IsFull)
            {
                var list = CutArr(strArr, 4);
                Words = String.Join(" ", list);
            }
            else
            {
                var list = CutArr(strArr, 2);
                Words = String.Join(" ", list);
            }
        }

        public List<string> CutArr(string[] strArr, int length)
        {
            var list = new List<string>();
            for (var i = length; i < strArr.Length; i++)
            {
                list.Add(strArr[i]);
            }

            return list;
        }
    }
}
