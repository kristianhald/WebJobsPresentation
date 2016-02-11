namespace ProcessingJob
{
    public class CaseData
    {
        public CaseData(string name, string data)
        {
            Name = name;
            Data = data;
        }

        public string Name { get; }

        public string Data { get; }
    }
}
