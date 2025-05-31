namespace bloodDonationAppBackend.Model
{
    public class userProfile
    {

        public int Id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        
        public string bloodGroup { get; set; }
        public string mobileNumber { get; set; }
        public string gender { get; set; }
        public bool bloodDonate { get; set; }
        public string message { get; set; }
    }
}
