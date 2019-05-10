namespace IdentityTraning.Models.DbModel
{
    public class Worker
    {
        public string Id { get; set; }

        public string Information { get; set; }
        public string ImgLink { get; set; }

        public virtual Position Position { get; set; }
        public virtual User User { get; set; }
        public virtual Shop Shop { get; set; }
    }
}