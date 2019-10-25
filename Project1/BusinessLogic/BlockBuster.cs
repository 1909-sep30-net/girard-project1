

namespace BusinessLogic
{
    public class BlockBuster
    {
        public string Location { get; set; }

        public int LocationId { get; set; }

        public BlockBuster (int id, string location)
        {
            this.Location = location;
            this.LocationId = id;
        }
    }
}
