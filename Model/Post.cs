

namespace bloodDonationAppBackend.Model
{
    public class Post
    {
        public int Id { get; set; }
        public string postTitle { get; set; }
        public string bloodGroup { get; set; }
        public int amountBlood { get; set; } 
        public string date { get; set; }   
        public string hospitalName { get; set; }
        public string message { get; set; }
        public string mobileNumber { get; set; }
        public string country { get; set; }
        public string city { get; set; }


    }
}
