namespace powerplant_coding_challenge.Models
{
    public class Response
    {
        public string name { get; set; }
        public int p { get; set; }
        public Response(string name1, int power)
        {
            name = name1;
            p = power;
        }
    }
}